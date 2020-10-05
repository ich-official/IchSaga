//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-19 21:14:49
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// socket协议方式，账号中角色相关的controller，等于RoleController，
/// </summary>
public class RoleCtrlrS : SingletonBase<RoleCtrlrS> ,IDisposable{
    /// <summary>
    /// 初始化账号-角色相关
    /// </summary>
    public void Init()
    {
        //增加监听
        #region 登录相关
        //客户端发送登录区服消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_LoginGameServerReqProto, OnLoginGameServer);

        //客户端发送创建角色消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_AddRoleReqProto, OnAddRole);

        //客户端发送删除角色消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_DeleteRoleReqProto, OnDeleteRole);

        //客户端发送进入游戏消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_EnterGameReqProto, OnEnterGame);

        //客户端发送获取角色详情消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_RoleInfoReqProto, OnGetRoleInfo);

        #endregion
    }
    #region req

    /// <summary>
    /// 登陆时服务器的操作
    /// </summary>
    /// <param name="role"></param>
    /// <param name="buffer"></param>
    private void OnLoginGameServer(byte[] buffer)
    {
        Account_LoginGameServerReqProto proto = Account_LoginGameServerReqProto.GetProto(buffer);

        //玩家帐号
        int accountId = proto.AccountId;
        int gameServerId = proto.GameServerId;
        //role.AccountId = accountId;
        LoginGameServerResp(accountId, gameServerId);
    }

    private void OnEnterGame( byte[] buffer)
    {
        Account_EnterGameReqProto proto = Account_EnterGameReqProto.GetProto(buffer);
        int roleID = proto.RoleId;

        EnterGameServerResp();
    }

    private void OnDeleteRole( byte[] buffer)
    {
        Account_DeleteRoleReqProto proto = Account_DeleteRoleReqProto.GetProto(buffer);
        int roleID = proto.RoleId;
        Debug.Log("server: roleID---" + roleID);
        DeleteRoleServerResp(roleID);

    }

    private void OnAddRole(byte[] buffer)
    {
        Account_AddRoleReqProto proto = Account_AddRoleReqProto.GetProto(buffer);
        int classId = proto.ClassId;
        string nickName = proto.RoleNickName;
        int gameServerId = proto.GameServerId;
        AddRoleServerResp(classId, nickName, gameServerId);
    }

    private void OnGetRoleInfo(byte[] buffer)
    {
        Account_RoleInfoReqProto proto = Account_RoleInfoReqProto.GetProto(buffer);
        int roleId = proto.RoldId;
        GetRoleInfoResp(roleId);
    }

    #endregion


    #region resp
    /// <summary>
    /// 服务器操作，把客户端传来的accountID查询对应的RoleID，并把这个role对象返回给客户端
    /// 20.07.10Ich新增区服判断
    /// </summary>
    /// <param name="role"></param>
    /// <param name="accountId"></param>
    private void LoginGameServerResp(int accountId,int gameServerId)
    {
        Account_LoginGameServerRespProto proto = new Account_LoginGameServerRespProto();
        //TODO 这步是调用CacheModel层去数据库查角色，返回一个角色实体列表，后续改成我自己的逻辑    （select * from Role where [AccountId]=accountId）

        List<RoleEntity> lst=RoleDBModelServer.Instance.GetRoles(accountId,gameServerId);
        int count = 0;
        while (count < lst.Count)
        {
            //如果角色状态是已删除，则不向客户端返回该角色
            if (lst[count].Status == EnumEntityStatus.Deleted)
            {
                Debug.Log(lst[count].NickName + "是已删除的角色！");
                lst.RemoveAt(count);                
            }
            else
            {
                count++;
            }
        }
        //List<RoleEntity> lst = RoleCacheModel.Instance.GetList(condition: string.Format("[AccountId]={0}", accountId));

        if (lst != null && lst.Count > 0)
        {
            proto.RoleCount = lst.Count;
            proto.RoleList = new List<Account_LoginGameServerRespProto.RoleItem>();
            GlobalCache.Instance.Account_CurrentId = accountId; //登陆成功时记录一下当前的accountId，后面创建角色时使用
            for (int i = 0; i < lst.Count; i++)
            {
                proto.RoleList.Add(new Account_LoginGameServerRespProto.RoleItem()
                {
                    RoleId = lst[i].Id,
                    RoleNickName = lst[i].NickName,
                    RoleLevel = lst[i].Level,
                    RoleClass = (byte)lst[i].ClassId
                });
            }
        }
        else
        {
            proto.RoleCount = 0;
        }

        //给客户端发消息，
        SocketManagerServer.Instance.SendMessageToClient(proto.ToArray());
        //role.Client_Socket.SendMsg(proto.ToArray());
    }

    /// <summary>
    /// 添加角色成功时，msgCode返回当前角色的ID
    /// </summary>
    /// <param name="classId"></param>
    /// <param name="nickName"></param>
    /// <param name="gameServerId"></param>
    private void AddRoleServerResp(int classId,string nickName,int gameServerId)
    {
        Account_AddRoleRespProto proto = new Account_AddRoleRespProto();
        int currentAddId = RoleDBModelServer.Instance.AddRole(classId,nickName,gameServerId,1);
        if (currentAddId == -1)
        {
            proto.IsSuccess = false;
            proto.MsgCode = Constant.ROLE_ADD_FAIL;
        }
        else
        {
            proto.IsSuccess = true;
            proto.MsgCode = currentAddId;
        }
        SocketManagerServer.Instance.SendMessageToClient(proto.ToArray());
    }

