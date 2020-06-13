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
/// <summary>
/// 启动游戏界面的主控制器，=InitSceneCtrl
/// </summary>
public class SceneStartGameController : MonoBehaviour {
  
    void Start()
    {
#if DISABLE_AB  //调试模式
        StartCoroutine(LoadLoginScene()); //禁用AB时为本地调试模式，把Download下场景都开启
        DownloadManager.Instance.CopyDBToPersist();
        //DownloadManager.Instance.InitStreamingAsset(OnInitComplete);
#else
        //不禁用AB，正式打包模式，检查版本更新
        DownloadManager.Instance.CopyDBToPersist();     //把XML模拟数据库放进persist里
        DownloadManager.Instance.InitStreamingAsset(OnInitComplete);
#endif

    }
    private IEnumerator LoadLoginScene()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.Instance.LoadLoginScene();
    }

    /// <summary>
    /// 初始化完毕后，再跳转场景
    /// </summary>
    private void OnInitComplete()
    {
        StartCoroutine(LoadLoginScene());
    }


}
