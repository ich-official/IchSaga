using UnityEngine;
using System.Collections;

/// <summary>
/// 只管理场景UI,原名SceneUIMgr
/// </summary>

public class Leo_UISceneManager : SingletonBase<Leo_UISceneManager>
{
    public Leo_UISceneBase currentScene;    //当前的场景
    public enum SceneUIType
    {
        LOGIN,
        LOADING,
        MAIN
    }

    public GameObject LoadSceneUI(SceneUIType type)
    {
        GameObject obj = null;
        switch (type)
        {
            case SceneUIType.LOGIN:
                obj = ResourcesManager.Instance.
                    Load(ResourcesManager.ResourceType.UI_SCENE, "UIRoot_Login");
                currentScene = obj.GetComponent<Leo_UISceneLoginController>();
                    break;
            case SceneUIType.LOADING:
                break;
            case SceneUIType.MAIN:
                obj = ResourcesManager.Instance.
                    Load(ResourcesManager.ResourceType.UI_SCENE, "UIRoot_Main");
                currentScene = obj.GetComponent<Leo_UISceneMainController>();
                break;
        }

        return obj;
    }
}
