//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 00:29:55
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// ͨ��UI�Ի����������=MessageCtrl
/// </summary>
public class UIDialogController : ControllerBase<UIDialogController>
{
    private GameObject mDialogObj;
    /// <summary>
    /// �򿪶Ի����ͬʱ��ʾ��ʾ��Ϣ������ť���ί��
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="okAction"></param>
    /// <param name="cancelAction"></param>
    public void Show(string msg, UIDialogType type = UIDialogType.OK, Action okAction=null, Action cancelAction=null)
    {
        if(mDialogObj==null) mDialogObj = ResourcesManager.Instance.Load(ResourcesManager.ResourceType.UIPanel, "Panel_Dialog", isCache: true);
        mDialogObj.transform.parent = UIRootController.Instance.currentScene.containerCenter;
        mDialogObj.transform.localPosition = Vector3.zero;
        mDialogObj.transform.localScale = Vector3.one;
        mDialogObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;  //�ӵ��壬��ֹ�㵽�����UI
        UIPanelDialogView view = mDialogObj.GetComponent<UIPanelDialogView>();
        if (view != null)
        {
            view.Show(msg, okAction, cancelAction, type);
        }
    }

    public void Hide()
    {

    }

}
