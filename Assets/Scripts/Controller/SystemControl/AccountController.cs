﻿//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
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
/// 登陆时处理账号相关的功能写在这里，controller的一部分
/// </summary>
public class AccountController : ControllerBase<AccountController>, ISystemController
{
    private UIPanelLoginView mLoginView;
    private UIPanelRegView mRegView;

    public AccountController()
    {
        AddEventListener(Constant.UIPanelLoginView_LoginButton, OnLoginButtonClick);
        AddEventListener(Constant.UIPanelLoginView_GotoRegButton, OnGotoRegButtonClick);
        AddEventListener(Constant.UIPanelRegView_ConfirmButton, OnRegConfirmButtonClick);
        AddEventListener(Constant.UIPanelRegView_ReturnButton, OnRegReturnButtonClick);

    }
    //在UIViewManager里调用此方法，然后在此方法中switch选择具体打开哪个view
    public void OpenView(UIWindowType windowType)
    {
        switch (windowType)
        {
            case UIWindowType.LOGIN:
                OpenLoginView();
                break;
            case UIWindowType.REG:
                OpenRegView();
                break;
        }
    }

    //打开登陆页面
    public void OpenLoginView()
    {
        mLoginView = Leo_UIWindowManager.Instance.OpenWindowUI(UIWindowType.LOGIN, true).GetComponent<UIPanelLoginView>();
        mLoginView.OnViewClose = () => { OpenRegView(); };
    
    }
    //打开注册页面
    public void OpenRegView()
    {
        mRegView = Leo_UIWindowManager.Instance.OpenWindowUI(UIWindowType.REG, true).GetComponent<UIPanelRegView>();
        mRegView.OnViewClose = () => { OpenLoginView(); };
    }

    //登陆游戏，进入主城
    public void OnLoginButtonClick(params object[] obj)
    {
        //这里把帐密传给服务器，验证账号并跳转场景，进入主城
        LogUtil.LogTest("login Click");
        Dictionary<string, object> dic = new Dictionary<string, object>();//把注册信息保存在字典中
        dic["Type"] = 1;
        dic["UserName"] = mLoginView.Username.text;
        dic["Pwd"] = mLoginView.Pwd.text;
        if(GlobalInit.isSingleMode){
            HttpSimulator.Instance.DoPostSingle(ServerAPI.Account, JsonMapper.ToJson(dic), OnLoginCallback);
        }
        else{
            dic["cdId"] = "test";//设备ID
            dic["sign"] = SecurityUtil.Md5(string.Format("{0}:{1}", dic["cdId"],dic["t"]));//签名
            dic["t"] = GlobalInit.Instance.CurrentTimeStamp; //时间戳
            HttpManager.Instance.SendMessages("127.0.0.1/api/account", JsonMapper.ToJson(dic), true, OnLoginCallback);
        }
    }

    private void OnLoginCallback(CallbackArgs args)
    {
        if (args.hasError)
        {

            ShowDialog(args.errorCode + ":" + args.errorMsg, DialogType.OK, okAction: () =>
            {
            //这是一个匿名委托，类似java匿名内部类、匿名方法一样
                Debug.Log("OK按钮回调，模拟器报错：" + args.errorCode + args.errorMsg);
            });
        }
        else
        {
            Debug.Log("模拟器返回消息：" + args.json);
            SceneManager.Instance.LoadMainScene();

        }
    }

    public void OnGotoRegButtonClick(params object[] obj)
    {
        Debug.Log("gotoReg Click");
        mLoginView.SelfClose(true);

    }
    //执行注册，把账号传回服务器保存
    public void OnRegConfirmButtonClick(params object[] obj)
    {
        //这里把帐密传给服务器，等待服务器返回注册信息
        //这里添加了单机逻辑，
        Debug.Log("reg confirm");
#if SINGLE_MODE
        Dictionary<string,object> dic=new Dictionary<string,object>();//把注册信息保存在字典中
        dic["Type"] = 0;
        dic["UserName"] = mRegView.Username.text;
        dic["Pwd"] = mRegView.Pwd.text;
        dic["ChannelId"] = 1;
        HttpSimulator.Instance.DoPostSingle(ServerAPI.Account,JsonMapper.ToJson(dic), OnRegCallback);
#else
        Leo_HttpManager.Instance.SendMessages("127.0.0.1/api/account", "", true, OnRegCallback);

#endif
    }
    private void OnRegCallback(CallbackArgs args)
    {
        if (args.hasError)
        {
            Debug.Log("模拟器报错：" + args.errorCode+args.errorMsg);
            DialogController.Instance.Show(args.errorCode + ":" + args.errorMsg);
        }
        else
        {
            Debug.Log("模拟器返回消息：" + args.json);
        }
    }


    public void OnRegReturnButtonClick(params object[] obj)
    {
        Debug.Log("reg return");
        mRegView.SelfClose(true);


    }
    public override void Dispose()
    {
        base.Dispose();
        RemoveEventListener(Constant.UIPanelLoginView_LoginButton, OnLoginButtonClick);
        RemoveEventListener(Constant.UIPanelLoginView_GotoRegButton, OnGotoRegButtonClick);
        RemoveEventListener(Constant.UIPanelRegView_ConfirmButton, OnRegConfirmButtonClick);
        RemoveEventListener(Constant.UIPanelRegView_ReturnButton, OnRegReturnButtonClick);

    }
}
