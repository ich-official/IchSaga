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
/// �������������շ������������Ϣ��
/// </summary>
public class GameServerController : ControllerBase<GameServerController>, ISystemController
{
    private UIPanelEnterServerView mEnterServerView;
    private UIPanelSelectServerView mSelectServerView;
    private Dictionary<int, List<GameServerEntity>> dic = new Dictionary<int, List<GameServerEntity>>();

    
    #region ע����������
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


    #region ��ť����¼�
    /// <summary>
    /// ���������ť�ĵ���¼�
    /// </summary>
    /// <param name="p"></param>
    private void GameServerViewChangeServerClick(object[] p)
    {
        //�����ť��������view��Ȼ���ټ�������
        OpenSelectServerView();
        mSelectServerView.OnViewLoadDone = () => {
            GetServerListHttp();    //�����б�
            GetServerInfosHttp();   //��������
        };
        mSelectServerView.OnInfoItemClick = OnInfoItemClick;
    }
    /// <summary>
    /// ѡ����һ��������������enterServer���棬����ѡ�����ݴ�����
    /// </summary>
    /// <param name="obj"></param>
    private void OnInfoItemClick(GameServerEntity obj)
    {
        mSelectServerView.SelfClose();
        mEnterServerView.SetText(obj.Name);
        ResetGameServer();  //ѡ��һ�������������غ�Ҫ�Ѵ����б�����
    }
    public void ResetGameServer()
    {
        dic.Clear();
    }

    /// <summary>
    /// ������Ϸ��ť�ĵ���¼������������������½��Ϣ
    /// </summary>
    /// <param name="p"></param>
    private void GameServerViewEnterGameClick(object[] p)
    {
        SetLastLoginInfos();
    }
    #endregion

    #region ������������

    /// <summary>
    /// ���������ȡ������ҳ��
    /// </summary>
    private void GetServerListHttp()
    {
        //���������ȡ������ҳ��
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;
        HttpSimulator.Instance.DoPostSingle(ServerAPI.GameServer, JsonMapper.ToJson(dic), OnGetServerListCallback);
    }
    /// <summary>
    /// ��������ȫ��������Ϣ
    /// </summary>
    private void GetServerInfosHttp()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 2;
        HttpSimulator.Instance.DoPostSingle(ServerAPI.GameServer, JsonMapper.ToJson(dic), OnGetServerInfosCallback);

    }
    /// <summary>
    /// �����˻����һ�ε�½��Ϣ
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

    #region ������������������
    /// <summary>
    /// ��ȡ�������б�ص�
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
                dic.Add(i, null);   //�����ֵ�key�����Ǵ����б�ҳ��
            }
        }

    }

    /// <summary>
    /// ��ȡȫ������������ص�
    /// </summary>
    /// <param name="args"></param>
    private void OnGetServerInfosCallback(CallbackArgs args)
    {
        if (!args.hasError)
        {
            List<GameServerEntity> list = ConvertEntityListToChildType<GameServerEntity>(args.objList);
            List<GameServerEntity> listForDic = new List<GameServerEntity>();   //�����ֵ��е�list��ÿ10��һ�����е���ʽ
            int pageIndex = (list.Count%10==0)?(list.Count/10):list.Count/10+1; //����һ���ж���ƪ
            for (int i = 0; i < list.Count; i++)
            {
                listForDic.Add(list[i]);
                if (((i+1) % 10 == 0 && i != 0) || (i == list.Count - 1))//ÿ10��һ�飬�������һ�����ݵ�����10����Ҳ��Ϊһ��
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



    #region ��/�رս���
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
    /// ִ�д򿪽����������Ĺ��ܣ���UI��ֵ
    /// </summary>
    private void OpenEnterServerView()
    {
        mEnterServerView = Leo_UIWindowManager.Instance.OpenWindowUI(UIWindowType.EnterServer, true).GetComponent<UIPanelEnterServerView>();
        mEnterServerView.SetText(GlobalCache.Instance.Account_LastLoginServerName);
        //GlobalCache.Instance.Account_LastLoginServerName = null;  //�����ڵ�½��������ʱ���ͷ�
        //mEnterServerView.OnViewClose = () => { OpenRegView(); };
    
    }
    /// <summary>
    /// ִ�н���ѡ���������Ĺ���
    /// </summary>
    private void OpenSelectServerView()
    {
        mSelectServerView = Leo_UIWindowManager.Instance.OpenWindowUI(UIWindowType.SelectServer, true).GetComponent<UIPanelSelectServerView>();
        //mSelectServerView.OnViewClose = () => { OpenRegView(); };
    }

    #endregion



}
