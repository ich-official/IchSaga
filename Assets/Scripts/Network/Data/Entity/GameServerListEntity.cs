//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 11:55:35
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// �����б�ʵ��
/// </summary>
public class GameServerListEntity : ClientEntityBase
{
    /// <summary>
    /// �б�ҳ��
    /// </summary>
    public int PageIndex;
    /// <summary>
    /// �б�����������1-10����11-20��
    /// </summary>
    public string Name;
}
