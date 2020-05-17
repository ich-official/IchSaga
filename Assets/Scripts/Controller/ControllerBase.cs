//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 01:28:14
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端controller的基类（不包含服务器模拟器）
/// </summary>
public class ControllerBase<T> : IDisposable where T : new()
{

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    public static T GetInstance()
    {
        if (_instance == null)
        {
            _instance = new T();
        }
        return _instance;
    }

    public virtual void Dispose()
    {

    }
    /// <summary>
    /// 打开对话框的同时显示提示信息，给按钮添加委托，需要弹出UI对话框时使用
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="okAction"></param>
    /// <param name="cancelAction"></param>
    protected void ShowDialog(string msg,DialogType type=DialogType.OK, Action okAction=null,Action cancelAction=null){
        DialogController.Instance.Show(msg,type,okAction, cancelAction);
    }
    /// <summary>
    /// 添加事件监听，可用于观察者模式派发委托时使用
    /// </summary>
    /// <param name="key"></param>
    /// <param name="handler"></param>
    protected void AddEventListener(string key,DispatcherBase<UIDisPatcher,object[],string>.OnActionHandler handler)
    {
        UIDisPatcher.Instance.AddEventListener(key, handler);
    }
    protected void RemoveEventListener(string key, DispatcherBase<UIDisPatcher, object[], string>.OnActionHandler handler)
    {
        UIDisPatcher.Instance.RemoveEventListener(key, handler);
    }

    /// <summary>
    /// 把此ClientEntityBase的list类型转成子类的list类型。
    /// </summary>
    protected List<T> ConvertEntityListToChildType<T>(List<ClientEntityBase> baseList) where T : ClientEntityBase, new()
    {
        List<T> listT = new List<T>();
        for (int i = 0; i < baseList.Count; i++)
        {
            T t = new T();
            t = (T)baseList[i];
            listT.Add(t);
        }
        return listT;
    }

}
