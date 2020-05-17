//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 11:55:42
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class GameServerEntity : ClientEntityBase
{
    /// <summary>
    /// ����ID
    /// </summary>
    public int Id;
    /// <summary>
    /// ��������״̬
    /// </summary>
    public int RunStatus;
    /// <summary>
    /// //�Ƿ����Ƽ���
    /// </summary>
    public bool IsRecommend;

    /// <summary>
    /// �Ƿ�������
    /// </summary>
    public bool IsNew;

    /// <summary>
    /// ������
    /// </summary>
    public string Name;
    /// <summary>
    /// IP
    /// </summary>
    public string Ip;
    /// <summary>
    /// �˿ں�
    /// </summary>
    public int Port;
}
