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
/// �ͻ���controller�Ļ��ࣨ������������ģ������
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
    /// �򿪶Ի����ͬʱ��ʾ��ʾ��Ϣ������ť���ί�У���Ҫ����UI�Ի���ʱʹ��
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="okAction"></param>
    /// <param name="cancelAction"></param>
    protected void ShowDialog(string msg,DialogType type=DialogType.OK, Action okAction=null,Action cancelAction=null){
        DialogController.Instance.Show(msg,type,okAction, cancelAction);
    }
    /// <summary>
    /// ����¼������������ڹ۲���ģʽ�ɷ�ί��ʱʹ��
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
    /// �Ѵ�ClientEntityBase��list����ת�������list���͡�
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
