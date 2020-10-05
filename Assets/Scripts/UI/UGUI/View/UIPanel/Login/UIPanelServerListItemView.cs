//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-30 01:30:43
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
/// �����б�ҳ
/// </summary>
public class UIPanelServerListItemView : MonoBehaviour {

    private int mPageIndex; //�б�ҳ��
    [SerializeField]
    private Text Name; //�б�����

    public Action<int> OnServerListItemClick;   //����б�ҳ���л�����������ҳ
	void Start () {
        GetComponent<Button>().onClick.AddListener(ThisItemClick);
	}

    private void ThisItemClick()
    {
        if (OnServerListItemClick != null)
        {
            OnServerListItemClick(mPageIndex);
        }
    }

    public void SetUI(GameServerListEntity entity)
    {
        mPageIndex = entity.PageIndex;
        Name.text = entity.Name;
    }
}
