//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 23:47:11
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.ComponentModel;

//枚举类型汇总
#region  ENUM加载窗口的类型
/// <summary>
/// 要加载哪个window（NGUI时）/Panel（UGUI时）
/// </summary>
public enum UIPanelType
{
    /// <summary>
    /// //不打开任何窗口
    /// </summary>
    NONE,
    LOGIN,
    REG,
    PLAYERINFO,
    EnterServer,    //进入大区，开始游戏
    SelectServer,    //选择大区
    RoleMenu,        //主城“角色”菜单
    MainQuest       //主线任务的panel，UI设计参考了jjsg和网页my

}
#endregion

#region ENUM窗口靠近哪个锚点的方向
/// <summary>
/// 加载窗口靠近哪个位置
/// </summary>
public enum AnchorPosition  //temp,WindouUIContainerType
{
    TOP_LEFT,
    TOP_RIGHT,
    BOTTOM_LEFT,
    BOTTOM_RIGHT,
    CENTER
}
#endregion

#region ENUM窗口的打开方式（动画）
public enum ShowWindowStyle
{
    NONE,
    CENTER2BIG,
    TOP2BOTTOM,
    BOTTOM2TOP,
    LEFT2RIGHT,
    RIGHT2LEFT
}
#endregion

#region ENUM场景的名字
public enum SceneName
{
    LOGIN5X,
    MAINSCENE5X,
    SELECT_ROLE,
}
#endregion

#region ENUM 角色类型
public enum RoleType
{
    NONE,
    PLAYER,
    ENEMY
}
#endregion

#region ENUM 状态机中的状态
public enum RoleState
{
    NONE=0,
    Idle=1,
    Run=2,
    Attack=3,
    Hurt=4,
    Die=5,
    Fight = 6
}
#endregion

#region ENUM 角色动画名字，截取动画片段本身的名字
public enum RoleAniName
{
    Idle_Normal,
    Idle_Fight,
    Run,
    PhyAttack1,
    PhyAttack2,
    PhyAttack3,
    Hurt,
    Die
}
#endregion

#region ENUM 跳转动画条件名字，状态机上自己定义parameter的名字
public enum RoleAniChangeCondition
{
    ToIdleNormal,
    ToFight,
    ToRun,
    ToAttack,
    ToHurt,
    ToDie,
    ToIdleFight,
    CurrentState
}
#endregion

#region ENUM 对话框UI的类型，单按钮还是多按钮
public enum UIDialogType
{
    OK,
    OKAndCancel
}
#endregion

#region ENUM 仿mmcoyframework EnumEntityStatus通用数据状态枚举
public enum EnumEntityStatus : byte
{
    /// <summary>
    /// 已删除
    /// </summary>
    [Description("已删除")]
    Deleted = 0,
    /// <summary>
    /// 有效
    /// </summary>
    [Description("有效")]
    Released = 1
}
#endregion