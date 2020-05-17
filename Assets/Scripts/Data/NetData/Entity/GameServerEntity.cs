//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 11:55:42
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GameServerEntity : ClientEntityBase
{
    /// <summary>
    /// 大区ID
    /// </summary>
    public int Id;
    /// <summary>
    /// 大区运行状态
    /// </summary>
    public int RunStatus;
    /// <summary>
    /// //是否是推荐服
    /// </summary>
    public bool IsRecommend;

    /// <summary>
    /// 是否是新区
    /// </summary>
    public bool IsNew;

    /// <summary>
    /// 大区名
    /// </summary>
    public string Name;
    /// <summary>
    /// IP
    /// </summary>
    public string Ip;
    /// <summary>
    /// 端口号
    /// </summary>
    public int Port;
}
