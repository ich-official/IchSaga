//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-27 15:03:44
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏中关卡的控制器，=GameLeverCtrl
/// </summary>
public class GameQuestController : ControllerBase<GameQuestController>, ISystemController
{

    private UIPanelMainQuestView mUIPanelQuestView;

    private UIPanelMainQuestDetailView mUIPanelQuestDetailView;

    private Dictionary<int, List<GameQuestEntity>> dic = new Dictionary<int, List<GameQuestEntity>>();


    public void OpenView(UIPanelType windowType)
    {
        switch (windowType)
        {
            case UIPanelType.MainQuest:
                OpenMainQuestView();
                break;

        }
    }

    private void OpenMainQuestView()
    {
        //加载panel部分
        mUIPanelQuestView = UIViewManagerNGUI.Instance.OpenWindowUI(UIPanelType.MainQuest, true).GetComponent<UIPanelMainQuestView>();
        mUIPanelQuestDetailView = mUIPanelQuestView.transform.GetComponentInChildren<UIPanelMainQuestDetailView>();
        ReadExcelDataUtil excel = new ReadExcelDataUtil();
        ChapterEntity chapterEntity = ChapterDBModel.Instance.Get(1);
        excel.SetValue(Constant.EXCEL_ChapterId, chapterEntity.Id);
        excel.SetValue(Constant.EXCEL_ChapterName, chapterEntity.ChapterName);
        mUIPanelQuestView.SetUI(excel);

        //读取item详情（本地excel）部分
        //获取了GameQuest.xls里的全部内容，现在开始整理归类
        List<GameQuestEntity> questEntityList = GameQuestDBModel.Instance.GetAllData();

        #region 为item按章节分组的过程，并添加进dic中
        if (dic.Count == 0)
        {
            int tempChapterCount = 1;   //临时变量，用于分组使用
            int tempIndex = 0;
            for (int i = 0; i < questEntityList.Count; i++)
            {
                if (questEntityList[i].ChapterID != tempChapterCount) //章节发生了改变
                {
                    List<GameQuestEntity> questEntityPage = new List<GameQuestEntity>();
                    tempIndex = i;
                    for (int j = 0; j < tempIndex; j++)
                    {
                        questEntityPage.Add(questEntityList[j]);
                    }
                    dic.Add(tempChapterCount, questEntityPage);//《页码，item详情》
                    tempChapterCount++;
                }
            }
            List<GameQuestEntity> questEntityLastPage = new List<GameQuestEntity>();    //循环结束，把最后一篇的内容加到dic中
            for (int j = tempIndex; j < questEntityList.Count; j++)
            {

                tempIndex = questEntityList.Count;
                questEntityLastPage.Add(questEntityList[j]);

            }
            dic.Add(tempChapterCount, questEntityLastPage);//《页码，item详情》
        }

        #endregion

        //先加载panel，完毕后再加载item
        mUIPanelQuestView.OnViewLoadDone = () =>
        {
            //把做好的dic传给PanelView，view上可以直接使用
            mUIPanelQuestView.SetInfosDic(dic);

            //打开主线面板时，默认显示第一篇的关卡内容
            mUIPanelQuestView.SetQuestItem(1);
        };

        mUIPanelQuestView.OnItemClick = OnQuestItemClick;  //这一步是点击一个item后，panel要做的事


        

    }




    private void OnQuestItemClick(GameQuestEntity obj)
    {
        //TODO:加载右侧关卡详情
        Debug.Log("加载了右侧详情....");
        //读表

        GameQuestGradeEntity entity = GameQuestGradeDBModel.Instance.GetEntityByGameLevelId(obj.Id);

        if (entity == null) return;

        ReadExcelDataUtil data = new ReadExcelDataUtil();
        data.SetValue(Constant.EXCEL_QuestId, entity.GameLevelId);
        data.SetValue(Constant.EXCEL_QuestName, obj.Name);
        //赋值文字
        data.SetValue(Constant.EXCEL_BasicRewardText1, entity.Exp);
        data.SetValue(Constant.EXCEL_BasicRewardText2, entity.Gold);
        data.SetValue(Constant.EXCEL_ItemRewardText1, entity.EquipDesc);
        data.SetValue(Constant.EXCEL_ItemRewardText2, entity.MaterialDesc);
        //赋值图片
        data.SetValue(Constant.EXCEL_ItemRewardImg1, entity.EquipImg);
        data.SetValue(Constant.EXCEL_ItemRewardImg2, entity.MaterialImg);

        mUIPanelQuestDetailView.SetUI(data);

    }
}
