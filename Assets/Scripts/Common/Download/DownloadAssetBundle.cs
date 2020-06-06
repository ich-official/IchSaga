//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-02 15:21:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

/// <summary>
/// 下载资源的主下载器
/// </summary>
public class DownloadAssetBundle : SingletonMono<DownloadAssetBundle> {

    private string mVersionUrl;
    private Action<List<DownloadDataEntity>> mOnInitVersion;
    //下载器数量
    private DownloadABMultiThread[] mRoutine = new DownloadABMultiThread[DownloadManager.DownloadRoutineNum];   //开启几个多线程下载

    private int mRoutineIndex = 0;  //下载器编号索引

    /// <summary>
    /// 本次资源包的总大小
    /// </summary>
    public int TotalSize
    {
        get;
        private set;
    }

    /// <summary>
    /// 本次资源包的总数量
    /// </summary>
    public int TotalCount
    {
        get;
        private set;
    }
    protected override void OnStart()
    {
        base.OnStart();
        //mono类的原因，可能初始化还未完成就开始执行，避免此问题，使用携程进行。
        StartCoroutine(DownloadVersion(mVersionUrl));
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
    /// <summary>
    /// 初始化服务器的版本信息
    /// </summary>
    public void InitServerVersion(string url,Action<List<DownloadDataEntity>> OnInitVersion)
    {
        mVersionUrl = url;
        mOnInitVersion = OnInitVersion;
    }
    /// <summary>
    /// 把服务器上的版本信息文件下载下来
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private IEnumerator DownloadVersion(string url)
    {
        yield return null;
        WWW www = new WWW(url);
        float timeOut = Time.time;
        float progress = www.progress;  //下载进度

        while(www!=null && !www.isDone)
        {
            //如果进度发生变化，就把超时时间点定为目前时间
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;
            }
            //判断超时
            if ((Time.time - timeOut) >= DownloadManager.DownloadTimeOut)
            {
                LogUtil.Log("下载超时");
                yield break;
            }
        }
        yield return www;
        //确保www没有问题
        string content = "";
        if (www!=null&& www.error == null)
        {
            content = www.text;
            //Debug.Log("download ok!" + content);
            if (mOnInitVersion != null)
            {
                //把文件下载完毕后立即执行委托，委托内容是封装好的资源详情list
                mOnInitVersion(DownloadManager.Instance.PackDownloadData(content));
            }
        }
        else
        {
            Debug.Log("下载失败！" + content);
        }
    }

    /// <summary>
    /// 下载实际资源
    /// </summary>
    /// <param name="mNeedDownloadDataList"></param>
    internal void DownloadFiles(List<DownloadDataEntity> downloadList)
    {
        TotalSize = 0;
        TotalCount = 0;

        //初始化下载器
        for (int i = 0; i < mRoutine.Length; i++)
        {
            if (mRoutine[i] == null)
            {
                mRoutine[i] = gameObject.AddComponent<DownloadABMultiThread>();
            }
        }

        //循环的给下载器分配下载任务
        for (int i = 0; i < downloadList.Count; i++)
        {
            mRoutineIndex = mRoutineIndex % mRoutine.Length; //0-4

            //其中的一个下载器 给他分配一个文件
            mRoutine[mRoutineIndex].AddDownload(downloadList[i]);

            mRoutineIndex++;
            TotalSize += downloadList[i].Size;
            TotalCount++;
        }

        //让下载器开始下载
        for (int i = 0; i < mRoutine.Length; i++)
        {
            if (mRoutine[i] == null) continue;
            mRoutine[i].StartDownload();
        }
    }
}
