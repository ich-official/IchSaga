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
using UnityEngine.UI;
using System;
/// <summary>
/// 所有UI控件的基类，V层
/// </summary>
public class UIViewBase : MonoBehaviour {
    public Action OnViewLoadDone;   //一个view加载完成后
    void Awake()
    {
        Button[] btns = GetComponentsInChildren<Button>(true);  //true:including hiding buttons
        for (int i = 0; i < btns.Length; i++)
        {
            EventTriggerListener.Get(btns[i].gameObject).onClick += BtnClick; //UGUI写法
        }
        OnAwake();
       
    }

    protected virtual void OnAwake()
    {

    }
    void Start()
    {

        OnStart();
    }
    protected virtual void OnStart()
    {
        if (OnViewLoadDone != null) OnViewLoadDone();
    }

    private void BtnClick(GameObject obj)
    {
        OnBtnClick(obj);
    }
    protected virtual void OnBtnClick(GameObject obj) { }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }
    /// <summary>
    /// 自定义OnDestroy方法
    /// </summary>
    protected virtual void BeforeOnDestroy()
    {

    }
}
