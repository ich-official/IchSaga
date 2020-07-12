//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-21 18:40:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	Warning: This is an auto-generate file, please don't modify it casually !
//-----------------------------------------------------------
using System.Collections;

/// <summary>
/// Class实体
/// </summary>
public partial class ClassEntity : AbstractEntity
{
    /// <summary>
    /// 职业名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string HeadPic { get; set; }

    /// <summary>
    /// 职业半身像
    /// </summary>
    public string JobPic { get; set; }

    /// <summary>
    /// 预设名称
    /// </summary>
    public string PrefabName { get; set; }

    /// <summary>
    /// 职业描述
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    /// 系数---攻击
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    /// 系数--防御
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    /// 系数--命中率
    /// </summary>
    public int Hit { get; set; }

    /// <summary>
    /// 系数--闪避率
    /// </summary>
    public int Dodge { get; set; }

    /// <summary>
    /// 系数--暴击率
    /// </summary>
    public int Cri { get; set; }

    /// <summary>
    /// 系数--抗性
    /// </summary>
    public int Res { get; set; }

    /// <summary>
    /// 使用的物理攻击Id
    /// </summary>
    public string UsedPhyAttackIds { get; set; }

    /// <summary>
    /// 使用的技能Id
    /// </summary>
    public string UsedSkillIds { get; set; }

}
