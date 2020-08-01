//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

/// <summary>
/// 把本项目的数据类型转换成byte[]数组类型。笔记：short=2字节 int=4字节 long=8字节 float=4字节 double=8字节 char=4字节 
/// MemoryStreamUtil
/// </summary>
public class MemoryStreamUtil : MemoryStream {
    public MemoryStreamUtil()
    {

    }

    public MemoryStreamUtil(byte[] buffer):base(buffer)
    {

    }

    #region Short
    //short=ToInt16();从流中读取一个short数据
    public short ReadShort()
    {
        byte[] byteArray = new byte[2];
        base.Read(byteArray, 0, 2);
        return BitConverter.ToInt16(byteArray, 0);

    }
    //把一个short数据写入流
    public void WriteShort(short value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion

    #region uShort
    public ushort ReadUShort()
    {
        byte[] byteArray = new byte[2];
        base.Read(byteArray, 0, 2);
        return BitConverter.ToUInt16(byteArray, 0);

    }
    //把一个ushort数据写入流
    public void WriteUShort(ushort value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion 

    #region Int
    //int=ToInt32();从流中读取一个int数据
    public int ReadInt()
    {
        byte[] byteArray = new byte[4];
        base.Read(byteArray, 0, 4);
        return BitConverter.ToInt32(byteArray, 0);

    }
    //把一个int数据写入流
    public void WriteInt(int value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion

    #region uInt
    public uint ReadUInt()
    {
        byte[] byteArray = new byte[4];
        base.Read(byteArray, 0, 4);
        return BitConverter.ToUInt32(byteArray, 0);

    }
    //把一个uint数据写入流
    public void WriteUInt(uint value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion 

    #region Long
    //long=ToInt64();从流中读取一个long数据
    public long ReadLong()
    {
        byte[] byteArray = new byte[8];
        base.Read(byteArray, 0, 4);
        return BitConverter.ToInt64(byteArray, 0);

    }
    //把一个short数据写入流
    public void WriteLong(long value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion

    #region uLong
    public ulong ReadULong()
    {
        byte[] byteArray = new byte[8];
        base.Read(byteArray, 0, 8);
        return BitConverter.ToUInt64(byteArray, 0);

    }
    //把一个ulong数据写入流
    public void WriteUInt(ulong value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion 

    #region Float
    //从流中读取一个float数据
    public float ReadFloat()
    {
        byte[] byteArray = new byte[4];
        base.Read(byteArray, 0, 4);
        return BitConverter.ToSingle(byteArray, 0);

    }
    //把一个float数据写入流
    public void WriteFloat(float value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion

    #region Double
    public double ReadDouble()
    {
        byte[] byteArray = new byte[8];
        base.Read(byteArray, 0, 8);
        return BitConverter.ToDouble(byteArray, 0);

    }
    //把一个double数据写入流
    public void WriteDouble(double value)
    {
        byte[] byteArray = BitConverter.GetBytes(value);
        base.Write(byteArray, 0, byteArray.Length);

    }
    #endregion 
    
    #region Bool
    public bool ReadBool()
    {
        return base.ReadByte() == 1;

    }
    //把一个double数据写入流
    public void WriteBool(bool value)
    {
        base.WriteByte(((byte)(value == true ? 1 : 0)));

    }
    #endregion

    #region String 
    /// <summary>
    /// 读取时如果有数据，那么之前写入时是知道多长的，写入的时候是传2个值的，一个是长度，一个是字符串本身
    /// </summary>
    /// <returns></returns>
    public string ReadUTF8String(){
        ushort len = this.ReadUShort();
        byte[] byteArray = new byte[len];
        base.Read(byteArray, 0, len);
        return Encoding.UTF8.GetString(byteArray);
    }

    public void WriteUTF8String(string value)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(value);
        if (byteArray.Length > 65535)
        {
            throw new InvalidCastException("string is out of range!");
        }
        this.WriteUShort((ushort)byteArray.Length); //记录字符串长度
        base.Write(byteArray, 0, byteArray.Length); //记录字符串本身
    }
    #endregion
}
