using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class AssetBundleWindow : EditorWindow {
    private AssetBundleDAL mDal;
    private List<AssetBundleEntity> mList;
    private Dictionary<string, bool> mDic;
    private string XMLPath;

    private string[] mType = { "All", "Role", "Scene", "Effect", "Audio", "None" };
    private int typeIndex = 0;  //AB资源类型的索引下标
    private int selectTypeIndex = -1;   //当前选择的类型的下标
    private string[] mBuildTarget = { "Windows", "Android", "Iphone" };
    private int selectBuildTargetIndex = -1;    //当前选择的平台的下标

#if UNITY_STANDALONE_WIN 
    private BuildTarget buildTarget = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0;
#elif UNITY_ANDROID
    private BuildTarget buildTarget = BuildTarget.Android;
    private int buildTargetIndex = 1;
#elif UNITY_IPHONE 
    private BuildTarget buildTarget = BuildTarget.iOS;
    private int buildTargetIndex = 2;
#endif

    public void OnEnable()
    {
        XMLPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        mDal = new AssetBundleDAL(XMLPath);
        mList = mDal.GetList();

        mDic = new Dictionary<string, bool>();
        foreach (var item in mList)
        {
            mDic[item.key] = false;
        }

        /*
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i].key);
        }
         * */
    }
    public AssetBundleWindow()
    {

    }
    Vector2 pos;
    void OnGUI()
    {
        if (mList == null) return;
        #region 第一行 按钮
        GUILayout.BeginHorizontal("box");

        selectTypeIndex = EditorGUILayout.Popup(typeIndex, mType, GUILayout.Width(100));
        //选择后立即勾选
        if (selectTypeIndex != typeIndex)
        {
            typeIndex = selectTypeIndex;
            EditorApplication.delayCall = OnTypeIndexCallback;  //选完某个type后立即执行打钩操作
        }
        /*
        if (GUILayout.Button("选择资源类型", GUILayout.Width(150)))
        {
            //EditorApplication.delayCall = OnTypeIndexCallback;
            Debug.Log("已弃用！选择后立即打勾");
        }
        */
        //选择后立即切换
        selectBuildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, mBuildTarget, GUILayout.Width(100));
        if (selectBuildTargetIndex != buildTargetIndex) {
            buildTargetIndex = selectBuildTargetIndex;
            EditorApplication.delayCall = OnTargetIndexCallback;
        }
        /*
        if (GUILayout.Button("选择平台", GUILayout.Width(150)))
        {
            //EditorApplication.delayCall = OnTargetIndexCallback;
            Debug.Log("已弃用！选择后立即打勾");
        }
        */
        if (GUILayout.Button("开始AB标签分类", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnSaveConfigCallback;
        }

        if (GUILayout.Button("开始打AB包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCreateABCallback;
        }

        if (GUILayout.Button("清空AB包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnClearABCallback;
        }
        if (GUILayout.Button("拷贝数据表", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCopyDataTableCallBack;
        }
        if (GUILayout.Button("生成版本文件", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnCreateVersionFileCallBack;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        #endregion

        #region 第二行 AB资源标题
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("  ",GUILayout.Width(20));
        GUILayout.Label("Pkg_Name",GUILayout.Width(200));
        GUILayout.Label("Tag", GUILayout.Width(100));
        GUILayout.Label("IsFolder", GUILayout.Width(200));
        GUILayout.Label("IsFirstData", GUILayout.Width(200));
        GUILayout.EndHorizontal();

        #endregion

        #region 第三行开始 具体的AB资源信息
        GUILayout.BeginVertical();
        pos=GUILayout.BeginScrollView(pos);
        for (int i = 0; i < mDic.Count; i++)
        {
            AssetBundleEntity entity = mList[i];
            GUILayout.BeginHorizontal("box");

            mDic[entity.key]=GUILayout.Toggle(mDic[entity.key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name, GUILayout.Width(200));
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(100));
           // GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();

            foreach (string path in entity.pathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }

        }
        GUILayout.EndScrollView();
        GUILayout.EndVertical();


        #endregion
    }

    /// <summary>
    /// 生成版本文件，以后每次下载资源时对比版本文件，如果MD5或者大小不同，可确定为需要更新的文件
    /// </summary>
    private void OnCreateVersionFileCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + mBuildTarget[buildTargetIndex];
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string strVersionFilePath = path + "/Version.txt"; //版本文件路径

        //如果版本文件存在 则删除
        IOUtil.DeleteFile(strVersionFilePath);

        StringBuilder sbContent = new StringBuilder();

        DirectoryInfo directory = new DirectoryInfo(path);

        //拿到文件夹下所有文件
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];
            string fullName = file.FullName; //全名 包含路径扩展名

            //相对路径
            string name = fullName.Substring(fullName.IndexOf(mBuildTarget[buildTargetIndex]) + mBuildTarget[buildTargetIndex].Length + 1);

            string md5 = EncryptUtil.GetFileMD5(fullName); //文件的MD5
            if (md5 == null) continue;

            string size = Math.Ceiling(file.Length / 1024f).ToString(); //计算文件大小

            bool isFirstData = true; //是否首次游戏必须数据
            bool isBreak = false;

            for (int j = 0; j < mList.Count; j++)
            {
                foreach (string xmlPath in mList[j].pathList)
                {
                    string tempPath = xmlPath;
                    if (xmlPath.IndexOf(".") != -1)
                    {
                        tempPath = xmlPath.Substring(0, xmlPath.IndexOf("."));
                    }
                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        isFirstData = mList[j].IsFirstData;
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak) break;
            }
            //把数据表都设置为初始数据
            if (name.IndexOf("DataTable") != -1)
            {
                isFirstData = true;
            }

            string strLine = string.Format("{0} {1} {2} {3}", name, md5, size, isFirstData ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());
        Debug.Log("创建版本文件成功");
    }

    private void OnCopyDataTableCallBack()
    {
        string fromPath = Application.dataPath + "/Download/DataTable"; //源文件目录
        string toPath = Application.dataPath + "/../AssetBundles/" + mBuildTarget[buildTargetIndex] + "/Download/DataTable";    //复制到目标文件目录
        IOUtil.CopyDirectory(fromPath, toPath);
        Debug.Log("拷贝数据表完毕");
    }

    private void OnSaveConfigCallback()
    {
        List<AssetBundleEntity> list = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in mList)
        {
            if (mDic[entity.key])
            {
                entity.IsChecked = true;
                list.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                list.Add(entity);
            }
        }
        //循环为AB资源分类
        for (int i = 0; i < list.Count; i++)
        {
            AssetBundleEntity entity = list[i];
            if (entity.IsFolder)    //如果是文件夹，则遍历里面的所有项,需要转换成绝对路径
            {
                string[] folders = new string[entity.pathList.Count];
                for(int j = 0; j < folders.Length; j++)
                {
                    folders[j] = Application.dataPath + "/" + entity.pathList[j];   //组合一个绝对路径
                }
                SaveFolderSettings(folders, !entity.IsChecked);
            }
            else    //不是文件夹，则直接操作此项
            {
                string[] folders = new string[entity.pathList.Count];
                for (int j = 0; j < folders.Length; j++)
                {
                    folders[j] = Application.dataPath + "/" + entity.pathList[j];   //组合一个绝对路径
                    SaveFileSettings(folders[j], !entity.IsChecked);
                }
                
            }
        }
    }
    /// <summary>
    /// 批量设置AB资源的分类标签
    /// </summary>
    /// <param name="folders">文件夹</param>
    /// <param name="isSetNull">是否将已分过类的资源还原成none</param>
    private void SaveFolderSettings(string[] folders, bool isSetNull)
    {
        foreach(string folderPath in folders)
        {
            //1.先看文件夹下的所有文件
            string[] files = Directory.GetFiles(folderPath);    //文件夹下的所有文件
            //2.对文件进行设置
            foreach (string filePath in files)
            {
                Debug.Log("filePath=" + filePath);
                //进行设置
                SaveFileSettings(filePath, isSetNull);
            }

            //3.看这个文件夹下的子文件夹
            string[] subFolders = Directory.GetDirectories(folderPath);
            SaveFolderSettings(subFolders, isSetNull);
        }
    }

    private void SaveFileSettings(string filePath, bool isSetNull) 
    {
        FileInfo file = new FileInfo(filePath);
        if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
        {
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
            //路径
            string newPath = filePath.Substring(index);
            Debug.Log("newPath=" + newPath);

            //文件名
            string fileName = newPath.Replace("Assets/", "").Replace(file.Extension, "");

            //后缀
            string variant = file.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity" : "assetbundle";

            AssetImporter import = AssetImporter.GetAtPath(newPath);    //获取指定路径（相对路径）下的资源的导入器
            import.SetAssetBundleNameAndVariant(fileName, variant);     //设置AB资源的文件名和扩展名
            if (isSetNull)
            {
                import.SetAssetBundleNameAndVariant(null, null);
            }
            import.SaveAndReimport();
        }
    }

    /// <summary>
    /// 将对应类型的资源打上勾
    /// </summary>
    public void OnTypeIndexCallback()
    {
        
        switch(typeIndex){
            case 0:
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.key] = true;
                }
                break;
            case 1:
                foreach (AssetBundleEntity entity in mList)
                {
                    if (entity.Tag == "Role") mDic[entity.key] = true;
                }
                break;
            case 2:
                foreach (AssetBundleEntity entity in mList)
                {
                    if (entity.Tag == "Scene") mDic[entity.key] = true;
                }
                break;
            case 3:
                foreach (AssetBundleEntity entity in mList)
                {
                    if (entity.Tag == "Effect") mDic[entity.key] = true;
                }
                break;
            case 4:
                foreach (AssetBundleEntity entity in mList)
                {
                    if (entity.Tag == "Audio") mDic[entity.key] = true;
                }
                break;
            case 5:
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.key] = false;
                }
                break;
        }
    }
    /// <summary>
    /// 选择打包的平台
    /// </summary>
    public void OnTargetIndexCallback()
    {
        switch (buildTargetIndex)
        {
            case 0:
                buildTarget = BuildTarget.StandaloneWindows;
                break;
            case 1:
                buildTarget = BuildTarget.Android;
                break;
            case 2:
                buildTarget = BuildTarget.iOS;
                break;
        }
    }


    public void OnCreateABCallback()
    {
        string toPath = Application.dataPath+"/../AssetBundles/"+ mBuildTarget[buildTargetIndex]; //AB包保存的目标路径
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }
        BuildPipeline.BuildAssetBundles(toPath,BuildAssetBundleOptions.None, buildTarget);
    }


    public void OnClearABCallback()
    {
        string path = Application.dataPath + "/../AssetBundles/" + mBuildTarget[buildTargetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path,true);
            Debug.Log("clear ok");
        }
        else{
            Debug.Log("无此路径！");
        }
    }


    #region Old
    /// <summary>
    /// 思想：循环判断列表中的资源，把打勾的添加到一个list里，然后再循环这个list调用BuildAssetBundle();
    /// 老方法，现在使用的是Pipeline一句话打包
    /// </summary>
    public void OnCreateABCallbackOld()
    {
        //循环添加要打包的资源到一个临时list里
        List<AssetBundleEntity> list = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in mList)
        {
            if (mDic[entity.key])
            {
                list.Add(entity);
            }
        }
        for (int i = 0; i < list.Count; i++)
        {
            BuildAssetBundleOld(list[i]);
        }
        Debug.Log("build ok");
    }

    /// <summary>
    /// 打AB包具体实现过程
    /// </summary>
    /// <param name="entity"></param>
    public void BuildAssetBundleOld(AssetBundleEntity entity)
    {
        AssetBundleBuild[] abBuild = new AssetBundleBuild[1];
        AssetBundleBuild build = new AssetBundleBuild();

        build.assetBundleName = entity.Name;
        build.assetBundleVariant = (entity.Tag == "Scene") ? "unity3d" : "assetbundle";//资源后缀
        build.assetNames = entity.pathList.ToArray();//资源路径
        string toPath = Application.dataPath + "/../AssetBundles/" + mBuildTarget[buildTargetIndex];
        //string toPath = "";
        abBuild[0] = build; //build参数设置完毕后给数组赋值
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }
        BuildPipeline.BuildAssetBundles(toPath, abBuild, BuildAssetBundleOptions.None, buildTarget);
    }

    #endregion
}
