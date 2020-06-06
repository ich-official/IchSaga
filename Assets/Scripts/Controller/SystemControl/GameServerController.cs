//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using LitJson;
/// <summary>
/// 区服控制器，收发与区服相关信息的
/// </summary>
public class GameServerController : ControllerBase<GameServerController>, ISystemController
{
    private UIPanelEnterServerView mEnterServerView;
    private UIPanelSelectServerView mSelectServerView;
    private Dictionary<int, List<GameServerEntity>> dic = new Dictionary<int, List<GameServerEntity>>();

    
    #region 注册与解除监听
    public GameServerController()
    {
        AddEventListener(Constant.UIPanelEnterServerView_EnterGameButton, GameServerViewEnterGameClick);
        AddEventListener(Constant.UIPanelEnterServerView_ChangeServerButton, GameServerViewChangeServerClick);

    }

    public void Dispose()
    {
        RemoveEventListener(Constant.UIPanelEnterServerView_EnterGameButton, GameServerViewEnterGameClick);
        RemoveEventListener(Constant.UIPanelEnterServerView_ChangeServerButton, GameServerViewChangeServerClick);

    }
    #endregion


    #region 按钮点击事件
    /// <summary>
    /// 点击换区按钮的点击事件
    /// </summary>
    /// <param name="p"></param>
    private void GameServerViewChangeServerClick(object[] p)
    {
        //点击按钮，立即打开view，然后再加载数据
        OpenSelectServerView();
        mSelectServerView.OnViewLoadDone = () => {
            GetServerListHttp();    //大区列表
            GetServerInfosHttp();   //大区详情
        };
        mSelectServerView.OnInfoItemClick = OnInfoItemClick;
    }
    /// <summary>
    /// 选择了一个服务器，返回enterServer界面，并已选的数据传回来
    /// </summary>
    /// <param name="obj"></param>
    private void OnInfoItemClick(GameServerEntity obj)
    {
        mSelectServerView.SelfClose();
        mEnterServerView.SetText(obj.Name);
        ResetGameServer();  //选择一个服务器并返回后，要把大区列表重置
    }
    public void ResetGameServer()
    {
        dic.Clear();
    }

    /// <summary>
    /// 进入游戏按钮的点击事件，向服务器发送最后登陆信息
    /// </summary>
    /// <param name="p"></param>
    private void GameServerViewEnterGameClick(object[] p)
    {
        SetLastLoginInfos();
    }
    #endregion

    #region 请求网络数据

    /// <summary>
    /// 网络请求获取服务器页码
    /// </summary>
    private void GetServerListHttp()
    {
        //网络请求获取服务器页码
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;
        HttpSimulator.Instance.DoPostSingle(ServerAPI.GameServer, JsonMapper.ToJson(dic), OnGetServerListCallback);
    }
    /// <summary>
    /// 网络请求全部大区信息
    /// </summary>
    private void GetServerInfosHttp()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 2;
        HttpSimulator.Instance.DoPostSingle(ServerAPI.GameServer, JsonMapper.ToJson(dic), OnGetServerInfosCallback);

    }
    /// <summary>
    /// 发送账户最后一次登陆信息
    /// </summary>
    private void SetLastLoginInfos()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 4;
        dic["Id"] = GlobalCache.Instance.Account_CurrentId;
        dic["lastLoginServerId"] = GlobalCache.Instance.Account_LastLoginServerId;
        dic["lastLoginServerName"] = GlobalCache.Instance.Account_LastLoginServerName;
        HttpSimulator.Instance.DoPostSingle(ServerAPI.GameServer, JsonMapper.ToJson(dic), OnSetLastLoginInfosCallback);

    }


    #endregion

    #region 服务器返回网络数据
    /// <summary>
    /// 获取服务器列表回调
    /// </summary>
    /// <param name="args"></param>
    private void OnGetServerListCallback(CallbackArgs args)
    {
        if (!args.hasError)
        {
            //OpenSelectServerView();
            List<GameServerListEntity> list = ConvertEntityListToChildType<GameServerListEntity>(args.objList);
            //list.Reverse();
            mSelectServerView.SetListItem(list);
            for (int i = 0; i < list.Count; i++)
            {
                dic.Add(i, null);   //设置字典key，就是大区列表页码
            }
        }

    }

    /// <summary>
    /// 获取全部服务器详情回调
    /// </summary>
    /// <param name="args"></param>
    private void OnGetServerInfosCallback(CallbackArgs args)
    {
        if (!args.hasError)
        {
            List<GameServerEntity> list = ConvertEntityListToChildType<GameServerEntity>(args.objList);
            List<GameServerEntity> listForDic = new List<GameServerEntity>();   //加入字典中的list，每10个一组排列的形式
            int pageIndex = (list.Count%10==0)?(list.Count/10):list.Count/10+1; //计算一共有多少篇
            for (int i = 0; i < list.Count; i++)
            {
                listForDic.Add(list[i]);
                if (((i+1) % 10 == 0 && i != 0) || (i == list.Count - 1))//每10个一组，或者最后一个数据但不足10个，也列为一组
                {
                    dic[pageIndex] = listForDic;
                    pageIndex--;
                    listForDic = null;
                    listForDic = new List<GameServerEntity>();
                }
            }
            mSelectServerView.SetInfosDic(dic);
            mSelectServerView.SetInfosItem(1);
        }
    }

    private void OnSetLastLoginInfosCallback(CallbackArgs args)
    {
        if (args.hasError)
        {
            LogUtil.Log(args.errorCode + args.errorMsg);
        }
        else
        {
            LogUtil.Log(args.json);
            SceneManager.Instance.LoadMainScene();
        }
    }
    #endregion



    #region 打开/关闭界面
    public void OpenView(UIWindowType windowType)
    {
        switch (windowType)
        {
            case UIWindowType.EnterServer:
                OpenEnterServerView();
                break;
            case UIWindowType.SelectServer:
                OpenSelectServerView();
                break;
        }
    }
    /// <summary>
    /// 执行打开进入大区界面的功能，给UI赋值
    /// </summary>
    private void OpenEnterServerView()
    {
        mEnterServerView = Leo_UIWindowManager.Instance.OpenWindowUI(UIWindowType.EnterServer, true).GetComponent<UIPanelEnterServerView>();
        mEnterServerView.SetText(GlobalCache.Instance.Account_LastLoginServerName);
        //GlobalCache.Instance.Account_LastLoginServerName = null;  //这句放在登陆界面销毁时再释放
        //mEnterServerView.OnViewClose = () => { OpenRegView(); };
    
    }
    /// <summary>
    /// 执行进入选择大区界面的功能
    /// </summary>
    private void OpenSelectServerView()
    {
        mSelectServerView = Leo_UIWindowManager.Instance.OpenWindowUI(UIWindowType.SelectServer, true).GetComponent<UIPanelSelectServerView>();
        //mSelectServerView.OnViewClose = () => { OpenRegView(); };
    }

    #endregion



}
