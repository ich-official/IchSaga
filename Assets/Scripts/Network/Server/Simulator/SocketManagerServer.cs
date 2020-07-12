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
using System.Text;

/// <summary>
/// 服务器模拟器，模拟socket方式服务器处理数据的逻辑，单机模式下使用
/// 该类的功能有：客户端给服务器发送数据包，服务器给客户端返回数据包
/// </summary>
public class SocketManagerServer : SimulatorBase<SocketManagerServer>
{
    #region socket连接+服务器操作包完全模拟，最后返回的数据包走客户端正式处理流程
    MemoryStreamUtil mReceiveStream = new MemoryStreamUtil();
    /*
    public void Connect()
    {
        Debug.Log("connect success:127.0.0.1");
    }
    */
    #region SendMessageToServer客户端模拟socket通信向服务器发送数据
    

    #endregion 

    #region SendMessageToClient 服务器模拟socket通信向客户端返回数据的方法
    /// <summary>
    /// 服务器返回数据给客户端
    /// </summary>
    /// <param name="returnPkg">服务器返回的原始数据，需要经过makePkg后才能返回给客户端</param>
    public void SendMessageToClient(byte[] returnPkg)
    {
        byte[] receive = SocketManager.Instance.MakeDataPkg(returnPkg);
        SocketManager.Instance.ReceiveCallbackSingle(receive);
    }
    #endregion 
    
    #region 服务器处理数据核心逻辑，服务器接收数据包
    /// <summary>
    /// 模拟要点：解客户端数据包，同时向正式代码模拟发送服务端数据包
    /// 41课以后包体结构更复杂，包体=协议编号+协议内容
    /// 43课以后数据包完全体为：包体=压缩标志+CRC校验码+异或算法后的真实数据(协议编号+协议内容)
    /// </summary>
    /// <param name="dataPkg">本地向服务器传什么数据</param>
    /// <param name="isDIYReturnMsg">是否由客户端决定服务器返回什么数据，默认为false，就是由服务端通过逻辑处理后返回一个伪真实的数据，如果为true，就是一个由客户端决定的纯伪造的数据</param>
    /// <param name="returnPkg">希望服务器向本地传什么数据</param>
    public void ServerOperator(byte[] dataPkg,bool isDIYReturnPkg=false)
    {
        if (isDIYReturnPkg == true) {//向客户端返回的是客户端指定的数据包，不必再做下列处理
            return; 
        }
        mReceiveStream.Position = mReceiveStream.Length;
        mReceiveStream.Write(dataPkg, 0, dataPkg.Length); //数据写进内存，等待操作
        mReceiveStream.Position = 0;
        int currentMsgLen = mReceiveStream.ReadUShort();    //包体长度
        int currentFullLen = 2 + currentMsgLen; //包头+包体长度
        if (mReceiveStream.Length >= currentFullLen)    //接收的数据包大于一个完整包的长度，表示至少有一个完整包传过来了
        {
            byte[] buffer = new byte[currentMsgLen];    //此buffer为数据包，包体内容
            mReceiveStream.Position = 2;        //把包头去掉，剩下包体
            mReceiveStream.Read(buffer, 0, currentMsgLen);//把数据读到buffer中
            #region 43课以后完整版数据包增加以下内容
            ushort CRC = 0;     //CRC校验码
            bool isCompress = false;    //压缩标志
            byte[] realDataBuffer = new byte[buffer.Length - 3];    //刨除CRC和压缩标志，真正数据部分
            using (MemoryStreamUtil stream = new MemoryStreamUtil(buffer))
            {
                isCompress = stream.ReadBool(); //获取压缩标志
                CRC=stream.ReadUShort();    //获取CRC校验码
                stream.Read(realDataBuffer, 0, realDataBuffer.Length);
            }
            ushort newCRC = Crc16.CalculateCrc16(realDataBuffer); //开始校验CRC
            if (CRC == newCRC)  //校验通过
            {
                if (isCompress) realDataBuffer = ZlibHelper.DeCompressBytes(realDataBuffer);    //解压
                realDataBuffer = SecurityUtil.XorAlgorithm(realDataBuffer);//解密，此时realDataBuffer是可以操作的数据
            }
            else
            {
                Debug.Log("server:CRC verify failed!");
                return;
            }

            #endregion
            #region 41课以后服务器增加以下内容
            ushort protoCode = 0;
            byte[] protoContent = new byte[realDataBuffer.Length - 2];  //可操作数据buffer减去协议编号部分
            #endregion
            using (MemoryStreamUtil stream = new MemoryStreamUtil(realDataBuffer))
            {
                protoCode = stream.ReadUShort();
                stream.Read(protoContent, 0, protoContent.Length);
                SocketDispatcher.Instance.Dispatch(protoCode, protoContent);
                //EventDispatcherS.Instance.Dispatch(protoCode, null, protoContent);
            }
            //41课以后使用网络协议，增加此部分判断逻辑
            //43课以后使用观察者模式
            //此处作为模拟器不使用观察者模式，采用模拟数据的方式简易处理
            /*
            if (protoCode == Leo_ProtoCodeDefine.RoleOperation_Login)
            {
                Leo_ProtoTest test = Leo_ProtoTest.ToObject(protoContent);
                Debug.Log("服务器解析协议成功，协议编号：" + protoCode);
                Debug.Log("ID:" + test.ID);
                Debug.Log("name:" + test.name);
                Leo_ProtoTest testReturn = new Leo_ProtoTest();
                testReturn.protoCode = Leo_ProtoCodeDefine.RoleOperation_Login;
                testReturn.ID = 2002;
                testReturn.name = "return";
                testReturn.price = 2.22f;
                byte[] returnBuffer = Leo_SocketManager.Instance.MakeDataPkg(testReturn.ToArray()); //模拟服务器向客户端返回一条协议数据
            }
             * */
            //Leo_SocketDispatcher.Instance.Dispatch(protoCode, protoContent);
            
        }
        else
        {
            Debug.Log("服务器解析数据包错误！");
        }
        //2020-06-25新增，一次接收结束后把stream里的内容清空，保证下次接收的内容是新的
        mReceiveStream.Position = 0;
        mReceiveStream.SetLength(0);
    }
    #endregion 



    int reconnectCount = 3; //模拟断线重连的次数
    public void Disconnect(bool isReconnect=false)
    {

    }
    #endregion 
}