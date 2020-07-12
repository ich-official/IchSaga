//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-07-01 14:51:05
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	Update:手动修改过一次，添加了区服判断
//-----------------------------------------------------------
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
    public int GameServerId;    //区服ID

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(AccountId);
            ms.WriteInt(GameServerId);
            return ms.ToArray();
        }
    }

    public static Account_LoginGameServerReqProto GetProto(byte[] buffer)
    {
        Account_LoginGameServerReqProto proto = new Account_LoginGameServerReqProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.AccountId = ms.ReadInt();
            proto.GameServerId = ms.ReadInt();
        }
        return proto;
    }
}