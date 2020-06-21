//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 11:55:35
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 大区列表实体
/// </summary>
public class GameServerListEntity : ClientEntityBase
{
    /// <summary>
    /// 列表页码
    /// </summary>
    public int PageIndex;
    /// <summary>
    /// 列表名，举例：1-10服，11-20服
    /// </summary>
    public string Name;
}
