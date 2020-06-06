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

    public const int DownloadTimeOut = 5; //��ʱʱ��
    public static string DownloadBaseUrl = @"H:\IchSagaGit\IchSaga\AssetBundles\"; //���������ļ���·�� downloadAPI�Ļ�����ַ������Ŀʹ�ñ���·������ģ��
    public const int DownloadRoutineNum = 1; //��������������ģ����߳�

#if UNITY_STANDALONE_WIN
    public static string DownloadUrl = DownloadBaseUrl + "Windows/";
#elif UNITY_ANDROID || UNITY_EDITOR
    public static string DownloadUrl = DownloadBaseUrl + "Android/";
#elif UNITY_IPHONE
    public static string DownloadUrl = DownloadBaseUrl + "iOS/";
#endif
    public string LocalFilePath = Application.persistentDataPath + "/"; //�����ļ���Ŀ��·��

    private List<DownloadDataEntity> mNeedDownloadDataList = new List<DownloadDataEntity>(); //��Ҫ���ص������б�
    private List<DownloadDataEntity> mLocalDataList = new List<DownloadDataEntity>(); //���������б�

    private List<DownloadDataEntity> mServerDataList; //�������˵������б�

    private string mLoaclVersionPath;// ���ذ汾�ļ�·��

    private const string mVersionFileName = "Version.txt"; //�汾�ļ��ļ���

    private string mStreamingAssetsPath; //��Դ��ʼ��ʱ�� ԭʼ·��

    public Action OnInitComplete; //��ʼ�����

    /// <summary>
    /// ���汾�ļ����Ƿ��и���
    /// </summary>
    public void InitCheckVersion()
    {
        string strVersionUrl = DownloadUrl + mVersionFileName; //�汾�ļ�·����txt�ļ�����Ű汾��Ϣ��
        //TODO ��ȡ���txt�ļ�
        DownloadAssetBundle.Instance.InitServerVersion(strVersionUrl, OnInitVersionCallback);
    }
    /// <summary>
    /// �汾�ļ�������Ϻ�ִ�д˻ص��������ǶԱȰ汾�ļ���ȷ���Ƿ��а汾����
    /// ����Ҫ���µ��ļ���ӵ�mNeedDownloadDataList
    /// </summary>
    /// <param name="serverDownloadedEntity"></param>
    private void OnInitVersionCallback(List<DownloadDataEntity> serverDownloadedEntity)
    {
        string mLocalVersionFile = LocalFilePath + mVersionFileName;
        if (File.Exists(mLocalVersionFile))
        {
            //�����а汾�ļ�����ʼ�ȶ�
            //�����������ݵ�dic<�ļ���,MD5>
            Dictionary<string, string> serverDic = PackDownloadDataDic(serverDownloadedEntity);


            //��ȡ���ذ汾��Ϣ
            string content = IOUtil.GetFileText(mLocalVersionFile);
            Dictionary<string, string> clientDic = PackDownloadDataDic(content);    //�ѱ��صİ汾�ļ���װ��dic
            mLocalDataList = PackDownloadData(content); //�ѱ��صİ汾��Ϣ�ļ���װ��list


            //1.��ʼ�ȶԣ��ȶԱ���û�е��ļ�
            for (int i = 0; i < serverDownloadedEntity.Count; i++)
            {
                if (serverDownloadedEntity[i].IsFirstData && !clientDic.ContainsKey(serverDownloadedEntity[i].FullName))    //�ǳ�ʼ��Դ���ұ���û������ļ�
                {
                    mNeedDownloadDataList.Add(serverDownloadedEntity[i]); //���������б�
                }
            }

            //2.�Աȱ��ش��ڵģ������и��µ���Դ
            foreach (var item in clientDic)
            {
                //���MD5��һ��
                if (serverDic.ContainsKey(item.Key) && serverDic[item.Key] != item.Value)
                {
                    //
                    DownloadDataEntity entity = GetDownloadData(item.Key, serverDownloadedEntity);
                    if (entity != null)
                    {
                        mNeedDownloadDataList.Add(entity);  //����Դ������Ҫ���ص��б�
                    }
                }
            }
        }
        else
        {
            //����û�а汾�ļ�������Դȫ������
            for (int i = 0; i < serverDownloadedEntity.Count; i++)
            {
                if (serverDownloadedEntity[i].IsFirstData)
                {
                    mNeedDownloadDataList.Add(serverDownloadedEntity[i]);
                }
            }
        }
        //��������
        DownloadAssetBundle.Instance.DownloadFiles(mNeedDownloadDataList);
    }

    /// <summary>
    /// �޸ĸ��º����Դ�ļ�
    /// </summary>
    /// <param name="mCurrDownloadData"></param>
    internal void ModifyLocalData(DownloadDataEntity entity)
    {
        if (mLocalDataList == null) return;
        bool isExists = false;

        for (int i = 0; i < mLocalDataList.Count; i++)
        {
            //��������Ѵ���entity.FullName�ļ�������Դ��
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
    /// ���汾�صİ汾�ļ�
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
    /// �汾��Ϣ�ȶԳ���Ҫ���µ���Դ�󣬰������Դ����Entity
    /// </summary>
    /// <param name="key"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    private DownloadDataEntity GetDownloadData(string fullName, List<DownloadDataEntity> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            //CurrentCultureIgnoreCase��ʹ����������������򡢵�ǰ�������Ƚ��ַ�����ͬʱ���Ա��Ƚ��ַ����Ĵ�Сд��
            if (list[i].FullName.Equals(fullName, StringComparison.CurrentCultureIgnoreCase))
            {
                return list[i];
            }
        }
        return null;
    }

    /// <summary>
    /// �ѷ�������Դ�����list��װ��dic  key����Դ�ļ���   value��MD5
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
    /// ���ݷ�������ԭʼversion.txt��װ��dic key����Դ�ļ���   value��MD5
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
    /// �Ѱ汾��Ϣ����Ҫ���ص���Դ�����װ��Entityʵ��
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public List<DownloadDataEntity> PackDownloadData(string content)
    {
        List<DownloadDataEntity> lst = new List<DownloadDataEntity>();

        string[] arrLines = content.Split('\n');    //ÿ��һ�д���һ������
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');  //�Լ�����İ汾�ļ���ʽ�ǿո�
            if (arrData.Length == 4)    //�Լ�����İ汾�ļ���ÿһ����Դ��4������
            {
                //����Դ���������
                DownloadDataEntity entity = new DownloadDataEntity();
                entity.FullName = arrData[0];   //��Դ����
                entity.MD5 = arrData[1];    //��ԴMD5
                entity.Size = arrData[2].ToInt();   //��Դ��С
                entity.IsFirstData = arrData[3].ToInt() == 1;   //1��IsFirstData=true  
                lst.Add(entity);
            }
        }

        return lst;
    }




    /*
    /// <summary>
    /// �ڶ��������汾�ļ�
    /// </summary>
    public void InitCheckVersion()
    {
        UISceneInitCtrl.Instance.SetProgress("���ڼ��汾����", 0);

        string strVersionUrl = DownloadUrl + m_VersionFileName; //�汾�ļ�·��

        //��ȡ����ļ�
        AssetBundleDownload.Instance.InitServerVersion(strVersionUrl, OnInitVersionCallBack);
    }
    */

}
