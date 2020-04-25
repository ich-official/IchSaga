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
}
