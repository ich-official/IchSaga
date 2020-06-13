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
using System.Collections.Generic;

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
                //result = GameServerDBModelServer.Instance.GetNewGameServer(); 
                result = null;  //�˷�����ʱ�ò���
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
                string Id = jsonData["Id"].ToString();
                string lastServerId = jsonData["lastLoginServerId"].ToString();
                string lastServerName = jsonData["lastLoginServerName"].ToString();
                //Dictionary<string, string> dic = new Dictionary<string, string>();
                //dic.Add("Id", jsonData["Id"].ToString());
                //dic.Add("lastLoginServerId", jsonData["lastLoginServerId"].ToString());
                //dic.Add("lastLoginServerName", jsonData["lastLoginServerName"].ToString());
                result = GameServerDBModelServer.Instance.UpdateLastLoginInfo(Id, lastServerId,lastServerName);
                break;
            default:
                result = null;
                break;
        }
        return result;
    }

}
