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
/// <summary>
/// 存放所有常量
/// </summary>
public class Constant  {

    //两个都是用于PlayerPrefs查找的key，不是value
    public const string MIR2_USERNAME = "MIR2_USERNAME";
    public const string MIR2_PASSWORD = "MIR2_PASSWORD";

    #region UI按钮名="控件上绑定的脚本名"
    public const string UIPanelLoginView_LoginButton = "UIPanelLoginView_LoginButton";
    public const string UIPanelLoginView_GotoRegButton = "UIPanelLoginView_GotoRegButton";
    public const string UIPanelRegView_ConfirmButton = "UIPanelRegView_ConfirmButton";
    public const string UIPanelRegView_ReturnButton = "UIPanelRegView_ReturnButton";

    #endregion



    #region Http模拟器错误码
    public const int UNKNOWN = 1000;
    public const int ACCOUNT_SQL_ERROR=1001;    //sql语句执行错误
    public const int ACCOUNT_USER_EXIST = 1002; //用户已存在
    public const int ACCOUNT_USER_NOT_EXIST = 1003; //用户不存在
    public const int ACCOUNT_PWD_WRONG = 1004;  //账户密码错误

    #endregion 

}
