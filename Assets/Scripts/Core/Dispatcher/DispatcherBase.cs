//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
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
/// 
/// </summary>
/// <typeparam name="T">继承此基类的类名</typeparam>
/// <typeparam name="P">委托里使用的参数</typeparam>
/// <typeparam name="X">字典中的key</typeparam>
public class DispatcherBase<T,P,X> : IDisposable where T:new() where P:class 
{

    #region singleton
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    #endregion

    #region     //通用格式

    public delegate void OnActionHandler(P p);
    public Dictionary<X, List<OnActionHandler>> dic = new Dictionary<X, List<OnActionHandler>>();


    public void AddEventListener(X key, OnActionHandler actionHandler)
    {
        if (dic.ContainsKey(key))
        {
            dic[key].Add(actionHandler);
        }
        else
        {
            List<OnActionHandler> list = new List<OnActionHandler>();
            list.Add(actionHandler);
            dic[key] = list;
        }
    }

    public void RemoveEventListener(X key, OnActionHandler actionHandler)
    {
        if (dic.ContainsKey(key))
        {
            List<OnActionHandler> list = dic[key];
            list.Remove(actionHandler);
            if (list.Count == 0)
            {
                dic.Remove(key);    //此ID下已经没有委托了，就把ID从字典中移除
            }
        }
    }
    /// <summary>
    /// 派发事件，通用写法
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="obj"></param>
    public void Dispatch(X key, P data)
    {
        if (dic.ContainsKey(key))//先判断有没有ID，有才能执行
        {
            //List<OnActionHandler> list = dic[eventID];    //老师单独用一个list来处理，我是直接处理元数据，目前没发现问题，有问题时再修改
            if (dic[key] != null)//判断list是否为空
            {
                for (int i = 0; i < dic[key].Count; i++)//循环执行list中的delegate
                {
                    if (dic[key][i] != null)//常规判断，delegate不为空
                    {
                        dic[key][i](data);//开始执行delegate，带上params参数
                    }
                }
            }
        }
    }

    /// <summary>
    /// 应对有params object[]参数的情况，重载一个此方法，防止没有p的时候调用报错
    /// </summary>
    /// <param name="key"></param>
    public void Dispatch(X key)
    {
        Dispatch(key, null);
    }

    #endregion

    public virtual void Dispose()
    {

    }
}
