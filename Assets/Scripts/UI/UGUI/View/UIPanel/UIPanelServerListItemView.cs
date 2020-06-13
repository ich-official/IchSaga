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
/// 区服列表页
/// </summary>
public class UIPanelServerListItemView : MonoBehaviour {

    private int mPageIndex; //列表页码
    [SerializeField]
    private Text Name; //列表名称

    public Action<int> OnServerListItemClick;   //点击列表页，切换服务器详情页
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
