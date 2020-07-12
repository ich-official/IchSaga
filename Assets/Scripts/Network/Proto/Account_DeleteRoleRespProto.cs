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
/// 服务器返回删除角色消息
/// </summary>
public struct Account_DeleteRoleRespProto : IProto
{
    public ushort ProtoCode { get { return 10006; } }

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

    public static Account_DeleteRoleRespProto GetProto(byte[] buffer)
    {
        Account_DeleteRoleRespProto proto = new Account_DeleteRoleRespProto();
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