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
/// ���������item
/// </summary>
public class UIGizmosQuestItemView : MonoBehaviour {

    [SerializeField]
    private GameObject mQuestMainBtn;   //ÿ��item����ͼobj

    [SerializeField]
    private Text mQuestNameText;         //�ؿ�����

    [SerializeField]
    private Button mQuickRaidBtn;        //һ��ɨ����ť

    private GameQuestEntity entity = new GameQuestEntity();   //�����item����Ϣ���ؿ��ٵ�½�����õ�
    public Action<GameQuestEntity> OnQuestItemClick;   //�������ҳ��ѡ���Ӧ����

    // Use this for initialization
    void Start () {
        mQuestMainBtn.GetComponent<Button>().onClick.AddListener(ThisItemClick);
    }

    //���һ������buttonʱ
    public void ThisItemClick()
    {
        if (OnQuestItemClick != null)
        {
            OnQuestItemClick(entity);
        }
    }

    /// <summary>
    /// һ��ɨ���Ƿ������ؿ����֣��ؿ�ͼƬ
    /// </summary>
    /// <param name="raidIsActive"></param>
    /// <param name="questName"></param>
    /// <param name="questImg"></param>
    public void SetUI(GameQuestEntity entity)
    {
        this.entity = entity;
        mQuestMainBtn.GetComponent<Image>().overrideSprite = GameUtil.LoadSprite(Constant.PATH_QuestImg+ entity.DlgPic);
        mQuestNameText.text = entity.Name;
        //mQuickRaidBtn.interactable = false;   //UI�ṹ�ı䣬һ��ɨ�����ڴ�UI�Ͽ���
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
