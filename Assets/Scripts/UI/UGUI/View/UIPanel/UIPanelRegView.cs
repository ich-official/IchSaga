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
/// 此view的截图展示地址/ScreenShots/UI/RegView.jpg
/// </summary>
public class UIPanelRegView : UIPanelViewBase
{
    public InputField Username;
    public InputField Pwd;

    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);
        switch (obj.name)
        {
            case "ConfirmButton":
                Debug.Log("regOK");
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelRegView_ConfirmButton);
                break;
            case "ReturnButton":
                Debug.Log("return");
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelRegView_ReturnButton);
                break;
        }
    }
}
