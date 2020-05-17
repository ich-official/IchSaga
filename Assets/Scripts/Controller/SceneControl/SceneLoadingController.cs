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

//原名LoadingSceneCtrl
/// <summary>
/// loading场景的主控制器
/// </summary>
public class SceneLoadingController : MonoBehaviour
{

    public Leo_UISceneLoadingController mLoadingController;
    private AsyncOperation mAO = null;
    private int mCurrentProcessValue = 0;

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
        if (mLoadingController != null)
        {
            Destroy(mLoadingController.gameObject);
        }
    }
    private IEnumerator LoadingSceneAsync()
    {
        string sceneName = null;
        switch (SceneManager.Instance.currentSceneName)
        {
            case SceneName.LOGIN5X:
                sceneName = "Login5x";
                break;
            case SceneName.MAINSCENE5X:
                sceneName = "Main5x";
                break;
        }

        mAO = Application.LoadLevelAsync(sceneName);
        //是否加载完立即进入场景（true:是  false:否，手动控制进入场景）
        mAO.allowSceneActivation = false;
        yield return mAO;
    }
    
    void Update()
    {
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
