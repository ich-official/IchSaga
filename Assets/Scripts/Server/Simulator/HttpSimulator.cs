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
/// http���䷽ʽģ��������������ú�HttpManager��һ���ģ�ͬΪ�ͻ����ࣩ��Ϊ�������֣��Ե���дһ����
/// </summary>
public class HttpSimulator : SimulatorBase<HttpSimulator>
{
    private bool isRequested = false;   //�����Ƿ��Ѿ�����
    public delegate void mHttpCallbackSingle(CallbackArgs args);
    public mHttpCallbackSingle CallbackSingle;  //ģ����������Ϣʱִ�д�ί��


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
            case ServerAPI.Account://��½��ע��
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
            case ServerAPI.Time://����ʱ�����
                CallbackArgs argsTime = AccountControllerServer.Instance.Post(json);
                break;
        }
    }
    public void RequestSingle(CallbackArgs args)
    {
        isRequested = false;//ģ�������������ݣ�һ�μ����������
        CallbackSingle(args);   //ģ�������صĶ���ֱ��ʹ�ã��ȳ��淽������
    }
}

/// <summary>
/// ģ��web������������ݣ�������������ʾ
/// </summary>
public class CallbackArgsFake
{
    public bool hasError;//�Ƿ񱨴�
    public int errorCode;//������
    public string errorMsg;//������Ϣ
    public string json;//���������ص�json��
}

public enum ServerAPI
{
    Account,
    GameServer,
    Recharge,
    AppleRecharge,
    Time
}
