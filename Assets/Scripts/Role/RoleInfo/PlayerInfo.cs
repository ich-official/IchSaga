//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 玩家自身信息
/// </summary>
public class PlayerInfo : RoleInfoBase {

    public int ClassId;

    public int TotalRechargeGem;

    public int Gem;

    public int Gold;

    #region 构造函数
    public PlayerInfo()
    {

    }
    public PlayerInfo(Account_RoleInfoRespProto proto)
    {
        RoleId = proto.RoldId;
        RoleNickName = proto.RoleNickName;
        ClassId = proto.ClassId;
        Level = proto.Level;
        TotalRechargeGem = proto.TotalRechargeGem;
        Gem = proto.Gem;
        Gold = proto.Gold;
        Exp = proto.Exp;
        MaxHP = proto.MaxHP;
        MaxMP = proto.MaxMP;
        CurrHP = proto.CurrHP;
        CurrMP = proto.CurrMP;
        Attack = proto.Attack;
        Defense = proto.Defense;
        Hit = proto.Hit;
        Dodge = proto.Dodge;
        Cri = proto.Cri;
        Res = proto.Res;
        SumDPS = proto.SumDPS;
    }
    #endregion
}
