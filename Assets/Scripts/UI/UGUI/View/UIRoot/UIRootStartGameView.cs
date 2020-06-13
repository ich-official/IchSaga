//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-07 18:01:28
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// =UISceneInitCtrl
/// </summary>
public class UIRootStartGameView : UIRootViewBase
{

    [SerializeField]
    private Text DownloadLabel; //下载进度条上面的文字
    [SerializeField]
    private Slider SliderDownloadProgress;  //下载/解压进度条

    public static UIRootStartGameView Instance;


    private void Awake()
    {
        Instance = this;
    }


    public void SetProgress(string text, float value)
    {
        DownloadLabel.text = text;
        SliderDownloadProgress.value = value;
    }
}
