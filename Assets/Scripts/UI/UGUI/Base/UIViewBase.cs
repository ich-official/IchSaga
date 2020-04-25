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
/// <summary>
/// 所有UI控件的基类，V层
/// </summary>
public class UIViewBase : MonoBehaviour {
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

    }

    private void BtnClick(GameObject obj)
    {
        OnBtnClick(obj);
    }
    protected virtual void OnBtnClick(GameObject obj) { }

    void OnDestroy()
    {
        LeoOnDestroy();
    }

    protected virtual void LeoOnDestroy()
    {

    }
}
