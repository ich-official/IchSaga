//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 23:53:06
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �ͻ���ʵ����̳еģ�����ģ����������ʱʹ��
/// </summary>
public class ClientEntityBase {
    /// <summary>
    /// �Ѵ˸����list����ת�������list���͡�
    /// </summary>
    public List<T> ConvertListToChildType<T>(List<ClientEntityBase> baseList) where T : ClientEntityBase, new()
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
