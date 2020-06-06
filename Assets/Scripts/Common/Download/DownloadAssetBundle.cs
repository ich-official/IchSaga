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
/// ������Դ����������
/// </summary>
public class DownloadAssetBundle : SingletonMono<DownloadAssetBundle> {

    private string mVersionUrl;
    private Action<List<DownloadDataEntity>> mOnInitVersion;
    //����������
    private DownloadABMultiThread[] mRoutine = new DownloadABMultiThread[DownloadManager.DownloadRoutineNum];   //�����������߳�����

    private int mRoutineIndex = 0;  //�������������

    /// <summary>
    /// ������Դ�����ܴ�С
    /// </summary>
    public int TotalSize
    {
        get;
        private set;
    }

    /// <summary>
    /// ������Դ����������
    /// </summary>
    public int TotalCount
    {
        get;
        private set;
    }
    protected override void OnStart()
    {
        base.OnStart();
        //mono���ԭ�򣬿��ܳ�ʼ����δ��ɾͿ�ʼִ�У���������⣬ʹ��Я�̽��С�
        StartCoroutine(DownloadVersion(mVersionUrl));
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }
    /// <summary>
    /// ��ʼ���������İ汾��Ϣ
    /// </summary>
    public void InitServerVersion(string url,Action<List<DownloadDataEntity>> OnInitVersion)
    {
        mVersionUrl = url;
        mOnInitVersion = OnInitVersion;
    }
    /// <summary>
    /// �ѷ������ϵİ汾��Ϣ�ļ���������
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private IEnumerator DownloadVersion(string url)
    {
        yield return null;
        WWW www = new WWW(url);
        float timeOut = Time.time;
        float progress = www.progress;  //���ؽ���

        while(www!=null && !www.isDone)
        {
            //������ȷ����仯���Ͱѳ�ʱʱ��㶨ΪĿǰʱ��
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;
            }
            //�жϳ�ʱ
            if ((Time.time - timeOut) >= DownloadManager.DownloadTimeOut)
            {
                LogUtil.Log("���س�ʱ");
                yield break;
            }
        }
        yield return www;
        //ȷ��wwwû������
        string content = "";
        if (www!=null&& www.error == null)
        {
            content = www.text;
            //Debug.Log("download ok!" + content);
            if (mOnInitVersion != null)
            {
                //���ļ�������Ϻ�����ִ��ί�У�ί�������Ƿ�װ�õ���Դ����list
                mOnInitVersion(DownloadManager.Instance.PackDownloadData(content));
            }
        }
        else
        {
            Debug.Log("����ʧ�ܣ�" + content);
        }
    }

    /// <summary>
    /// ����ʵ����Դ
    /// </summary>
    /// <param name="mNeedDownloadDataList"></param>
    internal void DownloadFiles(List<DownloadDataEntity> downloadList)
    {
        TotalSize = 0;
        TotalCount = 0;

        //��ʼ��������
        for (int i = 0; i < mRoutine.Length; i++)
        {
            if (mRoutine[i] == null)
            {
                mRoutine[i] = gameObject.AddComponent<DownloadABMultiThread>();
            }
        }

        //ѭ���ĸ�������������������
        for (int i = 0; i < downloadList.Count; i++)
        {
            mRoutineIndex = mRoutineIndex % mRoutine.Length; //0-4

            //���е�һ�������� ��������һ���ļ�
            mRoutine[mRoutineIndex].AddDownload(downloadList[i]);

            mRoutineIndex++;
            TotalSize += downloadList[i].Size;
            TotalCount++;
        }

        //����������ʼ����
        for (int i = 0; i < mRoutine.Length; i++)
        {
            if (mRoutine[i] == null) continue;
            mRoutine[i].StartDownload();
        }
    }
}
