//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using System.Collections;

/// <summary>
/// RoleClass实体，自动生成，不要轻易修改
/// </summary>
public partial class RoleClassEntity : AbstractEntity
{
    /// <summary>
    /// 职业名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 职业头像
    /// </summary>
    public string HeadIcon { get; set; }

    /// <summary>
    /// 职业立绘
    /// </summary>
    public string RoleRichi { get; set; }

    /// <summary>
    /// 模型预设名
    /// </summary>
    public string PrefabName { get; set; }

    /// <summary>
    /// 职业描述
    /// </summary>
    public string Desc { get; set; }

}
