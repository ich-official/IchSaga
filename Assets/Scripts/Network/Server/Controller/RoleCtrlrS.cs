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
        EventDispatcherS.Instance.AddEventListener(ProtoCodeDefine.Account_AddRoleReqProto, OnAddRole);

        //客户端发送删除角色消息
        EventDispatcherS.Instance.AddEventListener(ProtoCodeDefine.Account_DeleteRoleReqProto, OnDeleteRole);

        //客户端发送进入游戏消息
        EventDispatcherS.Instance.AddEventListener(ProtoCodeDefine.Account_EnterGameReqProto, OnEnterGame);
        #endregion
    }

    private void OnEnterGame(RoleS role, byte[] buffer)
    {
        throw new NotImplementedException();
    }

    private void OnDeleteRole(RoleS role, byte[] buffer)
    {
        throw new NotImplementedException();
    }

    private void OnAddRole(RoleS role, byte[] buffer)
    {
        throw new NotImplementedException();
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

        //role.AccountId = accountId;
        LoginGameServerResp(accountId);
    }
    /// <summary>
    /// 服务器操作，把客户端传来的accountID查询对应的RoleID，并把这个role对象返回给客户端
    /// </summary>
    /// <param name="role"></param>
    /// <param name="accountId"></param>
    private void LoginGameServerResp(int accountId)
    {
        Account_LoginGameServerRespProto proto = new Account_LoginGameServerRespProto();
        //TODO 这步是调用CacheModel层去数据库查角色，返回一个角色实体列表，后续改成我自己的逻辑    （select * from Role where [AccountId]=accountId）

        List<RoleEntity> lst=RoleDBModelServer.Instance.GetRoles(accountId);
        //List<RoleEntity> lst = RoleCacheModel.Instance.GetList(condition: string.Format("[AccountId]={0}", accountId));

        if (lst != null && lst.Count > 0)
        {
            proto.RoleCount = lst.Count;
            proto.RoleList = new List<Account_LoginGameServerRespProto.RoleItem>();

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

        //给客户端发消息，后续改成我自己的方式
        SocketManagerServer.Instance.SendMessageToClient(proto.ToArray());
        //role.Client_Socket.SendMsg(proto.ToArray());
    }
    
    public override void Dispose()
    {
        //移除监听
    }
}
