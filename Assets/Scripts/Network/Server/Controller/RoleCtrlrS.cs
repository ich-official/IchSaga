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
/// socketЭ�鷽ʽ���˺��н�ɫ��ص�controller������RoleController��
/// </summary>
public class RoleCtrlrS : SingletonBase<RoleCtrlrS> ,IDisposable{
    /// <summary>
    /// ��ʼ���˺�-��ɫ���
    /// </summary>
    public void Init()
    {
        //���Ӽ���
        #region ��¼���
        //�ͻ��˷��͵�¼������Ϣ
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_LoginGameServerReqProto, OnLoginGameServer);

        //�ͻ��˷��ʹ�����ɫ��Ϣ
        EventDispatcherS.Instance.AddEventListener(ProtoCodeDefine.Account_AddRoleReqProto, OnAddRole);

        //�ͻ��˷���ɾ����ɫ��Ϣ
        EventDispatcherS.Instance.AddEventListener(ProtoCodeDefine.Account_DeleteRoleReqProto, OnDeleteRole);

        //�ͻ��˷��ͽ�����Ϸ��Ϣ
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
    /// ��½ʱ�������Ĳ���
    /// </summary>
    /// <param name="role"></param>
    /// <param name="buffer"></param>
    private void OnLoginGameServer(byte[] buffer)
    {
        Account_LoginGameServerReqProto proto = Account_LoginGameServerReqProto.GetProto(buffer);

        //����ʺ�
        int accountId = proto.AccountId;

        //role.AccountId = accountId;
        LoginGameServerResp(accountId);
    }
    /// <summary>
    /// �������������ѿͻ��˴�����accountID��ѯ��Ӧ��RoleID���������role���󷵻ظ��ͻ���
    /// </summary>
    /// <param name="role"></param>
    /// <param name="accountId"></param>
    private void LoginGameServerResp(int accountId)
    {
        Account_LoginGameServerRespProto proto = new Account_LoginGameServerRespProto();
        //TODO �ⲽ�ǵ���CacheModel��ȥ���ݿ���ɫ������һ����ɫʵ���б������ĳ����Լ����߼�    ��select * from Role where [AccountId]=accountId��

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

        //���ͻ��˷���Ϣ�������ĳ����Լ��ķ�ʽ
        SocketManagerServer.Instance.SendMessageToClient(proto.ToArray());
        //role.Client_Socket.SendMsg(proto.ToArray());
    }
    
    public override void Dispose()
    {
        //�Ƴ�����
    }
}
