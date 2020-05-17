//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 23:03:09
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

/// <summary>
/// 调试serverAPI用的
/// </summary>
public class HttpDebug : MonoBehaviour {


	void Start () {
        GameServerDebug();
    }

    #region GameServerDebug
    public void GameServerDebug()
    {
        //Type：0,获取最新大区  1，获取区服列表  2，获取全部区服信息
        Dictionary<string, object> dic = new Dictionary<string, object>();//把注册信息保存在字典中
        dic["Type"] = 1;    
        HttpSimulator.Instance.DoPostSingle(ServerAPI.GameServer, JsonMapper.ToJson(dic), OnHttpDebugCallback);
        
        
    }
    #endregion

    private void OnHttpDebugCallback(CallbackArgs args)
    {
        if (args.hasError)
        {
            LogUtil.Log(args.errorMsg);
        }
        else
        {
            LogUtil.Log(args.json);
            for (int i = 0; i < args.objList.Count; i++)
            {
                GameServerListEntity entity = args.objList[i] as GameServerListEntity;
                LogUtil.Log(entity.PageIndex + "," + entity.Name );

            }

        }
    }
	

	void Update () {
	
	}

    void OnDestroy()
    {
        SqliteHelper.Instance.Disconnect();
    }
}
