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
/// 服务器返回创建角色消息
/// 此协议手动修改过，无论isSuccess是否为true，都读取msgCode，配合AddRole后返回RoleId时使用
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
            ms.WriteInt(MsgCode);
            /*
            if (!IsSuccess)
            {
                
            }
            */
            return ms.ToArray();
        }
    }
    /// <summary>
    /// 手动修改过，无论是否成功都读取msgCode
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static Account_AddRoleRespProto GetProto(byte[] buffer)
    {
        Account_AddRoleRespProto proto = new Account_AddRoleRespProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            proto.MsgCode = ms.ReadInt();
            /*
            if (!proto.IsSuccess)
            {
                proto.MsgCode = ms.ReadInt();
            }
            */
        }
        return proto;
    }
}