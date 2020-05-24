using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class AssetBundleWindow : EditorWindow {
    private AssetBundleDAL mDal;
    private List<AssetBundleEntity> mList;
    private Dictionary<string, bool> mDic;

    private string[] mType = { "All", "Role", "Scene", "Effect", "Audio", "None" };
    private int typeIndex = 0;

    private string[] mBuildTarget = { "Windows", "Android", "Iphone" };


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


    public AssetBundleWindow()
    {
        string XMLPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
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
    Vector2 pos;
    void OnGUI()
    {
        if (mList == null) return;
        #region 第一行 按钮
        GUILayout.BeginHorizontal("box");

        typeIndex = EditorGUILayout.Popup(typeIndex, mType, GUILayout.Width(100));
        if (GUILayout.Button("选择资源类型", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnTypeIndexCallback;
        }

        buildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, mBuildTarget, GUILayout.Width(100));
        if (GUILayout.Button("选择平台", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnTargetIndexCallback;
        }

        if (GUILayout.Button("开始打AB包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCreateABCallback;
        }

        if (GUILayout.Button("清空AB包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnClearABCallback;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        #endregion

        #region 第二行 AB资源标题
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("  ",GUILayout.Width(20));
        GUILayout.Label("Pkg_Name",GUILayout.Width(200));
        GUILayout.Label("Tag", GUILayout.Width(100));
        GUILayout.Label("Save_Path",GUILayout.Width(200));
        GUILayout.Label("Version", GUILayout.Width(100));
        GUILayout.Label("Size", GUILayout.Width(100));
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
            GUILayout.Label(entity.ToPath, GUILayout.Width(200));
            GUILayout.Label(entity.Version, GUILayout.Width(100));
            GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));
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

    /// <summary>
    /// 思想：循环判断列表中的资源，把打勾的添加到一个list里，然后再循环这个list调用BuildAssetBundle();
    /// </summary>
    public void OnCreateABCallback()
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
            BuildAssetBundle(list[i]);
        }
        Debug.Log("build ok");
    }



    /// <summary>
    /// 打AB包具体实现过程
    /// </summary>
    /// <param name="entity"></param>
    public void BuildAssetBundle(AssetBundleEntity entity)
    {
        AssetBundleBuild[] abBuild = new AssetBundleBuild[1];
        AssetBundleBuild build = new AssetBundleBuild();

        build.assetBundleName = entity.Name;
        build.assetBundleVariant = (entity.Tag == "Scene") ? "unity3d" : "assetbundle";//资源后缀
        build.assetNames = entity.pathList.ToArray();//资源路径
        string toPath = Application.dataPath + "/../AssetBundles/" + mBuildTarget[buildTargetIndex] + entity.ToPath;
        abBuild[0] = build; //build参数设置完毕后给数组赋值
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }
        BuildPipeline.BuildAssetBundles(toPath, abBuild, BuildAssetBundleOptions.None, buildTarget);
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

}
