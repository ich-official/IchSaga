using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DownloadABMultiThread : MonoBehaviour {

    #region ����������
    private List<DownloadDataEntity> mList = new List<DownloadDataEntity>(); //��Ҫ���ص��ļ��б�

    private DownloadDataEntity mCurrDownloadData; //��ǰ�������ص�����

    private int mDownloadTotalSize; //�Ѿ����غõ��ļ����ܴ�С
    private int mCurrDownloadSize; //��ǰ���ص��ļ���С
    /// <summary>
    /// ��Ҫ���ص�����
    /// </summary>
    public int NeedDownloadCount
    {
        private set;
        get;
    }

    /// <summary>
    /// �Ѿ�������ɵ�����
    /// </summary>
    public int CompleteCount
    {
        private set;
        get;
    }

    /// <summary>
    /// ��ǰ�������Ѿ����صĴ�С
    /// </summary>
    public int DownloadSize
    {
        get { return mDownloadTotalSize + mCurrDownloadSize; }
    }

    /// <summary>
    /// �Ƿ�ʼ����
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
        mCurrDownloadData = mList[0];   //ÿ������ȡ�б����һ��ȥ���أ�����ջ�ṹ
        string dataUrl = DownloadManager.DownloadUrl + mCurrDownloadData.FullName;
        //��·�� ���ڴ����ļ���
        string shortPath = mCurrDownloadData.FullName.Substring(0, mCurrDownloadData.FullName.LastIndexOf('\\'));

        //�õ�����·��
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
                Debug.LogError("����ʧ�� ��ʱ");
                yield break;
            }

            yield return null; //һ��Ҫ��һ֡����ֹ����

        }
        yield return www;

        if (www != null && www.error == null)
        {
            using (FileStream fs = new FileStream(DownloadManager.Instance.LocalFilePath + mCurrDownloadData.FullName, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(www.bytes, 0, www.bytes.Length);   //���ļ�д������·����
            }
        }

        yield return new WaitForSeconds(0.5f);  //ÿ�������һ����Դ���͵ȴ�0.5��
        //���سɹ�
        mCurrDownloadSize = 0;  //���õ�ǰ�����ļ��Ĵ�С
        mDownloadTotalSize += mCurrDownloadData.Size;    //��һ���ܴ�С
        //������Դ���ǵ�����
        DownloadManager.Instance.ModifyLocalData(mCurrDownloadData);

        mList.RemoveAt(0);  //������ϵ��ļ��Ƴ������ص��б�
        CompleteCount++;


        if (mList.Count == 0)
        {
            //�б�û���ļ��ˣ���Դ�������
            mList.Clear();
        }
        else
        {
            IsStartDownload = true;
        }
    }
}