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
using UnityEngine.UI;
/// <summary>
/// 此view的截图展示地址/ScreenShots/UI/LoginView.jpg
/// </summary>
public class UIPanelLoginView : UIPanelViewBase {

    public InputField Username;
    public InputField Pwd;

    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);
        switch (obj.name)
        {
            case "LoginButton":
                Debug.Log("login");
                //if (OnLoginButtonClick != null) OnLoginButtonClick();使用观察者模式后不再直接调用委托了
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelLoginView_LoginButton);
                break;
            case "GotoRegButton":
                Debug.Log("reg");
                //ToRegView();
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelLoginView_GotoRegButton);

                break;
        }

    }

}
