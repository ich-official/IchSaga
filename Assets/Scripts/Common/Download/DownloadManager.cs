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
/// <summary>
/// TODO:老师的工程只有一个下载任务，我需要做多个下载任务，形成队列，队列内无任务了，才真正完成检查更新功能
/// </summary>
public class DownloadManager : SingletonBase<DownloadManager> {

    public const int DownloadTimeOut = 5; //超时时间

    public const int DownloadRoutineNum = 1; //下载器的数量，模拟多线程
#if DISABLE_AB  //编辑器调试模式
    public static string DownloadBaseUrl = @"H:\IchSagaGit\IchSaga\AssetBundles\"; //服务器上文件的路径 downloadAPI的基础地址，本项目使用本地路径进行模拟（把资源预先放进手机，从服务器下载改为从手机指定路径加载）

#else   //真机模式
    public static string DownloadBaseUrl = @"file:///storage/emulated/0/IchSagaSupport/"; //服务器上文件的路径 downloadAPI的基础地址，本项目使用本地路径进行模拟（把资源预先放进手机，从服务器下载改为从手机指定路径加载）
#endif
#if UNITY_STANDALONE_WIN
    public static string DownloadUrl = DownloadBaseUrl + "Windows/";
#elif UNITY_ANDROID || UNITY_EDITOR
    public static string DownloadUrl = DownloadBaseUrl + "Android/";
#elif UNITY_IPHONE
    public static string DownloadUrl = DownloadBaseUrl + "iOS/";
#endif
    //电脑上的路径是C:/Users/Administrator/AppData/LocalLow/Ich_Official/IchSaga
    public string LocalFilePath = Application.persistentDataPath + "/"; //下载文件的目标路径

    private List<DownloadDataEntity> mNeedDownloadDataList = new List<DownloadDataEntity>(); //需要下载的数据列表
    private List<DownloadDataEntity> mLocalDataList = new List<DownloadDataEntity>(); //本地数据列表

    private List<DownloadDataEntity> mServerDataList; //服务器端的数据列表

    private string mLoaclVersionPath;// 本地版本文件路径,persist/Version.txt

    private const string mVersionFileName = "Version.txt"; //版本文件文件名

    private const string mDBFlagName = "DB.txt";    //ServerDB的文件名
    private string mStreamingAssetsPath; //资源初始化时候 原始路径
    private string mSADBPath;
    public Action OnInitComplete; //初始化完毕

    //第一步，初始化资源路径，把资源路径确定好，执行协程开始复制SA资源到persist里
    //目前有两种资源，一种是老师的常规download资源，一种是我自定义的ServerDB资源
    public void InitStreamingAsset(Action onInitComplete)
    {
        OnInitComplete = onInitComplete;
        mLoaclVersionPath = LocalFilePath + mVersionFileName;    //客户端的版本文件version.txt路径
        //判断本地是否有vertion.txt文件，以确定本地是否已经有资源
        if (File.Exists(mLoaclVersionPath))
        {
            //如果persist有version.txt 则检查更新
            Debug.Log("来自服务器(模拟器)下载");
            InitCheckVersion();
        }
        else
        {
            //如果persist没有version.txt  则从StreamingAsset里复制资源到persistentDataPath（类似Support工程里读写XML的真机测试部分）
#if UNITY_ANDROID && !UNITY_EDITOR
            mStreamingAssetsPath=Application.streamingAssetsPath + "/AssetBundles/";
#elif UNITY_EDITOR
            mStreamingAssetsPath = "file:///" + Application.streamingAssetsPath + "/AssetBundles/";
#endif

            string versionFileUrl = mStreamingAssetsPath + mVersionFileName;

            //
            GlobalInit.Instance.StartCoroutine(ReadStreamingAssetVersionFile(versionFileUrl, OnReadStreamingAssetDone));
            Debug.Log("来自SA");
        }
    }

    #region //检查AB资源更新的全套
    /// <summary>
    /// 读取StreamingAsset文件夹里的版本文件Version.txt
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <param name="onReadStreamingAssetOver"></param>
    /// <returns></returns>
    private IEnumerator ReadStreamingAssetVersionFile(string fileUrl, Action<string> OnReadStreamingAssetDone)
    {
        UIRootStartGameView.Instance.SetProgress("正在准备进行资源初始化", 0);    //显示UI
        yield return null;
        using (WWW www = new WWW(fileUrl))
        {
            yield return www;
            if (www.error == null)
            {
                if (OnReadStreamingAssetDone != null)
                {
                    OnReadStreamingAssetDone(Encoding.UTF8.GetString(www.bytes));
                }
            }
            else
            {
                OnReadStreamingAssetDone("");
            }
        }
    }

    /// <summary>
    /// StreamingAsset的Version.txt读取完毕了
    /// </summary>
    /// <param name="obj">StreamingAsset的Version.txt中的全部文本内容</param>
    private void OnReadStreamingAssetDone(string content)
    {
        GlobalInit.Instance.StartCoroutine(InitStreamingAssetList(content));
    }

    /// <summary>
    /// 真正开始把StreamingAsset的Version.txt中的资源逐条复制到persist里
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private IEnumerator InitStreamingAssetList(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            InitCheckVersion();
            yield break;
        }
        string[] arr = content.Split('\n');

