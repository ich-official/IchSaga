//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-21 22:24:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// http传输方式模拟器，此类的作用和HttpManager是一样的（同为客户端类），为方便区分，仍单独写一个类
/// </summary>
public class HttpSimulator : SimulatorBase<HttpSimulator>
{
    private bool isRequested = false;   //请求是否已经发出
    public delegate void mHttpCallbackSingle(CallbackArgs args);
    public mHttpCallbackSingle CallbackSingle;  //模拟器返回信息时执行此委托


    public void DoGetSingle()
    {

    }

    public void DoPostSingle(ServerAPI api,string json, mHttpCallbackSingle Callback)
    {
        if (isRequested) return;

        isRequested = true;
        CallbackSingle = Callback;

        switch (api)
        {
            case ServerAPI.Account://登陆、注册
                CallbackArgs argsAccount = AccountControllerServer.Instance.Post(json);
                RequestSingle(argsAccount);
                break;
            case ServerAPI.GameServer:
                CallbackArgs argsGameServer = GameServerControllerServer.Instance.Post(json);
                RequestSingle(argsGameServer);
                break;
            case ServerAPI.Recharge:
                break;
            case ServerAPI.AppleRecharge:
                break;
            case ServerAPI.Time://请求时间戳、
                CallbackArgs argsTime = AccountControllerServer.Instance.Post(json);
                break;
        }
    }
    public void RequestSingle(CallbackArgs args)
    {
        isRequested = false;//模拟器返回了数据，一次假请求完成了
        CallbackSingle(args);   //模拟器返回的对象直接使用，比常规方法简易
    }
}

/// <summary>
/// 模仿web请求回来的数据，单独用类来表示
/// </summary>
public class CallbackArgsFake
{
    public bool hasError;//是否报错
    public int errorCode;//错误码
    public string errorMsg;//错误信息
    public string json;//服务器返回的json串
}

public enum ServerAPI
{
    Account,
    GameServer,
    Recharge,
    AppleRecharge,
    Time
}
