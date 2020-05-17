using UnityEngine;
using System.Collections;
using LitJson;
using System.Net.Sockets;
using System;
using System.Text;

public class Leo_NetworkTest : MonoBehaviour {

   // private GameObject box;
    // public GameObject uiroot;
    Socket s;
    private byte[] mReceiveBuffer = new byte[10240];


    #region get请求测试

    #endregion
    void Awake()
    {
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.TEST, OnTestCallback);
        //box = Resources.Load("LeoPrefabs/box") as GameObject;
        //uiroot= GameObject.Find("box111");
    }
    void OnDestroy()
    {
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.RoleOperation_Login, OnTestCallback);
    }
    public void OnTestCallback(byte[] protoContent)
    {
        Leo_ProtoTest test= Leo_ProtoTest.ToObject(protoContent);
        Debug.Log("client:已解析数据");
        Debug.Log(test.ID);
        Debug.Log(test.name);
        Debug.Log(test.price);
    }
	void Start ()
    {
        #region http连接测试
        /*
        if (!Leo_HttpManager.Instance.IsRequested)
        {
            //Leo_HttpManager.Instance.SendMessages("", "", false, GetCallback);
            JsonData js = new JsonData();
            js["Type"] = 0; //暂时定义 0=注册 1=登陆
            js["Username"] = "111";
            js["Pwd"] = "111";
            Leo_HttpManager.Instance.SendMessages(@"http://127.0.0.1:8080/LeoGameServer/index.jsp", js.ToJson(), true, PostCallback);
        }
         * */
        #endregion

        #region socket连接测试
        /*
#if SINGLE_MODE     //如果是单机模式，就调用本地服务器模拟器的方法进行模拟网络传输
        Leo_ServerSimulator.Instance.Connect();
        using (Leo_MemoryStream stream = new Leo_MemoryStream())
        {
            stream.WriteUTF8String("hello_world");
            Debug.Log("模拟发送消息开始");
            byte[] data1=Leo_SocketManager.Instance.MakeDataPkg(stream.ToArray());
            stream.Position = 0;
            stream.SetLength(0);
            stream.WriteUTF8String("server received,hello user");
            byte[] data2=Leo_SocketManager.Instance.MakeDataPkg(stream.ToArray());
            Leo_ServerSimulator.Instance.SendMessageToServer(data1, data2);
        }

        
#else
        Leo_SocketManager.Instance.Connect("127.0.0.1", 8888);

        using (Leo_MemoryStream stream = new Leo_MemoryStream())
        {
            stream.WriteUTF8String("hello_world");
            Debug.Log("发送消息开始");
            Leo_SocketManager.Instance.SendMessageToQueue(stream.ToArray());
        }
#endif
        
        s=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        byte[] b = System.Text.Encoding.Default.GetBytes("hello world!!");
        s.Connect("127.0.0.1", 8888);
        s.BeginSend(b, 0, b.Length, SocketFlags.None, SendDataCallback, s);
        */
        
        #endregion

        #region 协议测试
        /*
#if SINGLE_MODE
        Leo_ProtoTest test = new Leo_ProtoTest();
        test.protoCode = Leo_ProtoCodeDefine.RoleOperation_Login;
        test.ID = 1001;
        test.name = "test";
        test.price = 1.88f;
        using (Leo_MemoryStream stream = new Leo_MemoryStream())
        {
            stream.WriteUTF8String("ok test ok");
            byte[] receive=Leo_SocketManager.Instance.MakeDataPkg(stream.ToArray());
            byte[] send=Leo_SocketManager.Instance.MakeDataPkg(test.ToArray());
            Leo_ServerSimulator.Instance.SendMessageToServer(send, receive);
        }
#endif   
         * */
        #endregion

        #region 加密协议测试
#if SINGLE_MODE
        Leo_ProtoTest test = new Leo_ProtoTest();
        test.protoCode = ProtoCodeDefine.TEST;
        test.ID = 1001;
        test.name = "test";
        test.price = 1.88f;
        using (MemoryStreamUtil stream = new MemoryStreamUtil())
        {
            //byte[] receive = Leo_SocketManager.Instance.MakeDataPkg(stream.ToArray());
            byte[] send = SocketManager.Instance.MakeDataPkg(test.ToArray());
            SocketSimulator.Instance.SendMessageToServer(send);
        }
#endif

        #endregion 
    }
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(box);
            box.transform.position = Vector3.zero;
            box.transform.parent = uiroot.transform;
        }
         * */

    }
    
    void SendDataCallback(IAsyncResult result)
    {
        Debug.Log("ok");
        s.BeginReceive(mReceiveBuffer, 0, mReceiveBuffer.Length, SocketFlags.None, ReceiveCallback, s);

    }

    // Update is called once per frame


    void ReceiveCallback(IAsyncResult result)
    {
        while (true)
        {
            int len = s.EndReceive(result);
            if (len > 0)
            {
                Debug.Log("receive msg:" + Encoding.ASCII.GetString(mReceiveBuffer).Trim());
            }
            break;
           
        }
        
    }

    #region HTTP测试用例
    //测试回调
    private void GetCallback(CallbackArgs args)
    {
        AccountEntityServer account = LitJson.JsonMapper.ToObject<AccountEntityServer>(args.json);

    }

    private void PostCallback(CallbackArgs args)
    {
        if (args.hasError)
        {
            Debug.Log(args.errorMsg);

        }
        else
        {
            //Leo_ReturnValues 这个里面的属性都要和服务器上的一一对应
            ReturnValues ret = JsonMapper.ToObject<ReturnValues>(args.json);
            Debug.Log(ret.hasError);
        }

    }
    #endregion
}
