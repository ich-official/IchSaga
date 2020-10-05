//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-26 15:31:13
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 真正的登陆界面，可选大区的，截图路径ScreenShots\UI\EnterServerView.png
/// </summary>
public class UIPanelEnterServerView : UIPanelViewBase
{
    public Text CurrentServerLabel; //当前选择的区服，未登录的运行显示推荐服，已登陆过显示上次登陆的服

    public void SetText(string ServerName)
    {
        CurrentServerLabel.text = ServerName;
    }
    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);
        switch (obj.name)
        {
            case "ChangeServerButton"://点击换区
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelEnterServerView_ChangeServerButton);
                break;
            case "EnterGameButton"://进入选择角色界面
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelEnterServerView_EnterGameButton);
                break;
        }

    }
}
