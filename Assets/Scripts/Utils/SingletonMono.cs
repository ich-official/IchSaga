//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-06 16:27:06
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 使用mono的单例
/// </summary>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    #region 单例
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                DontDestroyOnLoad(obj);
                instance = obj.SafeGetComponent<T>();
            }
            return instance;
        }
    }
    #endregion

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void BeforeOnDestroy() { }


}