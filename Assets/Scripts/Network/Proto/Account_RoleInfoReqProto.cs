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