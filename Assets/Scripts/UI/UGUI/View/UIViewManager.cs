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
using System.Collections.Generic;
/// <summary>
/// V层管理，所有UI显示的管理
/// </summary>
public class UIViewManager : SingletonBase<UIViewManager>
{
    private Dictionary<UIWindowType, ISystemController> dic = new Dictionary<UIWindowType, ISystemController>();

    //目前登录注册窗口基础逻辑功能已做好，先把这两个预先加入字典
    public UIViewManager()
    {
        dic.Add(UIWindowType.LOGIN, AccountController.Instance);
        dic.Add(UIWindowType.REG, AccountController.Instance);
    }
    public void OpenPanel(UIWindowType windowType)
    {
        dic[windowType].OpenView(windowType);
    }

}
