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
/// 效果等于Leo_UISceneManager（已作废），以后统一使用该类，UIRoot控制器
/// </summary>
public class UIRootController : SingletonBase<UIRootController>{
    public UIRootViewBase currentScene;    //当前的场景
    public enum UIRootType
    {
        LOGIN,
        LOADING,
        SELECT_ROLE,
        MAIN
    }

    public GameObject LoadUIRoot(UIRootType type)
    {
        GameObject obj = null;
        switch (type)
        {
            case UIRootType.LOGIN:
                obj = ResourcesManager.Instance.
                    Load(ResourcesManager.ResourceType.UIRoot, "UIRoot_LoginUGUI");
                currentScene = obj.GetComponent<UIRootViewBase>();
                break;
            case UIRootType.LOADING:
                break;
            case UIRootType.MAIN:
                obj = ResourcesManager.Instance.
                    Load(ResourcesManager.ResourceType.UIRoot, "UIRoot_MainUGUI");
                currentScene = obj.GetComponent<UIRootViewBase>();
                break;
            case UIRootType.SELECT_ROLE:
                obj = ResourcesManager.Instance.
                    Load(ResourcesManager.ResourceType.UIRoot, "UIRoot_SelectRoleUGUI");
                currentScene = obj.GetComponent<UIRootViewBase>();
                break;
        }

        return obj;
    }

}