        //循环复制，复制方法和之前PackDownloadData一样
        for (int i = 0; i < arr.Length; i++)
        {
            string[] arrInfo = arr[i].Split(' ');

            string fileUrl = arrInfo[0]; //短路径

            yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(mStreamingAssetsPath + fileUrl, LocalFilePath + fileUrl));

            float value = (i + 1) / (float)arr.Length;
            UIRootStartGameView.Instance.SetProgress(string.Format("初始化资源不消耗流量 {0}/{1}", i + 1, arr.Length), value);
        }

        //资源复制完毕后，再把Version.txt也复制到persist里
        yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(mStreamingAssetsPath + mVersionFileName, LocalFilePath + mVersionFileName));

        //资源复制完毕后，开始在线（模拟器）检查更新
        InitCheckVersion();

    }

    /// <summary>
    /// 解压某个资源到persist
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <param name="toPath">完整路径（带文件名）</param>
    /// <returns></returns>
    private IEnumerator AssetLoadToLocal(string fileUrl, string toPath)
    {
        Debug.Log("fileURL:" + fileUrl);
        Debug.Log("topath:" + toPath);
        using (WWW www = new WWW(fileUrl))
        {
            yield return www;
            if (www.error == null)
            {
                int lastIndexOf = toPath.LastIndexOf('\\'); //找到短路径
                if (lastIndexOf != -1)
                {
                    string localPath = toPath.Substring(0, lastIndexOf); //除去文件名以外的路径

                    if (!Directory.Exists(localPath))
                    {
                        Directory.CreateDirectory(localPath);
                    }
                }

                using (FileStream fs = File.Create(toPath, www.bytes.Length))
                {
                    fs.Write(www.bytes, 0, www.bytes.Length);
                    Debug.Log("写入了一个文件：" + fileUrl);
                    fs.Close();
                }
            }
        }
    }

    #endregion

  

    /// <summary>
    /// 第二步，检查版本文件，是否有更新
    /// </summary>
    public void InitCheckVersion()
    {
        string strVersionUrl = DownloadUrl + mVersionFileName; //服务器的版本文件路径，txt文件，存放版本信息的
        Debug.Log("模拟器路径：" + strVersionUrl);
        // 读取这个服务器的txt文件
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
            Debug.Log("正在比对服务器版本文件...");

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
            //本地没有版本文件，则从服务器上下载全部更新内容
            for (int i = 0; i < serverDownloadedEntity.Count; i++)
            {
                if (serverDownloadedEntity[i].IsFirstData)
                {
                    mNeedDownloadDataList.Add(serverDownloadedEntity[i]);
                }
            }
        }

        //拿到下载列表 mNeedDownloadDataList 进行下载，列表=0，所有资源都下载完毕
        if (mNeedDownloadDataList.Count == 0)
        {
            UIRootStartGameView.Instance.SetProgress("资源更新完毕", 1);
            if (OnInitComplete != null)
            {
                OnInitComplete();
            }
            return;
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

    #region //复制ServerDB到persist的全套

    /// <summary>
    /// 自定义方法，把StreamingAsset里的ServerDB拷贝到persist里
    /// </summary>
    public void CopyDBToPersist()
    {
        mSADBPath = Application.streamingAssetsPath + "/ServerDB/";   //SA中DB的路径
        string DBFileUrl = mSADBPath + mDBFlagName;
        string DBPersistUrl = LocalFilePath + mDBFlagName;
        //判断本地是否有DB.txt文件，有就跳过此步
        if (File.Exists(DBPersistUrl))
        {
            //如果persist有version.txt 则检查更新
            Debug.Log("本地已有DB.txt了！");
            return;
        }
        else
        {
            Debug.Log("没有DB.txt，开始解压SA资源");
            GlobalInit.Instance.StartCoroutine(ReadSADBFlagFile(DBFileUrl));
        }

    }

    /// <summary>
    /// 从SA里读DB.txt
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <param name="OnReadStreamingAssetDone"></param>
    /// <returns></returns>
    private IEnumerator ReadSADBFlagFile(string fileUrl )
    {
        yield return null;
        using (WWW www = new WWW(fileUrl))
        {
            yield return www;
            if (www.error == null)
            {
                GlobalInit.Instance.StartCoroutine(InitSADBList(Encoding.UTF8.GetString(www.bytes)));
            }
            else
            {
                GlobalInit.Instance.StartCoroutine(InitSADBList(""));
            }
        }
    }

    private IEnumerator InitSADBList(string content)
    {
        string[] arr = content.Split('\n');

        //循环复制，复制方法和之前PackDownloadData一样
        for (int i = 0; i < arr.Length; i++)
        {
            string[] arrInfo = arr[i].Split(' ');

            string fileUrl = arrInfo[0]; //短路径

            yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(mSADBPath + fileUrl, LocalFilePath + fileUrl));

            float value = (i + 1) / (float)arr.Length;
        }

        //资源复制完毕后，再把Version.txt也复制到persist里
        yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(mSADBPath + mDBFlagName, LocalFilePath + mDBFlagName));

    }


    #endregion

}
