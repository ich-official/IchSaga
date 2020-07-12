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
using System.Collections.Generic;
/// <summary>
/// 存放所有常量
/// </summary>
public class Constant  {

    //两个都是用于PlayerPrefs查找的key，不是value
    public const string MIR2_USERNAME = "MIR2_USERNAME";
    public const string MIR2_PASSWORD = "MIR2_PASSWORD";

    public const string QuickLoginID = "QuickLoginID";
    public const string QuickLoginUsername = "QuickLoginUsername";
    public const string QuickLoginPwd = "QuickLoginPwd";

    #region UI按钮名="控件上绑定的脚本名"
    //登陆
    public const string UIPanelLoginView_LoginButton = "UIPanelLoginView_LoginButton";
    public const string UIPanelLoginView_GotoRegButton = "UIPanelLoginView_GotoRegButton";
    //注册
    public const string UIPanelRegView_ConfirmButton = "UIPanelRegView_ConfirmButton";
    public const string UIPanelRegView_ReturnButton = "UIPanelRegView_ReturnButton";
    //进入游戏
    public const string UIPanelEnterServerView_ChangeServerButton = "UIPanelEnterServerView_ChangeServerButton";
    public const string UIPanelEnterServerView_EnterGameButton = "UIPanelEnterServerView_EnterGameButton";

    #endregion



    #region 与服务器通信的错误码
    public const int UNKNOWN = 1000;
    public const int SQL_ERROR=1001;    //sql语句执行错误

    public const int ACCOUNT_USER_EXIST = 2002; //用户已存在
    public const int ACCOUNT_USER_NOT_EXIST = 2003; //用户不存在
    public const int ACCOUNT_PWD_WRONG = 2004;  //用户名或密码错误

    public const int GAMESERVER_NO_SERVER_INFO = 3001;//没有查询到服务器信息

    public const int ROLE_ADD_FAIL = 4001;  //创建角色失败，原因：1、昵称已存在 2、昵称违规
    public const int ENTER_GAME_FAIL = 4002;    //进入游戏失败，暂时未定义原因
    public const int ROLE_DELETE_FAIL = 4003;   //删除角色失败，原因：未找到对应的角色ID
    #endregion


}
