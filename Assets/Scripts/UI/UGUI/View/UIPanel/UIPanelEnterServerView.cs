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
/// �����ĵ�½���棬��ѡ�����ģ���ͼ·��ScreenShots\UI\EnterServerView.png
/// </summary>
public class UIPanelEnterServerView : UIPanelViewBase
{
    public Text CurrentServerLabel; //��ǰѡ���������δ��¼��������ʾ�Ƽ������ѵ�½����ʾ�ϴε�½�ķ�

    public void SetText(string ServerName)
    {
        CurrentServerLabel.text = ServerName;
    }
    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);
        switch (obj.name)
        {
            case "ChangeServerButton"://�������
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelEnterServerView_ChangeServerButton);
                break;
            case "EnterGameButton"://����ѡ���ɫ����
                UIDisPatcher.Instance.Dispatch(Constant.UIPanelEnterServerView_EnterGameButton);
                break;
        }

    }
}
