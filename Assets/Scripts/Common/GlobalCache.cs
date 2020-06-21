//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-02 03:53:20
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// ��ʱ����һЩ��Ҫȫ��ʹ�õı�����ʹ����������ͷ�
/// </summary>
public class GlobalCache : SingletonBase<GlobalCache>
{
    #region ��¼ע��
    public int Account_CurrentId;   //��ǰҪ��½���˺�ID
    public int Account_LastLoginServerId;   //�û��ϴε�½��������ID
    public string Account_LastLoginServerName;  //�û��ϴε�½������������
    #endregion

    #region ѡ������
    public string GameServer_CurrentIp;   //��ǰѡ�������IP
    public int GameServer_CurrentPort;   //��ǰѡ��������˿ں�
    #endregion

}
