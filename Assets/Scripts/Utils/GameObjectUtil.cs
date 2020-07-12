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
/// <summary>
/// 游戏控件工具类，解决GetComponent时会引起报错的问题，没有Component时自动AddComponent
/// </summary>
public static class GameObjectUtil   {

    /// <summary>
    /// MonoBehaviour扩展方法，获取或创建组件，解决GetComponent报空问题
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T SafeGetComponent<T>(this GameObject obj) where T:MonoBehaviour
    {
        T t = obj.GetComponent<T>();
        if (t == null)
        {
            t = obj.AddComponent<T>();
        }
        return t;
    }
    /// <summary>
    /// 将monos数组里的全部对象置为null，在垃圾回收时使用
    /// </summary>
    /// <param name="monos"></param>
    public static void SetNull(this MonoBehaviour[] monos)
    {
        if (monos != null)
        {
            for (int i = 0; i < monos.Length; i++)
            {
                monos[i] = null;
            }
            monos = null;
        }
    }

    public static void SetNull(this Transform[] monos)
    {
        if (monos != null)
        {
            for (int i = 0; i < monos.Length; i++)
            {
                monos[i] = null;
            }
            monos = null;
        }
    }

    public static void SetNull(this Sprite[] monos)
    {
        if (monos != null)
        {
            for (int i = 0; i < monos.Length; i++)
            {
                monos[i] = null;
            }
            monos = null;
        }
    }
}
