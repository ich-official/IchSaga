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
/// ͨ��UI�Ի��򣬴�ȷ����ȡ����ť����ʽ,=UIMessageView
/// </summary>
public class UIPanelDialogView : MonoBehaviour {

    [SerializeField]
    private Text dialogContext; //��ʾ��Ϣ����
    [SerializeField]
    private Button dialogOKBtn; //ȷ����ť
    [SerializeField]
    private Button dialogCancelBtn; //ȡ����ť

    public Action OnOKClickHandler;
    public Action OnCancelClickHandler;

    void Awake()
    {
        EventTriggerListener.Get(dialogOKBtn.gameObject).onClick = OnOKClickCallback;
        EventTriggerListener.Get(dialogCancelBtn.gameObject).onClick = OnCancelClickCallback;

    }

    /// <summary>
    /// �򿪶Ի����ͬʱ��ʾ��ʾ��Ϣ������ť���ί��
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="okAction"></param>
    /// <param name="cancelAction"></param>
    public void Show(string msg, Action okAction, Action cancelAction, DialogType type = DialogType.OK)
    {
        gameObject.transform.localPosition = Vector3.zero;
        if (type == DialogType.OK)
        {
            //ֻ��ȷ����ť
            dialogCancelBtn.gameObject.SetActive(false);
            dialogOKBtn.transform.localPosition = Vector3.zero;
        }
        else
        {
            //��ȷ����ȡ��2����ť
            dialogCancelBtn.gameObject.SetActive(true);
            dialogOKBtn.transform.localPosition = new Vector3(-70, 0, 0);
        }
        dialogContext.text = msg;
        OnOKClickHandler = okAction;
        OnCancelClickHandler = cancelAction;
    }
    //���ضԻ���ʵ�ʲ��أ�ֻ�ŵ������֮���λ��
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
