//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-29 16:14:56
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 主线任务的item
/// </summary>
public class UIGizmosQuestItemView : MonoBehaviour {

    [SerializeField]
    private GameObject mQuestMainBtn;   //每个item的主图obj

    [SerializeField]
    private Text mQuestNameText;         //关卡名字

    [SerializeField]
    private Button mQuickRaidBtn;        //一键扫荡按钮

    private GameQuestEntity entity = new GameQuestEntity();   //把这个item的信息传回快速登陆界面用的
    public Action<GameQuestEntity> OnQuestItemClick;   //点击详情页，选择对应大区

    // Use this for initialization
    void Start () {
        mQuestMainBtn.GetComponent<Button>().onClick.AddListener(ThisItemClick);
    }

    //点击一个任务button时
    public void ThisItemClick()
    {
        if (OnQuestItemClick != null)
        {
            OnQuestItemClick(entity);
        }
    }

    /// <summary>
    /// 一键扫荡是否开启，关卡名字，关卡图片
    /// </summary>
    /// <param name="raidIsActive"></param>
    /// <param name="questName"></param>
    /// <param name="questImg"></param>
    public void SetUI(GameQuestEntity entity)
    {
        this.entity = entity;
        mQuestMainBtn.GetComponent<Image>().overrideSprite = GameUtil.LoadSprite(Constant.PATH_QuestImg+ entity.DlgPic);
        mQuestNameText.text = entity.Name;
        //mQuickRaidBtn.interactable = false;   //UI结构改变，一键扫荡不在此UI上控制
        //mQuickRaidBtn.enabled = false;
        string[] localPos = entity.PosInMap.Split('_');
        transform.localPosition = new Vector2(int.Parse(localPos[0]), int.Parse(localPos[1]));
    }


    private void OnDestroy()
    {
        mQuestMainBtn = null;
        mQuestNameText = null;
        mQuickRaidBtn = null;
    }
}