    private void DeleteRoleServerResp(int roleId)
    {
        Account_DeleteRoleRespProto proto = new Account_DeleteRoleRespProto();
        bool isSuccess = RoleDBModelServer.Instance.DeleteRole(roleId);
        proto.IsSuccess = isSuccess;
        if (isSuccess == false) proto.MsgCode = Constant.ROLE_DELETE_FAIL;
        SocketManagerServer.Instance.SendMessageToClient(proto.ToArray());
    }

    private void EnterGameServerResp()
    {
        Account_EnterGameRespProto proto = new Account_EnterGameRespProto();
        bool isSuccess = true;  //暂时写死
        proto.IsSuccess = isSuccess;
        if (isSuccess == false) proto.MsgCode = Constant.ENTER_GAME_FAIL;
        SocketManagerServer.Instance.SendMessageToClient(proto.ToArray());
    }
    private void GetRoleInfoResp(int roleId)
    {
        Account_RoleInfoRespProto proto = new Account_RoleInfoRespProto();
        Dictionary<string, string> result = new Dictionary<string, string>();
        result=RoleDBModelServer.Instance.GetRoleInfo(roleId);
        if (result == null)
        {
            proto.IsSuccess = false;
            proto.MsgCode = Constant.ROLE_GET_ROLEINFO_FAIL;
        }
        else
        {
            #region 角色属性详情
            proto.IsSuccess = true;
            proto.ClassId = (byte)int.Parse(result["ClassId"]);
            proto.Attack = int.Parse(result["Attack"]);
            proto.Cri = int.Parse(result["Cri"]);
            proto.CurrHP = int.Parse(result["CurrHP"]);
            proto.CurrMP = int.Parse(result["CurrMP"]);
            proto.Defense = int.Parse(result["Defense"]);
            proto.Dodge= int.Parse(result["Dodge"]);
            proto.Equip_Belt = int.Parse(result["Equip_BeltId"]);
            //proto.Equip_BeltTableId= int.Parse(result["Equip_BeltTableId"]);
            proto.Equip_Clothes= int.Parse(result["Equip_ClothesId"]);
            //proto.Equip_ClothesTableId= int.Parse(result["Equip_ClothesTableId"]);
            proto.Equip_Cuff= int.Parse(result["Equip_CuffId"]);
            //proto.Equip_CuffTableId= int.Parse(result["Equip_CuffTableId"]);
            proto.Equip_Necklace= int.Parse(result["Equip_NecklaceId"]);
            //proto.Equip_NecklaceTableId = int.Parse(result["Equip_NecklaceTableId"]);
            proto.Equip_Pants = int.Parse(result["Equip_PantsId"]);
            //proto.Equip_PantsTableId = int.Parse(result["Equip_PantsTableId"]);
            proto.Equip_Ring = int.Parse(result["Equip_RingsId"]);
            //proto.Equip_RingTableId = int.Parse(result["Equip_RingTableId"]);
            proto.Equip_Shoe = int.Parse(result["Equip_ShoesId"]);
            //proto.Equip_ShoeTableId = int.Parse(result["Equip_ShoeTableId"]);
            proto.Equip_Weapon = int.Parse(result["Equip_WeaponId"]);
            //proto.Equip_WeaponTableId = int.Parse(result["Equip_WeaponTableId"]);
            proto.CurrEnergy = int.Parse(result["CurrEnergy"]);
            proto.MaxEnergy = int.Parse(result["MaxEnergy"]);
            proto.Exp = int.Parse(result["Exp"]);
            proto.Gold = int.Parse(result["Gold"]);
            proto.Hit = int.Parse(result["Hit"]);
            proto.LastInWorldMapId = int.Parse(result["LastInWorldMapId"]);
            proto.LastInWorldMapPos = result["LastInWorldMapPos"];
            proto.Level = int.Parse(result["Level"]);
            proto.VIPLevel = int.Parse(result["VIPLevel"]);
            proto.MaxHP = int.Parse(result["MaxHP"]);
            proto.MaxMP = int.Parse(result["MaxMP"]);
            proto.Gem = int.Parse(result["Gem"]);
            proto.Res = int.Parse(result["Res"]);
            proto.RoleNickName = result["NickName"];
            proto.SumDPS = int.Parse(result["SumDPS"]);
            proto.RoldId = int.Parse(result["Id"]);
            proto.TotalRechargeGem = int.Parse(result["TotalRechargeGem"]);
            #endregion
        }
        SocketManagerServer.Instance.SendMessageToClient(proto.ToArray());
    }
    #endregion
    public void MyDispose()
    {

        #region 登录相关
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_LoginGameServerReqProto, OnLoginGameServer);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_AddRoleReqProto, OnAddRole);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_DeleteRoleReqProto, OnDeleteRole);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_EnterGameReqProto, OnEnterGame);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_RoleInfoReqProto, OnGetRoleInfo);

        #endregion

    }
}
