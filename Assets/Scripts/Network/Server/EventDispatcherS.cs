//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-19 21:14:49
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 模仿服务器上事件派发观察者
/// </summary>
public class EventDispatcherS : SingletonBase<EventDispatcherS>
{
    //委托原型
    public delegate void OnActionHandler(RoleS role, byte[] buffer);

    //委托字典
    private Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void AddEventListener(ushort protoCode, OnActionHandler handler)
    {
        if (dic.ContainsKey(protoCode))
        {
            dic[protoCode].Add(handler);
        }
        else
        {
            List<OnActionHandler> lstHandler = new List<OnActionHandler>();
            lstHandler.Add(handler);
            dic[protoCode] = lstHandler;
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    //移除监听
    public void RemoveEventListener(ushort protoCode, OnActionHandler handler)
    {
        if (dic.ContainsKey(protoCode))
        {
            List<OnActionHandler> lstHandler = dic[protoCode];
            lstHandler.Remove(handler);
            if (lstHandler.Count == 0)
            {
                dic.Remove(protoCode);
            }
        }
    }

    /// <summary>
    /// 派发协议
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="param"></param>
    public void Dispatch(ushort protoCode, RoleS role, byte[] buffer)
    {
        if (dic.ContainsKey(protoCode))
        {
            List<OnActionHandler> lstHandler = dic[protoCode];
            if (lstHandler != null && lstHandler.Count > 0)
            {
                for (int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        lstHandler[i](role, buffer);
                    }
                }
            }
        }
    }
}
