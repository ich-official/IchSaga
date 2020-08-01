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
/// 角色信息基类
/// </summary>
public class RoleInfoBase  {
    
    /// <summary>
    /// 本地ID，对应excel表中的ID
    /// </summary>
    public int roleLocalID;

    /// <summary>
    /// 远程服务器ID，对应服务器、数据库中的ID
    /// </summary>
    public long roleServerID;

    public string username;

    public int maxHP;
    
    public int currentHP;

    public int RoleId;  //该角色的ID




    /// <summary>
    ///昵称 
    /// </summary>
    public string RoleNickName;

    /// <summary>
    ///等级 
    /// </summary>
    public int Level;


    /// <summary>
    ///经验 
    /// </summary>
    public int Exp;

    /// <summary>
    ///最大HP 
    /// </summary>
    public int MaxHP;

    /// <summary>
    ///最大MP 
    /// </summary>
    public int MaxMP;

    /// <summary>
    ///当前HP 
    /// </summary>
    public int CurrHP;

    /// <summary>
    ///当前MP 
    /// </summary>
    public int CurrMP;

    /// <summary>
    ///攻击力 
    /// </summary>
    public int Attack;

    /// <summary>
    ///防御 
    /// </summary>
    public int Defense;

    /// <summary>
    ///命中 
    /// </summary>
    public int Hit;

    /// <summary>
    ///闪避 
    /// </summary>
    public int Dodge;

    /// <summary>
    ///暴击 
    /// </summary>
    public int Cri;

    /// <summary>
    ///抗性 
    /// </summary>
    public int Res;

    /// <summary>
    ///综合战斗力 
    /// </summary>
    public int SumDPS;


    #region 构造函数
    public RoleInfoBase()
    {

    }

    public RoleInfoBase(Account_RoleInfoRespProto proto)
    {

    }
    #endregion
}
