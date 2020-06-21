//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-02-25 22:40:37
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送登录区服消息
/// </summary>
public struct Account_LoginGameServerReqProto : IProto
{
    public ushort ProtoCode { get { return 10001; } }

    public int AccountId; //账户ID

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(AccountId);
            return ms.ToArray();
        }
    }

    public static Account_LoginGameServerReqProto GetProto(byte[] buffer)
    {
        Account_LoginGameServerReqProto proto = new Account_LoginGameServerReqProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.AccountId = ms.ReadInt();
        }
        return proto;
    }
}