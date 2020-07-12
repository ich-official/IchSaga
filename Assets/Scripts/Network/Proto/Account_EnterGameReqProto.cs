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
/// 客户端发送进入游戏消息
/// </summary>
public struct Account_EnterGameReqProto : IProto
{
    public ushort ProtoCode { get { return 10007; } }

    public int RoleId; //角色编号
    public int ChannelId; //渠道号
    public int GameServerId;    //游戏大区的Id号

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            ms.WriteInt(ChannelId);
            return ms.ToArray();
        }
    }

    public static Account_EnterGameReqProto GetProto(byte[] buffer)
    {
        Account_EnterGameReqProto proto = new Account_EnterGameReqProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.RoleId = ms.ReadInt();
            proto.ChannelId = ms.ReadInt();
        }
        return proto;
    }
}