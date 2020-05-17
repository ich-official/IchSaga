using UnityEngine;
using System.Collections;


public struct Leo_ProtoTest
{

    public ushort protoCode;  //老师实现了接口定义此属性，我暂时不用
    public int ID;
    public string name;
    public float price;

    /// <summary>
    /// 把对象转化成字节数组
    /// </summary>
    /// <returns></returns>
    public byte[] ToArray()
    {
        using (MemoryStreamUtil stream = new MemoryStreamUtil())
        {
            stream.WriteUShort(protoCode);
            stream.WriteInt(ID);
            stream.WriteUTF8String(name);
            stream.WriteFloat(price);
            return stream.ToArray();
        }
    }

    /// <summary>
    /// 把字节数组转化成对象，GetProto
    /// 注意：此方法解析的数据不包含protoCode，因为服务器传回数据时已经把protocode移除了，根据业务需要可以改成传回的
    /// </summary>
    /// <returns></returns>

    public static Leo_ProtoTest ToObject(byte[] buffer)
    {
        Leo_ProtoTest proto = new Leo_ProtoTest();
        using (MemoryStreamUtil stream =new MemoryStreamUtil(buffer)){
            //proto.protoCode = stream.ReadUShort();
            proto.ID=stream.ReadInt();
            proto.name = stream.ReadUTF8String();
            proto.price = stream.ReadFloat();
        }
        return proto;
    }

    public static Leo_ProtoTest ToObjectWithProtoCode(byte[] buffer)
    {
        Leo_ProtoTest proto = new Leo_ProtoTest();
        using (MemoryStreamUtil stream = new MemoryStreamUtil(buffer))
        {
            proto.protoCode = stream.ReadUShort();
            proto.ID = stream.ReadInt();
            proto.name = stream.ReadUTF8String();
            proto.price = stream.ReadFloat();
        }
        return proto;
    }
}

