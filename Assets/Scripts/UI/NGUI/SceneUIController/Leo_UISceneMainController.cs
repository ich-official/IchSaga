using UnityEngine;
using System.Collections;

/// <summary>
/// 主界面控制器
/// </summary>
public class Leo_UISceneMainController : Leo_UISceneBase {

    protected override void OnBtnClick(GameObject obj)
    {
        switch (obj.name)
        {
            case "PlayerIconButton":
                OpenPlayerInfoPanel();
                break;
        }
        base.OnBtnClick(obj);
    }

    void OpenPlayerInfoPanel()
    {
        Leo_UIWindowManager.Instance.OpenWindowUI(UIWindowType.PLAYERINFO, true);
    }
}
