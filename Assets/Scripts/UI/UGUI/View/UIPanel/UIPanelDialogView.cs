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
using System;

/// <summary>
/// 通用UI对话框，带确定和取消按钮的样式,=UIMessageView
/// </summary>
public class UIPanelDialogView : MonoBehaviour {

    [SerializeField]
    private Text dialogContext; //提示信息内容
    [SerializeField]
    private Button dialogOKBtn; //确定按钮
    [SerializeField]
    private Button dialogCancelBtn; //取消按钮

    public Action OnOKClickHandler;
    public Action OnCancelClickHandler;

    void Awake()
    {
        EventTriggerListener.Get(dialogOKBtn.gameObject).onClick = OnOKClickCallback;
        EventTriggerListener.Get(dialogCancelBtn.gameObject).onClick = OnCancelClickCallback;

    }

    /// <summary>
    /// 打开对话框的同时显示提示信息，给按钮添加委托
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="okAction"></param>
    /// <param name="cancelAction"></param>
    public void Show(string msg, Action okAction, Action cancelAction, DialogType type = DialogType.OK)
    {
        gameObject.transform.localPosition = Vector3.zero;
        if (type == DialogType.OK)
        {
            //只有确定按钮
            dialogCancelBtn.gameObject.SetActive(false);
            dialogOKBtn.transform.localPosition = Vector3.zero;
        }
        else
        {
            //有确定和取消2个按钮
            dialogCancelBtn.gameObject.SetActive(true);
            dialogOKBtn.transform.localPosition = new Vector3(-70, 0, 0);
        }
        dialogContext.text = msg;
        OnOKClickHandler = okAction;
        OnCancelClickHandler = cancelAction;
    }
    //隐藏对话框，实际不关，只放到摄像机之外的位置
    public void Hide()
    {
        gameObject.transform.localPosition = new Vector3(0, 5000, 0);
    }
    public void OnOKClickCallback(GameObject obj)
    {
        if (OnOKClickHandler != null) OnOKClickHandler();
        Hide();
    }
    public void OnCancelClickCallback(GameObject obj)
    {
        if (OnCancelClickHandler != null) OnCancelClickHandler();
        Hide();
    }
}
