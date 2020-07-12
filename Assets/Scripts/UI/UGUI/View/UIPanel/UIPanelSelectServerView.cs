//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-26 15:31:29
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

/// <summary>
/// 选服务器界面，包括推荐服务器和已登录过服务器
/// </summary>
public class UIPanelSelectServerView : UIPanelViewBase
{
    #region 组件声明
    [SerializeField]
    private GameObject ServerListItemPrefab;    //服务器左侧列表item预制体
    [SerializeField]
    private GridLayoutGroup ServerListGrid;   //左侧选择大区列表
    [SerializeField]
    private GameObject ServerInfosItemPrefab;   //右侧详情页item预制体
    [SerializeField]
    private GridLayoutGroup ServerInfosGrid;    //右侧详情页列表
    [SerializeField]
    private Transform ServerLastLoginPos1;      //上次登陆第一个生成点
    [SerializeField]
    private Transform ServerLastLoginPos2;      //上次登陆第二个生成点


    #endregion

    #region 变量声明
    private Dictionary<int, List<GameServerEntity>> dic = new Dictionary<int, List<GameServerEntity>>();
    private List<GameObject> listItemObjPool = new List<GameObject>();  //列表页对象池
    private List<GameObject> infoItemObjPool = new List<GameObject>();  //详情页对象池
    private UIPanelServerInfosItemView mInfoView;
    public Action<GameServerEntity> OnInfoItemClick;  //右侧详情页item点击时执行
    #endregion

    protected override void OnStart()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(ServerInfosItemPrefab) as GameObject;
            obj.transform.parent = ServerInfosGrid.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            infoItemObjPool.Add(obj);
        }
        base.OnStart();

    }
    #region 左侧大区列表操作
    /// <summary>
    /// 把大区列表的item生成出来
    /// </summary>
    public void SetListItem(List<GameServerListEntity> list)
    {
        if (list != null && ServerListItemPrefab!=null)
        {
            if (listItemObjPool.Count > 0)
            {
                for (int i = 0; i < listItemObjPool.Count; i++)
                {
                    Destroy(listItemObjPool[i]);    //临时写法，以后改为打开和关闭
                }
                listItemObjPool.Clear();
            }
            //生成一个列表item的过程
            for (int i = 0; i < list.Count; i++)
            {
                GameObject obj = Instantiate(ServerListItemPrefab) as GameObject;
                obj.transform.parent = ServerListGrid.transform;
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = Vector3.zero;
                UIPanelServerListItemView view = obj.GetComponent<UIPanelServerListItemView>();
                if (view != null)
                {
                    view.SetUI(list[i]);
                    //给item赋值点击事件
                    view.OnServerListItemClick = OnServerListItemClick;
                }
            }
        }
    }
    /// <summary>
    /// 大区列表item点击事件
    /// </summary>
    /// <param name="obj"></param>
    private void OnServerListItemClick(int pageIndex)
    {
        LogUtil.Log("点击了页码：" + pageIndex);
        SetInfosItem(pageIndex);
    }
    #endregion

    #region 右侧详情页操作
    /// <summary>
    /// 把大区详情页item生成出来
    /// </summary>
    /// <param name="list"></param>
    public void SetInfosItem(int pageIndex)
    {

        if (dic[pageIndex] != null && ServerInfosItemPrefab != null)
        {
            /*
    if (infoItemObjPool.Count >0)
    {
        for (int i = 0; i < infoItemObjPool.Count; i++)
        {
            Destroy(infoItemObjPool[i]);    //临时写法，以后改为打开和关闭
        }
        infoItemObjPool.Clear();
    }
    */
            for (int i = 0; i < infoItemObjPool.Count; i++)
            {
                if (i > dic[pageIndex].Count)
                {
                    infoItemObjPool[i].SetActive(false);
                }
            }
            //生成一个列表item的过程
            for (int i = 0; i < dic[pageIndex].Count; i++)
            {
                GameObject obj = infoItemObjPool[i];
                UIPanelServerInfosItemView view = obj.GetComponent<UIPanelServerInfosItemView>();
                if (!obj.activeSelf) obj.SetActive(true);
                if (view != null)
                {
                    view.SetUI(dic[pageIndex][i]);
                    //给item赋值点击事件
                    view.OnServerInfoItemClick = OnServerInfoItemClick;
                }
            }
        }
    }
    /// <summary>
    /// 详情页回调
    /// </summary>
    /// <param name="itemName"></param>
    private void OnServerInfoItemClick(GameServerEntity entity)
    {
        LogUtil.Log("点击了详情：" + entity.Name+","+entity.Port);
        GlobalCache.Instance.Account_LastLoginServerId = entity.Id;
        GlobalCache.Instance.Account_LastLoginServerName = entity.Name;
        GlobalCache.Instance.GameServer_CurrentIp = entity.Ip;
        GlobalCache.Instance.GameServer_CurrentPort = entity.Port;
        //实现OnInfoItemClick委托，进行传递值到EnterServerView上
        if (OnInfoItemClick != null)
        {
            OnInfoItemClick(entity);
        }

    }
    #endregion
    /// <summary>
    /// 单机版专用，把全部大区信息一波发送给客户端，供客户端本地切换使用，不再多次请求网络
    /// </summary>
    /// <param name="dic"></param>
    public void SetInfosDic(Dictionary<int, List<GameServerEntity>> gameServerEntityDic)
    {
        dic = gameServerEntityDic;
    }
    /// <summary>
    /// 设置当前选择（点击）的大区详情页
    /// </summary>
    public void SetSelectedServerInfo(GameServerEntity entity)
    {
        if (mInfoView != null)
        {
            //把这个信息返回到EnterServerView上并显示
            //mInfoView.SetUI(entity);
        }
    }

}
