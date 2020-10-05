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
/// V层管理，所有UI显示的管理,=UIViewUtil
/// </summary>
public class UIViewManager : SingletonBase<UIViewManager>
{
    private Dictionary<UIPanelType, ISystemController> dic = new Dictionary<UIPanelType, ISystemController>();

    //所有UIPanelView都必须加入此dic中，loadview时就是通过此dic查询的
    public UIViewManager()
    {
        dic.Add(UIPanelType.LOGIN, AccountController.Instance);
        dic.Add(UIPanelType.REG, AccountController.Instance);
        dic.Add(UIPanelType.EnterServer, GameServerController.Instance);
        dic.Add(UIPanelType.SelectServer, GameServerController.Instance);
        dic.Add(UIPanelType.RoleMenu, RoleController.Instance);
        dic.Add(UIPanelType.MainQuest, GameQuestController.Instance);
    }
    public void OpenPanel(UIPanelType windowType)
    {
        dic[windowType].OpenView(windowType);
    }

}
