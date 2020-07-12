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
        #endregion
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
        LoginGameServerResp(accountId,gameServerId);
    }
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

    private void AddRoleServerResp(int classId,string nickName,int gameServerId)
    {
        Account_AddRoleRespProto proto = new Account_AddRoleRespProto();
        bool isSuccess = RoleDBModelServer.Instance.AddRole(classId,nickName,gameServerId);
        proto.IsSuccess = isSuccess;
        if (isSuccess == false) proto.MsgCode = Constant.ROLE_ADD_FAIL;
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

    public void MyDispose()
    {

        #region 登录相关
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_LoginGameServerReqProto, OnLoginGameServer);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_AddRoleReqProto, OnAddRole);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_DeleteRoleReqProto, OnDeleteRole);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_EnterGameReqProto, OnEnterGame);
        #endregion

    }
}
