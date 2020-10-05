//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
/// <summary>
/// 登陆UI控制器
/// </summary>
public class LoginController : SingletonBase<LoginController>
{
    /// <summary>
    /// 打开登陆UI界面
    /// </summary>
    public void OpenLoginPanel()
    {
        UIViewManager.Instance.OpenPanel(UIPanelType.LOGIN);
    }

}