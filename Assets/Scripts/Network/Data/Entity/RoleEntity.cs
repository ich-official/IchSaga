//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-21 14:51:05
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class RoleEntity : ClientEntityBase
{
    #region ��������
    /// <summary>
    /// ����
    /// </summary>
    public int PKValue {
        get  { return this.Id;  }
        set {  this.Id = value;}
    }
    #endregion

    #region ʵ������

    /// <summary>
    /// ���
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ״̬
    /// </summary>
    public EnumEntityStatus Status { get; set; }

    /// <summary>
    ///�����ʺ�Id 
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    ///ְҵ��� 
    /// </summary>
    public int ClassId { get; set; }

    /// <summary>
    ///�ǳ� 
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///�Ա� 
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    ///�ȼ� 
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    ///�ۻ���ֵ 
    /// </summary>
    public int TotalRechargeGem { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Gem { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public int Exp { get; set; }

    /// <summary>
    ///���HP 
    /// </summary>
    public int MaxHP { get; set; }

    /// <summary>
    ///���MP 
    /// </summary>
    public int MaxMP { get; set; }

    /// <summary>
    ///��ǰHP 
    /// </summary>
    public int CurrHP { get; set; }

    /// <summary>
    ///��ǰMP 
    /// </summary>
    public int CurrMP { get; set; }

    /// <summary>
    ///������ 
    /// </summary>
    public int Attack { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public int Defense { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public int Hit { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public int Dodge { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public int Cri { get; set; }

    /// <summary>
    ///���� 
    /// </summary>
    public int Res { get; set; }

    /// <summary>
    ///�ۺ�ս���� 
    /// </summary>
    public int SumDPS { get; set; }

    /// <summary>
    ///������������ͼ��� 
    /// </summary>
    public int LastPassGameLevelId { get; set; }

    /// <summary>
    ///������������ͼ��� 
    /// </summary>
    public int LastInWorldMapId { get; set; }

    /// <summary>
    ///x_y_z_y����ת 
    /// </summary>
    public string LastInWorldMapPos { get; set; }

    /// <summary>
    ///����ʱ�� 
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///����ʱ�� 
    /// </summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>
    ///�������� 
    /// </summary>
    public int Equip_Weapon { get; set; }

    /// <summary>
    ///�������� 
    /// </summary>
    public int Equip_Pants { get; set; }

    /// <summary>
    ///�����·� 
    /// </summary>
    public int Equip_Clothes { get; set; }

    /// <summary>
    ///�������� 
    /// </summary>
    public int Equip_Belt { get; set; }

    /// <summary>
    ///�������� 
    /// </summary>
    public int Equip_Cuff { get; set; }

    /// <summary>
    ///�������� 
    /// </summary>
    public int Equip_Necklace { get; set; }

    /// <summary>
    ///����Ь 
    /// </summary>
    public int Equip_Shoe { get; set; }

    /// <summary>
    ///������ָ 
    /// </summary>
    public int Equip_Ring { get; set; }

    #endregion

}
