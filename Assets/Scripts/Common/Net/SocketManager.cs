//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 12:24:39
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.Collections.Generic;
using System.Text;

public class SocketManager : MonoBehaviour {

    #region  //单例，继承MonoBehaviour时的单例写法，返回的是一个组件
    private static SocketManager instance;
    public static SocketManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("SocketManager");
                DontDestroyOnLoad(obj);
                instance = obj.SafeGetComponent<SocketManager>();
            }
            return instance;
        }
    }
    #endregion

    #region 发送数据变量
    private Queue<byte[]> mSendQueue = new Queue<byte[]>(); //要传给服务器的数据包都缓存在队列中，等待队列操作
    private Action mCheckSendQueue; //委托，检查数据包队列是否有没发送的数据包

    private const int mCompressMinSize = 200;  //如果数据包比200字节大，就压缩
    private Socket client;
    #endregion

    #region 接收数据变量
    private byte[] mReceiveBuffer = new byte[10240];

    private MemoryStreamUtil mReceiveStream = new MemoryStreamUtil();

    private Queue<byte[]> mReceiveQueue = new Queue<byte[]>();

    private int mReceiveCount = 0;  //接收数据的数量
    #endregion
    void Start()
    {

    }

    //解析服务器返回的数据包，步骤如下：
    //1、把包头长度去掉  
    //2、CRC校验，
    //3、检查压缩标志，先校验CRC再解压，因为打包时的CRC就是压缩后的值 
    //4、使用异或算法得到真实包体  
    //5、通过解析协议编号，派发对应的委托
    void Update()
    {
        while (true)
        {
            if (mReceiveCount <= 3)
            {
                mReceiveCount++;
                lock (mReceiveQueue)
                {
                    if (mReceiveQueue.Count > 0)
                    {
                        byte[] buffer = mReceiveQueue.Dequeue();    //取一个服务器返回的未处理的数据包
                        byte[] dataBody = new byte[buffer.Length - 3];  //获取数据包包体，未解压（压缩标志+CRC一共3字节，包头部分在数据传过来的之前就处理掉了）
                        bool isCompress = false;    //压缩标志
                        ushort CRC = 0;     //CRC校验码
                        ushort protoCode = 0;   //协议编号

                        using (MemoryStreamUtil stream = new MemoryStreamUtil(buffer))
                        {

                            isCompress = stream.ReadBool();  
                            CRC = stream.ReadUShort();  
                            stream.Read(dataBody,0,dataBody.Length);
                        }
                        ushort newCRC = Crc16.CalculateCrc16(dataBody); 
                        if (CRC == newCRC)
                        {
                            if (isCompress) dataBody = ZlibHelper.DeCompressBytes(dataBody);    //解压
                            dataBody = SecurityUtil.XorAlgorithm(dataBody);//解密
                        }
                        else { 
                            Debug.Log("client:CRC验证失败！"); 
                            break; 
                        }
                        //开始解析原始真实数据
                        byte[] protoContent = new byte[dataBody.Length-2];   //协议内容，减去协议编号
                        using (MemoryStreamUtil stream = new MemoryStreamUtil(dataBody))
                        {
                            protoCode = stream.ReadUShort();
                            //这里开始使用观察者派发协议
                            stream.Read(protoContent, 0, protoContent.Length);
                            SocketDispatcher.Instance.Dispatch(protoCode, protoContent);
                        }

                    }
                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                mReceiveCount = 0;
                break;
            }
        }
        
        
        
    }

    public void Connect(string IP, int port)
    {
        if (client != null && client.Connected) return;
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            client.Connect(new IPEndPoint(IPAddress.Parse(IP), port));
            mCheckSendQueue = OnCheckSendQueueCallback;
            ReceiveMessage();
            Debug.Log("connect success");
        }
        catch(Exception e)
        {
            Debug.Log("connect fail");
            Debug.Log(e.Message);
        }
    }


    #region 发送数据
    /// <summary>
    /// 检查队列是否还有未发送的数据包
    /// </summary>
    private void OnCheckSendQueueCallback()
    {
        lock (mSendQueue)
        {
            if (mSendQueue.Count > 0)
            {
                Debug.Log("开始向服务器发送消息");
                SendMessageToServer(mSendQueue.Dequeue());   //队列操作API，pop一个数据
            }
        }
    }

    /// <summary>
    /// 制作一个数据包，该数据包是符合传给服务器的格式的（包头+包体）
    /// 当前工程的格式是包头：包体长度，包体：压缩标志+CRC校验码+加密的真正数据(异或算法)（协议编码+协议内容）
    /// 把private改为public，配合本机模式本地测试用
    /// </summary>
    /// <param name="data">真实数据</param>
    /// <returns></returns>
    public byte[] MakeDataPkg(byte[] data)
    {
        byte[] returnBuffer = null;
        data = SecurityUtil.XorAlgorithm(data); //1、加密
        bool isCompress = data.Length > mCompressMinSize;   //2、压缩
        if (isCompress)
        {
            //开始压缩
            data = ZlibHelper.CompressBytes(data);
        }
        ushort crc16 = Crc16.CalculateCrc16(data);  //3、CRC校验

        using(MemoryStreamUtil stream=new MemoryStreamUtil()){
            stream.WriteUShort((ushort)(data.Length+3));    //写包头，+3是因为多了一个bool，一个ushort，一共3字节
            stream.WriteBool(isCompress);   //写压缩标志
            stream.WriteUShort(crc16);       //写CRC
            stream.Write(data, 0, data.Length); //写加密后的真实数据
            returnBuffer = stream.ToArray();
        }
        Debug.Log("数据包构建完毕！");
        return returnBuffer;
    }
    /// <summary>
    /// 把数据包加入队列，等待队列操作
    /// </summary>
    /// <param name="msg"></param>
    public void SendMessageToQueue(byte[] msg)
    {
        //把数据做成符合服务器格式的数据包
        byte[] sendData = MakeDataPkg(msg);
        //加入队列时，先锁定防止操作出错
        lock (mSendQueue)
        {
            //把数据包加入队列，等待队列操作
            mSendQueue.Enqueue(sendData);
            Debug.Log("数据加入队列完毕");
            mCheckSendQueue.BeginInvoke(null, null);
        }

    }

    /// <summary>
    /// 把数据包真正传给服务器
    /// </summary>
    public void SendMessageToServer(byte[] dataPkg)
    {
        client.BeginSend(dataPkg, 0, dataPkg.Length, SocketFlags.None, SendDataCallback, client);
        Debug.Log("消息已发送");
    }

    //给服务器传数据包后的回调方法
    public void SendDataCallback(IAsyncResult result)
    {
        Debug.Log("已收到服务器回调");
        //服务器返回回调后，本次send结束，
        client.EndSend(result);
        //继续判断队列里是否还有没发送的数据包
        OnCheckSendQueueCallback();
    }

    #endregion 

    #region 接收数据
    private void ReceiveMessage()
    {
        client.BeginReceive(mReceiveBuffer, 0, mReceiveBuffer.Length, SocketFlags.None, ReceiveCallback, client);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int len = client.EndReceive(result);
            if (len > 0)    //接收到数据了
            {
                mReceiveStream.Position = mReceiveStream.Length;
                mReceiveStream.Write(mReceiveBuffer, 0, mReceiveBuffer.Length); //数据写进内存，等待操作
                //根据我们自定义数据包的格式，大于2字节代表有一个包传过来了
                if (mReceiveStream.Length > 2)
                {
                    while (true)
                    {
                        mReceiveStream.Position = 0;
                        int currentMsgLen = mReceiveStream.ReadUShort();    //包体长度
                        int currentFullLen = 2 + currentMsgLen; //包头+包体长度
                        if (mReceiveStream.Length >= currentFullLen)    //接收的数据包大于一个完整包的长度，表示至少有一个完整包传过来了
                        {
                            byte[] buffer = new byte[currentMsgLen];    //此buffer为真实数据

                            mReceiveStream.Position = 2;        //根据我们自定义的数据包格式，刨除前2位，后面的是真实数据
                            mReceiveStream.Read(buffer, 0, currentMsgLen);
                            lock (mReceiveQueue)
                            {
                                mReceiveQueue.Enqueue(buffer);      //加入接收队列中
                            }
                            
                            using (MemoryStreamUtil stream = new MemoryStreamUtil())
                            {
                                string data = stream.ReadUTF8String();  //
                                Debug.Log("data:" + data);
                            }

                            //上面是一个完整数据包处理过程，下面是本次接收多余的数据进行处理
                            int remainLen = (int)mReceiveStream.Length - currentFullLen;
                            if (remainLen > 0)  //本次接收的数据大于一个完整包时
                            {
                                mReceiveStream.Position = currentFullLen;
                                byte[] remainBuffer = new byte[remainLen];
                                mReceiveStream.Read(remainBuffer, 0, remainLen);    //把剩余所有数据读进内存

                                //把流中数据清空
                                mReceiveStream.Position = 0;
                                mReceiveStream.SetLength(0);

                                mReceiveStream.Write(remainBuffer, 0, remainBuffer.Length);
                                remainBuffer = null;
                            }
                            else
                            {
                                //把流中数据清空
                                mReceiveStream.Position = 0;
                                mReceiveStream.SetLength(0);
                                break;
                            }
                        }
                        else
                        {
                            //还没收到完整包
                            break;
                        }
                    }
                }
                ReceiveMessage();
            }
            else
            {
                Debug.Log("断开连接else");
            }
        }catch(Exception e){
            Debug.Log(e.Message);
            Debug.Log("断开连接catch");
        }
    }

    /// <summary>
    /// 单机模式下模拟服务器返回数据
    /// </summary>
    /// <param name="fakeResult"></param>
    public void ReceiveCallbackSingle(byte[] fakeResult)
    {
        mReceiveStream.Position = mReceiveStream.Length;
        mReceiveStream.Write(fakeResult, 0, fakeResult.Length); //数据写进内存，等待操作
        //根据我们自定义数据包的格式，大于2字节代表有一个包传过来了
        if (mReceiveStream.Length > 2)
        {
            mReceiveStream.Position = 0;
            int currentMsgLen = mReceiveStream.ReadUShort();    //包体长度
            int currentFullLen = 2 + currentMsgLen; //包头+包体长度
            if (mReceiveStream.Length >= currentFullLen)    //接收的数据包大于一个完整包的长度，表示至少有一个完整包传过来了
            {
                byte[] buffer = new byte[currentMsgLen];    //此buffer为真实数据
                mReceiveStream.Position = 2;        //根据我们自定义的数据包格式，刨除前2位，后面的是真实数据
                mReceiveStream.Read(buffer, 0, currentMsgLen);
                lock (mReceiveQueue)
                {
                    mReceiveQueue.Enqueue(buffer);      //把原始数据包加入队列中
                }
            }
            else { }
        }
    }
    #endregion 
    void OnDestroy()
    {
        if (client != null && client.Connected)
        {
            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
    }
}
