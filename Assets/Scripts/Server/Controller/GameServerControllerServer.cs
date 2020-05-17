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
/// 处理区服相关控制机（模拟器）
/// </summary>
public class GameServerControllerServer : SingletonBase<GameServerControllerServer>
{
    public CallbackArgs Post(string jsonStr)
    {
        //RetValue ret = new RetValue();
        CallbackArgs result;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);

        int type = Convert.ToInt32(jsonData["Type"].ToString());

        //0:获取新区信息  1:获取区服列表    2:获取全部区服信息  3:获取指定页的区服信息（pageIndex）
        switch(type){
            case 0:
                //获取新区信息
                result = GameServerDBModelServer.Instance.GetNewGameServer(); 
                break;
            case 1:
                //获取大区列表
                result = GameServerDBModelServer.Instance.GetGameServerList();
                break;
            case 2:
                //获取全部大区信息
                result = GameServerDBModelServer.Instance.GetGameServerInfos();
                break;
            case 3:
                //获取指定大区信息
                result = null;
                break;
            case 4:
                //更新最后登录的信息
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
