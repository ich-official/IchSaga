//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-30 20:18:09
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/// <summary>
/// 区服详情，
/// </summary>
public class UIPanelServerInfosItemView : MonoBehaviour {

    [SerializeField]
    private Sprite[] mServerStatusList;  //全部运行状态   0：维护  1：流畅  2：爆满
    [SerializeField]    
    private Image mServerStatus;  //服务器运行状态
    [SerializeField]
    private Text mServerName;   //服务器名字

    private GameServerEntity entity = new GameServerEntity();   //把这个item的信息传回快速登陆界面用的
    public Action<GameServerEntity> OnServerInfoItemClick;   //点击详情页，选择对应大区
    private string mIp;
    private int mPort;
	void Start () {
        GetComponent<Button>().onClick.AddListener(ThisItemClick);
	}

    //点击一个服务器详情button时
    public void ThisItemClick()
    {
        if (OnServerInfoItemClick != null)
        {
            OnServerInfoItemClick(entity);
        }
    }

	void Update () {
	
	}

    public void SetUI(GameServerEntity entity)
    {
        this.entity = entity;
        mServerStatus.overrideSprite = mServerStatusList[entity.RunStatus];
        mServerName.text = entity.Name;
        mIp = entity.Ip;
        mPort = entity.Port;
    }
}
