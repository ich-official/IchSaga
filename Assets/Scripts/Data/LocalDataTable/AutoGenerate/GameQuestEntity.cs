
//===================================================
//Author：Ich
//CreateTime：2020-08-29 16:04:42
//Version: 1.0.0
//ProjectURL: https://github.com/ich-official/IchSaga
//Contact_Me: ich_official@163.com//Warning: This is an auto-generate file, please don't modify it casually !
//===================================================
using System.Collections;

/// <summary>
/// GameQuest实体
/// </summary>
public partial class GameQuestEntity : AbstractEntity
{
    /// <summary>
    /// 所属章编号
    /// </summary>
    public int ChapterID { get; set; }

    /// <summary>
    /// 游戏关卡名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 场景名称
    /// </summary>
    public string SceneName { get; set; }

    /// <summary>
    /// 小地图图片
    /// </summary>
    public string SmallMapImg { get; set; }

    /// <summary>
    /// 是否boss关卡
    /// </summary>
    public int isBoss { get; set; }

    /// <summary>
    /// 关卡图标
    /// </summary>
    public string Ico { get; set; }

    /// <summary>
    /// 地图上的节坐标(x_y)
    /// </summary>
    public string PosInMap { get; set; }

    /// <summary>
    /// 关卡图片
    /// </summary>
    public string DlgPic { get; set; }

    /// <summary>
    /// 镜头的旋转角度
    /// </summary>
    public string CameraRotation { get; set; }

    /// <summary>
    /// 背景音乐
    /// </summary>
    public string Audio_BG { get; set; }

}
