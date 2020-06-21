//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-02-25 22:40:37
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回创建角色消息
/// </summary>
public struct Account_AddRoleRespProto : IProto
{
    public ushort ProtoCode { get { return 10004; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static Account_AddRoleRespProto GetProto(byte[] buffer)
    {
        Account_AddRoleRespProto proto = new Account_AddRoleRespProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}