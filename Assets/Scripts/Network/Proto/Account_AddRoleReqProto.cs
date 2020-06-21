//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-02-25 22:40:37
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送创建角色消息
/// </summary>
public struct Account_AddRoleReqProto : IProto
{
    public ushort ProtoCode { get { return 10003; } }

    public byte JobId; //职业ID
    public string RoleNickName; //角色名称

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteByte(JobId);
            ms.WriteUTF8String(RoleNickName);
            return ms.ToArray();
        }
    }

    public static Account_AddRoleReqProto GetProto(byte[] buffer)
    {
        Account_AddRoleReqProto proto = new Account_AddRoleReqProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.JobId = (byte)ms.ReadByte();
            proto.RoleNickName = ms.ReadUTF8String();
        }
        return proto;
    }
}