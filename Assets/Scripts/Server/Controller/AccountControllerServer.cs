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

/// <summary>
/// 
/// </summary>
public class AccountControllerServer : SingletonBase<AccountControllerServer>
{
    public CallbackArgs Post(string jsonStr)
    {
        //RetValue ret = new RetValue();
        CallbackArgs result;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);

        //����ģʽ��ʡ��������֤����
        //long t = Convert.ToInt64(jsonData["t"].ToString());
        //string deviceIdentifier = jsonData["deviceIdentifier"].ToString();
        //string deviceModel = jsonData["deviceModel"].ToString();
        //string sign = jsonData["sign"].ToString();

        //1.�ж�ʱ��� �������3�� ֱ�ӷ��ش���
        //DoVerifyTimestamp();

        //2.��֤ǩ��
        //DoVerifySign();


        int type = Convert.ToInt32(jsonData["Type"].ToString());
        string userName = jsonData["UserName"].ToString();
        string pwd = jsonData["Pwd"].ToString();
        //0:ע��  1:��½
        if (type == 0)
        {
            string channelId = jsonData["ChannelId"].ToString();

            //ע��
            result=AccountDBModelServer.Instance.Register(userName, pwd, channelId);


        }
        else
        {
            //��¼
            result = AccountDBModelServer.Instance.Login(userName, pwd);


        }
        return result;
    }
}
