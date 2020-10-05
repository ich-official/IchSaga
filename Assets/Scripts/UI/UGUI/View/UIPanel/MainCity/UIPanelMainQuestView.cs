//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-27 15:23:25
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
/// 
/// </summary>
public class UIPanelMainQuestView : UIPanelViewBase
{

    #region 组件声明

    [SerializeField]
    private GameObject questItemPrefab;   //关卡item预制体

    [SerializeField]
    private Transform QuestItemArea;    //关卡item的parent

    [SerializeField]
    private Text mChapterName;
    #endregion


    #region 变量声明


    private int mChapterId;

    private List<GameObject> questItemObjPool = new List<GameObject>();  //详情页对象池

    //dic<页数，每页上所有item的详情集合>
    private Dictionary<int, List<GameQuestEntity>> dic = new Dictionary<int, List<GameQuestEntity>>();

    public Action<GameQuestEntity> OnItemClick;  //右侧详情页item点击时执行
    #endregion






    protected override void OnStart()
    {
        //预先加载8个空item
        for (int i = 0; i < 8; i++)
        {
            GameObject obj = Instantiate(questItemPrefab) as GameObject;
            obj.transform.parent = QuestItemArea;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            questItemObjPool.Add(obj);
        }
        base.OnStart();
    }


    public void SetUI(ReadExcelDataUtil excel)
    {
        mChapterId = excel.GetValue<int>(Constant.EXCEL_ChapterId);
        mChapterName.SetText(excel.GetValue<string>(Constant.EXCEL_ChapterName));
        Debug.Log("主线剧情panel赋值完毕：chapterId=" + mChapterId);
    }

    /// <summary>
    /// 按章节ID进行赋值
    /// </summary>
    /// <param name="chapterIndex"></param>
    public void SetQuestItem(int chapterIndex)
    {

        for (int i = 0; i < questItemObjPool.Count; i++)
        {
            if (i > dic[chapterIndex].Count)
            {
                questItemObjPool[i].SetActive(false);
            }
        }
        //生成一个列表item的过程
        for (int i = 0; i < dic[chapterIndex].Count; i++)
        {
            GameObject obj = questItemObjPool[i];
            UIGizmosQuestItemView view = obj.GetComponent<UIGizmosQuestItemView>();
            if (!obj.activeSelf) obj.SetActive(true);
            if (view != null)
            {
                view.SetUI(dic[chapterIndex][i]);
                //给item赋值点击事件
                view.OnQuestItemClick = OnQuestItemClick;
            }
        }

    }


    /// <summary>
    /// 当关卡item被点击时，加载对应的关卡详情
    /// </summary>
    /// <param name="entity"></param>
    private void OnQuestItemClick(GameQuestEntity obj)
    {
        Debug.Log("点击了关卡：" + obj.ChapterID + obj.Id+ "," + obj.PosInMap);

        if (OnItemClick != null)
        {
            OnItemClick(obj);
        }

    }
    /// <summary>
    /// 把Controller上做好的dic传给view上直接使用
    /// </summary>
    /// <param name="gameServerEntityDic"></param>
    public void SetInfosDic(Dictionary<int, List<GameQuestEntity>> gameQuestEntityDic)
    {
        if(dic.Count==0) dic = gameQuestEntityDic;

    }


    protected override void BeforeOnDestroy()
    {

        base.BeforeOnDestroy();
    }

}
