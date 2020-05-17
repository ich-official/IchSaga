//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-22 02:24:34
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using UnityEngine;
using System.Collections;
/// <summary>
/// 场景切换类
/// </summary>
public class SceneManager : SingletonBase<SceneManager>{

    //当前场景的名字
    public SceneName currentSceneName
    {
        get;
        private set;
    }


    public void LoadLoginScene()
    {
        currentSceneName = SceneName.LOGIN5X;
        Application.LoadLevel("Loading5x");
    }
    
    public void LoadMainScene()
    {
        currentSceneName = SceneName.MAINSCENE5X;
        Application.LoadLevel("Loading5x");
    }
}
