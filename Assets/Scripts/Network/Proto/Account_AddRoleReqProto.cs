//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-07-01 14:51:05
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送创建角色消息
/// </summary>
public struct Account_AddRoleReqProto : IProto
{
    public ushort ProtoCode { get { return 10003; } }

    public byte ClassId; //职业ID
    public string RoleNickName; //角色名称
    public int GameServerId;    //角色所在区服ID

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteByte(ClassId);
            ms.WriteUTF8String(RoleNickName);
            ms.WriteInt(GameServerId);
            return ms.ToArray();
        }
    }

    public static Account_AddRoleReqProto GetProto(byte[] buffer)
    {
        Account_AddRoleReqProto proto = new Account_AddRoleReqProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.ClassId = (byte)ms.ReadByte();
            proto.RoleNickName = ms.ReadUTF8String();
            proto.GameServerId = ms.ReadInt();
        }
        return proto;
    }
}