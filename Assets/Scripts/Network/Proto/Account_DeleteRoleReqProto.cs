//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-02-25 22:40:37
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送删除角色消息
/// </summary>
public struct Account_DeleteRoleReqProto : IProto
{
    public ushort ProtoCode { get { return 10005; } }

    public int RoleId; //角色ID

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            return ms.ToArray();
        }
    }

    public static Account_DeleteRoleReqProto GetProto(byte[] buffer)
    {
        Account_DeleteRoleReqProto proto = new Account_DeleteRoleReqProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.RoleId = ms.ReadInt();
        }
        return proto;
    }
}