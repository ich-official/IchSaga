//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-01 16:40:56
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����UI����ͼ
/// </summary>
public class UIRootMainCityView : UIRootViewBase
{

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void OnStart()
    {
        base.OnStart();
        //OnViewLoadDoneί���ڻ������Ѿ�д�������಻����д��
        //if (OnViewLoadDone != null) OnViewLoadDone();
    }

    protected override void OnBtnClick(GameObject obj)
    {
        switch (obj.name)
        {
            case "PutInButton":
                ChangeMenuStatus(obj);
                break;
            case "RoleButton":
                UIViewManager.Instance.OpenPanel(UIPanelType.RoleMenu);
                break;
            case "StartMainQuestButton":
                UIViewManager.Instance.OpenPanel(UIPanelType.MainQuest);
                break;
        }

        base.OnBtnClick(obj);
    }
    /// <summary>
    /// �ײ��˵���ʾ/����
    /// </summary>
    /// <param name="obj"></param>
    private void ChangeMenuStatus(GameObject obj)
    {
        UIGizmosMainMenusView.Instance.ChangeMenuStatus();
    }
}
