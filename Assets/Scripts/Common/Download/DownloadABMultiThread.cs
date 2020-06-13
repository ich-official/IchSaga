using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DownloadABMultiThread : MonoBehaviour {

    #region 变量与属性
    private List<DownloadDataEntity> mList = new List<DownloadDataEntity>(); //需要下载的文件列表

    private DownloadDataEntity mCurrDownloadData; //当前正在下载的数据

    private int mDownloadTotalSize; //已经下载好的文件的总大小
    private int mCurrDownloadSize; //当前下载的文件大小
    /// <summary>
    /// 需要下载的数量
    /// </summary>
    public int NeedDownloadCount
    {
        private set;
        get;
    }

    /// <summary>
    /// 已经下载完成的数量
    /// </summary>
    public int CompleteCount
    {
        private set;
        get;
    }

    /// <summary>
    /// 当前下载器已经下载的大小
    /// </summary>
    public int DownloadSize
    {
        get { return mDownloadTotalSize + mCurrDownloadSize; }
    }

    /// <summary>
    /// 是否开始下载
    /// </summary>
    public bool IsStartDownload
    {
        private set;
        get;
    }

    #endregion

    void Update()
    {
        if (IsStartDownload)
        {
            IsStartDownload = false;
            StartCoroutine(DownloadData());
        }
    }


    public void AddDownload(DownloadDataEntity entity)
    {
        mList.Add(entity);
    }

    public void StartDownload()
    {
        IsStartDownload = true;
        NeedDownloadCount = mList.Count;

    }


    private IEnumerator DownloadData()
    {
        if (NeedDownloadCount == 0) yield break;
        mCurrDownloadData = mList[0];   //每次总是取列表里第一个去下载，类似栈结构
        string dataUrl = DownloadManager.DownloadUrl + mCurrDownloadData.FullName;
        //短路径 用于创建文件夹
        string shortPath = mCurrDownloadData.FullName.Substring(0, mCurrDownloadData.FullName.LastIndexOf('\\'));

        //得到本地路径
        string localFilePath = DownloadManager.Instance.LocalFilePath + shortPath;
        if (!Directory.Exists(localFilePath))
        {
            Directory.CreateDirectory(localFilePath);
        }

        WWW www = new WWW(dataUrl);

        float timeout = Time.time;
        float progress = www.progress;

        while (www != null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeout = Time.time;
                progress = www.progress;

                mCurrDownloadSize = (int)(mCurrDownloadData.Size * progress);
            }
            if ((Time.time - timeout) > DownloadManager.DownloadTimeOut)
            {
                Debug.LogError("下载失败 超时");
                yield break;
            }

            yield return null; //一定要等一帧，防止卡死

        }
        yield return www;

        if (www != null && www.error == null)
        {
            using (FileStream fs = new FileStream(DownloadManager.Instance.LocalFilePath + mCurrDownloadData.FullName, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(www.bytes, 0, www.bytes.Length);   //把文件写到本地路径中
            }
        }

        yield return new WaitForSeconds(0.5f);  //每下载完成一个资源，就等待0.5秒
        //下载成功
        mCurrDownloadSize = 0;  //重置当前下载文件的大小
        mDownloadTotalSize += mCurrDownloadData.Size;    //加一下总大小
        //把新资源覆盖到本地
        DownloadManager.Instance.ModifyLocalData(mCurrDownloadData);

        mList.RemoveAt(0);  //下载完毕的文件移出待下载的列表
        CompleteCount++;


        if (mList.Count == 0)
        {
            //列表没有文件了，资源下载完毕
            mList.Clear();
        }
        else
        {
            IsStartDownload = true;
        }
    }
}