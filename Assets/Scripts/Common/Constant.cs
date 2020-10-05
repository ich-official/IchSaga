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
    public const int ROLE_GET_ROLEINFO_FAIL = 4004;     //查询角色详情失败，原因：未找到对应的角色ID
    #endregion

    #region UIResource本地路径相关
    public const string PATH_QuestImg = "UIPrefabs/UIResources/QuestImg/";
    public const string PATH_HeadImg = "UIPrefabs/UIResources/HeadImg/";
    public const string PATH_ItemImg = "UIPrefabs/UIResources/ItemImg/";
    
    #endregion



    #region 本地excel表读写相关dic的key
    //章节相关
    public const string EXCEL_ChapterId = "ChapterId";
    public const string EXCEL_ChapterName = "ChapterName";

    //关卡相关
    public const string EXCEL_QuestId = "QuestId";
    public const string EXCEL_QuestName = "QuestName";

    public const string EXCEL_BasicRewardImg1 = "BasicRewardImg1";
    public const string EXCEL_BasicRewardText1 = "BasicRewardText1";
    public const string EXCEL_BasicRewardImg2 = "BasicRewardImg2";
    public const string EXCEL_BasicRewardText2 = "BasicRewardText2";

    public const string EXCEL_ItemRewardImg1 = "ItemRewardImg1";
    public const string EXCEL_ItemRewardText1 = "ItemRewardText1";
    public const string EXCEL_ItemRewardImg2 = "ItemRewardImg2";
    public const string EXCEL_ItemRewardText2 = "ItemRewardText2";

    #endregion


    #region AB加载和真机使用路径相关
    public const string AB_InitScene = @"download\scene\initScene\{0}.unity3d";

#if UNITY_EDITOR   //编辑器调试时的路径
    public static readonly string AB_localFilePath = Application.dataPath + "/../" + "AssetBundles/";
#elif UNITY_ANDROID ||UNITY_STANDALONE_WIN  || UNITY_IPHONE
    public static readonly string AB_localFilePath = Application.persistentDataPath + "/";
#endif


    #endregion
}
