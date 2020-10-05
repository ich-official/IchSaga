
//===================================================
//Author：Ich
//CreateTime：2020-09-29 02:06:38
//Version: 1.0.0
//ProjectURL: https://github.com/ich-official/IchSaga
//Contact_Me: ich_official@163.com//Warning: This is an auto-generate file, please don't modify it casually !
//===================================================
using System.Collections;

/// <summary>
/// GameQuestGrade实体
/// </summary>
public partial class GameQuestGradeEntity : AbstractEntity
{
    /// <summary>
    /// 游戏关卡编号
    /// </summary>
    public int GameLevelId { get; set; }

    /// <summary>
    /// 难度等级分类 0=普通 1=困难 2=地狱
    /// </summary>
    public int Grade { get; set; }

    /// <summary>
    /// 关卡描述
    /// </summary>
    public string Desc { get; set; }

    /// <summary>
    /// 关卡类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 关卡类型相关数据
    /// </summary>
    public string Parameter { get; set; }

    /// <summary>
    /// 过关条件描述文字
    /// </summary>
    public string ConditionDesc { get; set; }

    /// <summary>
    /// 奖励经验
    /// </summary>
    public int Exp { get; set; }

    /// <summary>
    /// 奖励金币
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    /// 推荐战力
    /// </summary>
    public int CommendFighting { get; set; }

    /// <summary>
    /// 时间限制
    /// </summary>
    public float TimeLimit { get; set; }

    /// <summary>
    /// 时间1
    /// </summary>
    public float Star1 { get; set; }

    /// <summary>
    /// 时间2
    /// </summary>
    public float Star2 { get; set; }

    /// <summary>
    /// 奖励装备 装备Id_概率_数量|…
    /// </summary>
    public string Equip { get; set; }

    /// <summary>
    /// 奖励装备统一图片
    /// </summary>
    public string EquipImg { get; set; }

    /// <summary>
    /// 奖励装备统一描述
    /// </summary>
    public string EquipDesc { get; set; }

    /// <summary>
    /// 奖励道具 道具Id_概率_数量|…
    /// </summary>
    public string Item { get; set; }

    /// <summary>
    /// 奖励道具统一图片
    /// </summary>
    public string ItemImg { get; set; }

    /// <summary>
    /// 奖励道具统一描述
    /// </summary>
    public string ItemDesc { get; set; }

    /// <summary>
    /// 奖励材料 材料Id_概率_数量|…
    /// </summary>
    public string Material { get; set; }

    /// <summary>
    /// 奖励材料统一图片
    /// </summary>
    public string MaterialImg { get; set; }

    /// <summary>
    /// 奖励材料统一描述
    /// </summary>
    public string MaterialDesc { get; set; }

}
