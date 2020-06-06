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
        //StartCoroutine(LoadLoginScene()); 先检查版本更新
        //检查版本更新
        DownloadManager.Instance.InitCheckVersion();
    }
    private IEnumerator LoadLoginScene()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.Instance.LoadLoginScene();
    }
}
