//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-02-25 22:40:38
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端查询角色信息
/// </summary>
public struct Account_RoleInfoReqProto : IProto
{
    public ushort ProtoCode { get { return 10009; } }

    public int RoldId; //角色编号

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoldId);
            return ms.ToArray();
        }
    }

    public static Account_RoleInfoReqProto GetProto(byte[] buffer)
    {
        Account_RoleInfoReqProto proto = new Account_RoleInfoReqProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.RoldId = ms.ReadInt();
        }
        return proto;
    }
}