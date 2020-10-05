//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-09-23 11:01:26
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 任务界面右侧详情的view
/// </summary>
public class UIPanelMainQuestDetailView : UIPanelViewBase {

    #region 组件声明
    [SerializeField]
    private Text QuestNameText;
    [SerializeField]
    private Image BasicRewardImg1;    //基础奖励图片1（经验）
    [SerializeField]
    private Text BasicRewardText1;  //奖励奖励说明1（经验）
    [SerializeField]
    private Image BasicRewardImg2;    //基础奖励图片2（金币）
    [SerializeField]
    private Text BasicRewardText2;  //奖励奖励说明2（金币）
    [SerializeField]
    private Image ItemRewardImg1;     //道具奖励图片1
    [SerializeField]
    private Text ItemRewardText1;  //道具奖励说明1
    [SerializeField]
    private Image ItemRewardImg2;     //道具奖励图片2
    [SerializeField]
    private Text ItemRewardText2;  //道具奖励说明2

    #endregion

    private int mQuestId = 0;

    public void SetUI(ReadExcelDataUtil data)
    {
        mQuestId = data.GetValue<int>(Constant.EXCEL_QuestId);

        QuestNameText.SetText(data.GetValue<string>(Constant.EXCEL_QuestName));

        //经验、金币
        BasicRewardText1.SetText(data.GetValue<int>(Constant.EXCEL_BasicRewardText1)+"经验".ToString());
        BasicRewardText2.SetText(data.GetValue<int>(Constant.EXCEL_BasicRewardText2)+"金币".ToString());
        //装备、材料
        ItemRewardText1.SetText(data.GetValue<string>(Constant.EXCEL_ItemRewardText1));
        ItemRewardText2.SetText(data.GetValue<string>(Constant.EXCEL_ItemRewardText2));
        //道具奖励图片
        ItemRewardImg1.overrideSprite = GameUtil.LoadSprite(Constant.PATH_ItemImg + data.GetValue<string>(Constant.EXCEL_ItemRewardImg1));
        ItemRewardImg2.overrideSprite = GameUtil.LoadSprite(Constant.PATH_ItemImg + data.GetValue<string>(Constant.EXCEL_ItemRewardImg2));

    }

    protected override void BeforeOnDestroy()
    {
        QuestNameText = null;

        BasicRewardImg1 = null;
        BasicRewardText1 = null;
        BasicRewardImg2 = null;
        BasicRewardText2 = null;

        ItemRewardImg1 = null;
        ItemRewardText1 = null;
        ItemRewardImg2 = null;
        ItemRewardText2 = null;
        base.BeforeOnDestroy();
    }
}
