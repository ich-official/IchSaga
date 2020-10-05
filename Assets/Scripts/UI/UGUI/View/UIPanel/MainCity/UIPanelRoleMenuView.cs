//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-11 11:03:10
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 主城中“角色”菜单,=RoleInfoView，合并RoleEquipView
/// </summary>
public class UIPanelRoleMenuView : UIPanelViewBase {

    [SerializeField]
    private Transform mRoleModelPoint;

    #region role params group1
    [SerializeField]
    private Text HPText;

    [SerializeField]
    private Text MPText;

    [SerializeField]
    private Text AttackText;

    [SerializeField]
    private Text DefenseText;

    [SerializeField]
    private Text HitText;   //命中

    [SerializeField]
    private Text DodgeText;    //闪避

    [SerializeField]
    private Text CriText;  //暴击

    [SerializeField]
    private Text ResText;  //抗暴

    [SerializeField]
    private Text SumDPSText;    //总战斗力
    #endregion

    #region role params group2
    [SerializeField]
    private Text PoJiaText;

    [SerializeField]
    private Text GedangText;

    [SerializeField]
    private Text FireText;  //燃烧

    [SerializeField]
    private Text IceText;   //冰冻

    [SerializeField]
    private Text LuckyText; //幸运
    #endregion



    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);


    }



    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
    }

    /// <summary>
    /// 角色信息面板给属性赋值
    /// </summary>
    /// <param name="infos"></param>
    public void SetUI(PlayerInfo infos)
    {
        HPText.SetText(infos.MaxHP.ToString());
        MPText.SetText(infos.MaxMP.ToString());
        AttackText.SetText(infos.Attack.ToString());
        DefenseText.SetText(infos.Defense.ToString());
        HitText.SetText(infos.Hit.ToString());
        DodgeText.SetText(infos.Dodge.ToString());
        CriText.SetText(infos.Cri.ToString());
        ResText.SetText(infos.Res.ToString());
        SumDPSText.SetText("战斗力："+infos.SumDPS.ToString());

        //================================
        PoJiaText.SetText("0");
        GedangText.SetText("0");
        FireText.SetText("0");
        IceText.SetText("0");
        LuckyText.SetText("0");
    }

    /// <summary>
    /// 把左侧角色prefab加载出来
    /// </summary>
    public void SetRolePrefab(GameObject targetPrefab)
    {
        GameObject currentRole = Instantiate(targetPrefab);

        currentRole.SetTransform(mRoleModelPoint,Vector3.zero,Vector3.one,new Vector3(0,180,0));
        //currentRole.layer = 5;  //5=UI，错误的用法，只能修改本层，children不能修改
        currentRole.SetLayer(5);
    }
}
