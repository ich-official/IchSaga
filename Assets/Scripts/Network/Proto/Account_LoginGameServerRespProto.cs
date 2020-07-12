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
/// 服务器返回登录信息
/// </summary>
public struct Account_LoginGameServerRespProto : IProto
{
    public ushort ProtoCode { get { return 10002; } }

    public int RoleCount; //已有角色数量
    public List<RoleItem> RoleList; //角色项

    /// <summary>
    /// 角色项
    /// </summary>
    public struct RoleItem
    {
        public int RoleId; //角色编号
        public string RoleNickName; //角色昵称
        public byte RoleClass; //角色职业
        public int RoleLevel; //角色等级
    }

    public byte[] ToArray()
    {
        using (MemoryStreamUtil ms = new MemoryStreamUtil())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleCount);
            for (int i = 0; i < RoleCount; i++)
            {
                ms.WriteInt(RoleList[i].RoleId);
                ms.WriteUTF8String(RoleList[i].RoleNickName);
                ms.WriteByte(RoleList[i].RoleClass);
                ms.WriteInt(RoleList[i].RoleLevel);
            }
            return ms.ToArray();
        }
    }

    public static Account_LoginGameServerRespProto GetProto(byte[] buffer)
    {
        Account_LoginGameServerRespProto proto = new Account_LoginGameServerRespProto();
        using (MemoryStreamUtil ms = new MemoryStreamUtil(buffer))
        {
            proto.RoleCount = ms.ReadInt();
            proto.RoleList = new List<RoleItem>();
            for (int i = 0; i < proto.RoleCount; i++)
            {
                RoleItem _Role = new RoleItem();
                _Role.RoleId = ms.ReadInt();
                _Role.RoleNickName = ms.ReadUTF8String();
                _Role.RoleClass = (byte)ms.ReadByte();
                _Role.RoleLevel = ms.ReadInt();
                proto.RoleList.Add(_Role);
            }
        }
        return proto;
    }
}