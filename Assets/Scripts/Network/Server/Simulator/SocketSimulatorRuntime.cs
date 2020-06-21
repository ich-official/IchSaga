//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
/// <summary>
/// 单机模式下专用，用于模拟服务器端派发协议的业务逻辑操作
/// 
/// </summary>
public class SocketSimulatorRuntime : MonoBehaviour
{
#if SINGLE_MODE
    #region 注册和解除监听
    void Awake()
    {
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.TEST, OnTestCallback);
    }

    void OnDestroy()
    {
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.TEST, OnTestCallback);

    }
    #endregion

    #region 各类协议的回调函数
    //测试，证明整个流程已跑通
    public void OnTestCallback(byte[] protoContent)
    {
        Leo_ProtoTest test = Leo_ProtoTest.ToObject(protoContent);
        Debug.Log("server:已收到数据");
        Debug.Log(test.ID);
        Debug.Log(test.name);
        Debug.Log(test.price);
        Leo_ProtoTest test2 = new Leo_ProtoTest();
        test2.protoCode = ProtoCodeDefine.TEST;
        test2.ID = 9999;
        test2.name = "server msg";
        test2.price = 999f;
        byte[] returnPkg=SocketManager.Instance.MakeDataPkg(test2.ToArray());
        SocketManagerServer.Instance.SendMessageToClient(returnPkg);
    }
    #endregion
    void Start () {
	
	}
	
	void Update () {
	
	}
#endif
}
