//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-22 12:26:38
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
/// http方式，服务器端账户controller
/// </summary>
public class AccountCtrlrS : SingletonBase<AccountCtrlrS>
{
    public CallbackArgs Post(string jsonStr)
    {
        //RetValue ret = new RetValue();
        CallbackArgs result;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        //单机模式下省略以下验证过程
        //long t = Convert.ToInt64(jsonData["t"].ToString());
        //string deviceIdentifier = jsonData["deviceIdentifier"].ToString();
        //string deviceModel = jsonData["deviceModel"].ToString();
        //string sign = jsonData["sign"].ToString();

        //1.判断时间戳 如果大于3秒 直接返回错误
        //DoVerifyTimestamp();

        //2.验证签名
        //DoVerifySign();


        int type = Convert.ToInt32(jsonData["Type"].ToString());
        string userName = jsonData["UserName"].ToString();
        string pwd = jsonData["Pwd"].ToString();
        conditions.Add("Username", jsonData["UserName"].ToString());
        conditions.Add("Pwd", jsonData["Pwd"].ToString());
        //0:注册  1:登陆
        if (type == 0)
        {
            string channelId = jsonData["ChannelId"].ToString();
            conditions.Add("ChannelId", jsonData["ChannelId"].ToString());
            //注册
            //result =AccountDBModelServer.Instance.Register(userName, pwd, channelId);
            result =AccountDBModelServer.Instance.Register(conditions);


        }
        else
        {
            //登录
            //result = AccountDBModelServer.Instance.Login(userName, pwd);  //sqlite方式
            result = AccountDBModelServer.Instance.Login(conditions);       //xml方式


        }
        return result;
    }
}
