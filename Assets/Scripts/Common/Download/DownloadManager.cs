//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-02 15:21:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DownloadManager : SingletonBase<DownloadManager> {

    public const int DownloadTimeOut = 5; //超时时间
    public static string DownloadBaseUrl = @"H:\IchSagaGit\IchSaga\AssetBundles\"; //服务器上文件的路径 downloadAPI的基础地址，本项目使用本地路径进行模拟
    public const int DownloadRoutineNum = 1; //下载器的数量，模拟多线程

#if UNITY_STANDALONE_WIN
    public static string DownloadUrl = DownloadBaseUrl + "Windows/";
#elif UNITY_ANDROID || UNITY_EDITOR
    public static string DownloadUrl = DownloadBaseUrl + "Android/";
#elif UNITY_IPHONE
    public static string DownloadUrl = DownloadBaseUrl + "iOS/";
#endif
    public string LocalFilePath = Application.persistentDataPath + "/"; //下载文件的目标路径

    private List<DownloadDataEntity> mNeedDownloadDataList = new List<DownloadDataEntity>(); //需要下载的数据列表
    private List<DownloadDataEntity> mLocalDataList = new List<DownloadDataEntity>(); //本地数据列表

    private List<DownloadDataEntity> mServerDataList; //服务器端的数据列表

    private string mLoaclVersionPath;// 本地版本文件路径

    private const string mVersionFileName = "Version.txt"; //版本文件文件名

    private string mStreamingAssetsPath; //资源初始化时候 原始路径

    public Action OnInitComplete; //初始化完毕

    /// <summary>
    /// 检查版本文件，是否有更新
    /// </summary>
    public void InitCheckVersion()
    {
        string strVersionUrl = DownloadUrl + mVersionFileName; //版本文件路径，txt文件，存放版本信息的
        //TODO 读取这个txt文件
        DownloadAssetBundle.Instance.InitServerVersion(strVersionUrl, OnInitVersionCallback);
    }
    /// <summary>
    /// 版本文件下载完毕后执行此回调，功能是对比版本文件，确定是否有版本更新
    /// 把需要更新的文件添加到mNeedDownloadDataList
    /// </summary>
    /// <param name="serverDownloadedEntity"></param>
    private void OnInitVersionCallback(List<DownloadDataEntity> serverDownloadedEntity)
    {
        string mLocalVersionFile = LocalFilePath + mVersionFileName;
        if (File.Exists(mLocalVersionFile))
        {
            //本地有版本文件，则开始比对
            //服务器端数据的dic<文件名,MD5>
            Dictionary<string, string> serverDic = PackDownloadDataDic(serverDownloadedEntity);


            //读取本地版本信息
            string content = IOUtil.GetFileText(mLocalVersionFile);
            Dictionary<string, string> clientDic = PackDownloadDataDic(content);    //把本地的版本文件改装成dic
            mLocalDataList = PackDownloadData(content); //把本地的版本信息文件改装成list


            //1.开始比对，比对本地没有的文件
            for (int i = 0; i < serverDownloadedEntity.Count; i++)
            {
                if (serverDownloadedEntity[i].IsFirstData && !clientDic.ContainsKey(serverDownloadedEntity[i].FullName))    //是初始资源并且本地没有这个文件
                {
                    mNeedDownloadDataList.Add(serverDownloadedEntity[i]); //加入下载列表
                }
            }

            //2.对比本地存在的，但是有更新的资源
            foreach (var item in clientDic)
            {
                //如果MD5不一致
                if (serverDic.ContainsKey(item.Key) && serverDic[item.Key] != item.Value)
                {
                    //
                    DownloadDataEntity entity = GetDownloadData(item.Key, serverDownloadedEntity);
                    if (entity != null)
                    {
                        mNeedDownloadDataList.Add(entity);  //把资源加入需要下载的列表
                    }
                }
            }
        }
        else
        {
            //本地没有版本文件，则资源全部下载
            for (int i = 0; i < serverDownloadedEntity.Count; i++)
            {
                if (serverDownloadedEntity[i].IsFirstData)
                {
                    mNeedDownloadDataList.Add(serverDownloadedEntity[i]);
                }
            }
        }
        //进行下载
        DownloadAssetBundle.Instance.DownloadFiles(mNeedDownloadDataList);
    }

    /// <summary>
    /// 修改更新后的资源文件
    /// </summary>
    /// <param name="mCurrDownloadData"></param>
    internal void ModifyLocalData(DownloadDataEntity entity)
    {
        if (mLocalDataList == null) return;
        bool isExists = false;

        for (int i = 0; i < mLocalDataList.Count; i++)
        {
            //如果本地已存在entity.FullName文件名的资源了
            if (mLocalDataList[i].FullName.Equals(entity.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                mLocalDataList[i].MD5 = entity.MD5;
                mLocalDataList[i].Size = entity.Size;
                mLocalDataList[i].IsFirstData = entity.IsFirstData;
                isExists = true;
                break;
            }
        }

        if (!isExists)
        {
            mLocalDataList.Add(entity);
        }

        SavaLoaclVersion();

    }

    /// <summary>
    /// 保存本地的版本文件
    /// </summary>
    private void SavaLoaclVersion()
    {
        StringBuilder sbContent = new StringBuilder();

        for (int i = 0; i < mLocalDataList.Count; i++)
        {
            sbContent.AppendLine(string.Format("{0} {1} {2} {3}", mLocalDataList[i].FullName, mLocalDataList[i].MD5, mLocalDataList[i].Size, mLocalDataList[i].IsFirstData ? 1 : 0));
        }

        IOUtil.CreateTextFile(mLoaclVersionPath, sbContent.ToString());
    }

    /// <summary>
    /// 版本信息比对出需要更新的资源后，把这个资源做成Entity
    /// </summary>
    /// <param name="key"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private DownloadDataEntity GetDownloadData(string fullName, List<DownloadDataEntity> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //CurrentCultureIgnoreCase：使用区域敏感排序规则、当前区域来比较字符串，同时忽略被比较字符串的大小写。
            if (list[i].FullName.Equals(fullName, StringComparison.CurrentCultureIgnoreCase))
            {
                return list[i];
            }
        }
        return null;
    }

    /// <summary>
    /// 把服务器资源详情的list改装成dic  key：资源文件名   value：MD5
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private Dictionary<string, string> PackDownloadDataDic(List<DownloadDataEntity> list)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        for (int i = 0; i < list.Count; i++)
        {
            dic[list[i].FullName] = list[i].MD5;
        }

        return dic;
    }
    /// <summary>
    /// 根据服务器的原始version.txt改装成dic key：资源文件名   value：MD5
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private Dictionary<string, string> PackDownloadDataDic(string content)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        string[] arrLines = content.Split('\n');
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');
            if (arrData.Length == 4)
            {
                dic[arrData[0]] = arrData[1];
            }
        }
        return dic;
    }

    /// <summary>
    /// 把版本信息内需要下载的资源详情封装成Entity实体
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public List<DownloadDataEntity> PackDownloadData(string content)
    {
        List<DownloadDataEntity> lst = new List<DownloadDataEntity>();

        string[] arrLines = content.Split('\n');    //每换一行代表一条数据
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');  //自己定义的版本文件格式是空格
            if (arrData.Length == 4)    //自己定义的版本文件，每一条资源有4个属性
            {
                //把资源详情读出来
                DownloadDataEntity entity = new DownloadDataEntity();
                entity.FullName = arrData[0];   //资源名字
                entity.MD5 = arrData[1];    //资源MD5
                entity.Size = arrData[2].ToInt();   //资源大小
                entity.IsFirstData = arrData[3].ToInt() == 1;   //1：IsFirstData=true  
                lst.Add(entity);
            }
        }

        return lst;
    }




    /*
    /// <summary>
    /// 第二步：检查版本文件
    /// </summary>
    public void InitCheckVersion()
    {
        UISceneInitCtrl.Instance.SetProgress("正在检查版本更新", 0);

        string strVersionUrl = DownloadUrl + m_VersionFileName; //版本文件路径

        //读取这个文件
        AssetBundleDownload.Instance.InitServerVersion(strVersionUrl, OnInitVersionCallBack);
    }
    */

}
