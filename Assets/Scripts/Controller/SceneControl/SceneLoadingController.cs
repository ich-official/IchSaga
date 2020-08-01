//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 23:47:11
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//原名LoadingSceneCtrl
/// <summary>
/// loading场景的主控制器
/// </summary>
public class SceneLoadingController : MonoBehaviour
{

    public Leo_UISceneLoadingController mLoadingController;
    private AsyncOperation mAO = null;
    private int mCurrentProcessValue = 0;
    private AssetBundleCreateRequest request;
    private AssetBundle bundle;

    void Start()
    {
        //进入loading界面，代表要加载新场景，把之前的深度数据还原
        Leo_UIDepthManager.Instance.ResetDepth();   
        //异步加载场景携程
        StartCoroutine(LoadingSceneAsync());
        //进度条归零
        mLoadingController.SetProgressValue(0);
        //46课异步累加加载后，要监听一个加载完毕的委托
        DelegateDefine.Instance.OnSceneLoadDone += OnSceneLoadDone;
    }

    void OnDestroy()
    {
        DelegateDefine.Instance.OnSceneLoadDone -= OnSceneLoadDone;
    }
    /// <summary>
    /// 下一个场景完全加载完毕后，这个loading过场就销毁自己
    /// </summary>
    public void OnSceneLoadDone()
    {
        Debug.Log("mLoadingController:"+mLoadingController);
        if (mLoadingController != null)
        {
            Destroy(mLoadingController.gameObject);
        }
    }
    private IEnumerator LoadingSceneAsync()
    {
        string sceneName = null;
        switch (ScenesManager.Instance.currentSceneName)
        {
            case SceneName.LOGIN5X:
                sceneName = "Login5x";
                break;
            case SceneName.MAINSCENE5X:
                //sceneName = "Main5x";
                sceneName = "MainScene";
                break;
            case SceneName.SELECT_ROLE:
                sceneName = "Scene_SelectRole";
                break;
        }
        //Scene_SelectRole通过AB加载，这里判断一下
        if (ScenesManager.Instance.currentSceneName.Equals(SceneName.SELECT_ROLE) || ScenesManager.Instance.currentSceneName.Equals(SceneName.MAINSCENE5X))
        {
            //string tempPath = Application.persistentDataPath + @"/download\scene\initscene\scene_selectrole.unity3d";
            StartCoroutine(Load(string.Format("Download/Scene/InitScene/{0}.unity3d", sceneName),sceneName));
#if UNITY_IOS  //开发阶段暂时不用AB加载
            mAO = Application.LoadLevelAsync(sceneName);
            //是否加载完立即进入场景（true:是  false:否，手动控制进入场景）
            mAO.allowSceneActivation = false;
            yield return mAO;
#else
            #region Old
            //StartCoroutine(Load(string.Format("Android /download/scene/initscene/{0}.unity3d", sceneName),sceneName));

            Debug.Log("通过下载AB加载！");
            /*
            AssetBundleManager.Instance.LoadABAsync(string.Format("Android/download/scene/initscene/{0}.unity3d", "scene_selectrole"), "scene_selectrole").OnABLoadComplete = (Object obj) => {
                //mAO = Application.LoadLevelAsync(sceneName);
                mAO=SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                //是否加载完立即进入场景（true:是  false:否，手动控制进入场景）
                mAO.allowSceneActivation = false;
            };
            */
            //TODO:这是网上找到的一个临时方案，解决InvalidOperationException: This method cannot be used on a streamed scene AssetBundle
            //后续老师有解决方案，等学到对应位置时再修改
            //string tempPath = @"H:\IchSagaGit\IchSaga\AssetBundles\Android\download\scene\initscene\scene_selectrole.unity3d";
            //string tempPath = @"file:///storage/emulated/0/Android/data/com.ichgame.ichsaga/files/download\scene\initscene\scene_selectrole.unity3d";
            //Debug.Log("roleselect场景路径：" + tempPath);
            //WWW www = new WWW(tempPath);
            //yield return www;
            //if (www.error == null)
            //{
            //    //获取到ab包资源
            //    AssetBundle bundle = www.assetBundle;
            //    //输出所有资源地址
            //    string[] strs = bundle.GetAllScenePaths();
            //    foreach (string str in strs) Debug.Log(str);
            //    //一步读取场景
            //    mAO = SceneManager.LoadSceneAsync(sceneName);//不需要带后缀//这里Test是我的场景名字
            //    mAO.allowSceneActivation = false;
            //    /*
            //    while (async.progress < 0.9f)
            //    {
            //        Debug.Log("场景进度  " + async.progress);
            //        yield return null;
            //    }
            //    async.allowSceneActivation = true;
            //    */
            //    yield break;
            //}else{
            //    Debug.Log("www error:"+www.error);
            //}
            #endregion
            //88课通过下载的方式加载场景，重写此部分代码
#endif

        }
        else
        {
            mAO = Application.LoadLevelAsync(sceneName);
            //是否加载完立即进入场景（true:是  false:否，手动控制进入场景）
            mAO.allowSceneActivation = false;
            yield return mAO;
        }


    }
    
    private IEnumerator Load(string path, string strSceneName)
    {
#if LOCAL_LOAD_MODE
        yield return null;
        path = path.Replace(".unity3d", "");
        mAO = Application.LoadLevelAsync(strSceneName);
        //是否加载完立即进入场景（true:是  false:否，手动控制进入场景）
        mAO.allowSceneActivation = false;

#else
        yield return null;
        string fullPath = LocalFileManager.Instance.localFilePath + "Android/" +path;
        //这是新方法，解决OnComplete报错问题
        byte[] buffer = LocalFileManager.Instance.GetBuffer(fullPath);
        request=AssetBundle.LoadFromMemoryAsync(buffer);
        yield return request;
        bundle = request.assetBundle;
        mAO = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
        mAO.allowSceneActivation = false;

        /*
        AssetBundleManager.Instance.LoadABAsync("Android/"+path, strSceneName).OnABLoadComplete = (Object obj) => {
            //mAO = Application.LoadLevelAsync(sceneName);
            mAO = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
            //是否加载完立即进入场景（true:是  false:否，手动控制进入场景）
            mAO.allowSceneActivation = false;
        };
        */
        
#endif
    }

    void Update()
    {
        if (mAO == null) return;
        int process = 0;
        //加载场景的进度最大就是0.9
        if (mAO.progress < 0.9f)
        {
            //用百分比显示
            process = Mathf.Clamp((int)mAO.progress * 100,1,100);
        }
        else
        {
            process = 100;
        }
        //刚开场两个值都可能是0，发生逻辑错误，在上方修改process最小为1
        if (mCurrentProcessValue < process)
        {
            //防卡顿平滑显示
            mCurrentProcessValue++;
        }
        else
        {
            mAO.allowSceneActivation = true;
        }
        //传回的值范围是0-1
        mLoadingController.SetProgressValue(mCurrentProcessValue*0.01f);
        
    }
     
}
