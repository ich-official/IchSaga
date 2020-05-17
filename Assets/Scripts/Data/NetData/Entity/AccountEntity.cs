//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-01 12:44:10
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// �˻�ʵ��
/// </summary>
public class AccountEntity : ClientEntityBase {
    #region ʵ������
    /// <summary>
    /// ����
    /// </summary>
    public int PKValue
    {
        get { return this.Id; }
        set { this.Id = value; }
    }

    /// <summary>
    /// ���
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ״̬
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    ///�û��� 
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public string Pwd { get; set; }

    /// <summary>
    ///�ֻ� 
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public string MailAddress { get; set; }

    /// <summary>
    ///Ԫ�� 
    /// </summary>
    public int Gem { get; set; }

    /// <summary>
    ///������ 
    /// </summary>
    public short ChannelID { get; set; }

    /// <summary>
    ///����¼������Id 
    /// </summary>
    public int LastLoginServerId { get; set; }

    /// <summary>
    ///����¼���������� 
    /// </summary>
    public string LastLoginServerName { get; set; }

    /// <summary>
    ///����¼������ʱ�� 
    /// </summary>
    public DateTime LastLoginServerTime { get; set; }
    /// <summary>
    /// ����¼��������IP
    /// </summary>
    public string LastLoginServerIp { get; set; }
    /// <summary>
    /// ����¼�������Ķ˿ں�
    /// </summary>
    public int LastLoginServerPort { get; set; }
    /// <summary>
    ///����¼��ɫId 
    /// </summary>
    public int LastLoginRoleId { get; set; }

    /// <summary>
    ///����¼��ɫ���� 
    /// </summary>
    public string LastLoginRoleName { get; set; }

    /// <summary>
    ///����¼��ɫְҵId 
    /// </summary>
    public int LastLoginRoleClassId { get; set; }

    /// <summary>
    ///����ʱ�� 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///����ʱ�� 
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    ///�豸��ʶ 
    /// </summary>
    public string DeviceIdentifier { get; set; }

    /// <summary>
    ///�豸�ͺ� 
    /// </summary>
    public string DeviceModel { get; set; }

    #endregion

}
