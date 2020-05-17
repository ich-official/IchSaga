//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// 单例类，使用单例时都可继承此类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBase<T> : IDisposable where T : new(){

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
}
