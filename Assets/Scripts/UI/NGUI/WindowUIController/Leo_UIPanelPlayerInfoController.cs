using UnityEngine;
using System.Collections;

public class Leo_UIPanelPlayerInfoController : Leo_UIWindowBase {

    protected override void OnBtnClick(GameObject obj)
    {
        switch (obj.name)
        {
            case "CloseButton":
                SelfClose();
                break;
        }
    }

}
