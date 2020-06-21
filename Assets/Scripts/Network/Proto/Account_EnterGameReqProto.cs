//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-02-25 22:40:38
//备    注：
//===================================================
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