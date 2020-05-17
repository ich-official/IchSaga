//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-01 12:44:10
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 账户实体
/// </summary>
public class AccountEntity : ClientEntityBase {
    #region 实体属性
    /// <summary>
    /// 主键
    /// </summary>
    public int PKValue
    {
        get { return this.Id; }
        set { this.Id = value; }
    }

    /// <summary>
    /// 编号
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    ///用户名 
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///密码 
    /// </summary>
    public string Pwd { get; set; }

    /// <summary>
    ///手机 
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///邮箱 
    /// </summary>
    public string MailAddress { get; set; }

    /// <summary>
    ///元宝 
    /// </summary>
    public int Gem { get; set; }

    /// <summary>
    ///渠道号 
    /// </summary>
    public short ChannelID { get; set; }

    /// <summary>
    ///最后登录服务器Id 
    /// </summary>
    public int LastLoginServerId { get; set; }

    /// <summary>
    ///最后登录服务器名称 
    /// </summary>
    public string LastLoginServerName { get; set; }

    /// <summary>
    ///最后登录服务器时间 
    /// </summary>
    public DateTime LastLoginServerTime { get; set; }
    /// <summary>
    /// 最后登录服务器的IP
    /// </summary>
    public string LastLoginServerIp { get; set; }
    /// <summary>
    /// 最后登录服务器的端口号
    /// </summary>
    public int LastLoginServerPort { get; set; }
    /// <summary>
    ///最后登录角色Id 
    /// </summary>
    public int LastLoginRoleId { get; set; }

    /// <summary>
    ///最后登录角色名称 
    /// </summary>
    public string LastLoginRoleName { get; set; }

    /// <summary>
    ///最后登录角色职业Id 
    /// </summary>
    public int LastLoginRoleClassId { get; set; }

    /// <summary>
    ///创建时间 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///更新时间 
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    ///设备标识 
    /// </summary>
    public string DeviceIdentifier { get; set; }

    /// <summary>
    ///设备型号 
    /// </summary>
    public string DeviceModel { get; set; }

    #endregion

}
