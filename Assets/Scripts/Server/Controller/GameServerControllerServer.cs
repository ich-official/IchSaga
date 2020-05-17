//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 23:08:25
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using LitJson;
using System;

/// <summary>
/// ����������ؿ��ƻ���ģ������
/// </summary>
public class GameServerControllerServer : SingletonBase<GameServerControllerServer>
{
    public CallbackArgs Post(string jsonStr)
    {
        //RetValue ret = new RetValue();
        CallbackArgs result;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);

        int type = Convert.ToInt32(jsonData["Type"].ToString());

        //0:��ȡ������Ϣ  1:��ȡ�����б�    2:��ȡȫ��������Ϣ  3:��ȡָ��ҳ��������Ϣ��pageIndex��
        switch(type){
            case 0:
                //��ȡ������Ϣ
                result = GameServerDBModelServer.Instance.GetNewGameServer(); 
                break;
            case 1:
                //��ȡ�����б�
                result = GameServerDBModelServer.Instance.GetGameServerList();
                break;
            case 2:
                //��ȡȫ��������Ϣ
                result = GameServerDBModelServer.Instance.GetGameServerInfos();
                break;
            case 3:
                //��ȡָ��������Ϣ
                result = null;
                break;
            case 4:
                //��������¼����Ϣ
                int Id = Convert.ToInt32(jsonData["Id"].ToString());
                int lastServerId = Convert.ToInt32(jsonData["lastLoginServerId"].ToString());
                string lastServerName =jsonData["lastLoginServerName"].ToString();
                result = GameServerDBModelServer.Instance.UpdateLastLoginInfo(Id,lastServerId,lastServerName);
                break;
            default:
                result = null;
                break;
        }
        return result;
    }

}
