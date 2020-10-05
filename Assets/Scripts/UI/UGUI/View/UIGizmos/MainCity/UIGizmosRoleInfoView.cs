//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-02 21:51:50
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
/// 主城左上角头像上的各种信息
/// </summary>
public class UIGizmosRoleInfoView : MonoBehaviour
{

    [SerializeField]
    private Image RoleHeadImg;

    [SerializeField]
    private Text RoleNameText;

    [SerializeField]
    private Text RoleVIPText;

    [SerializeField]
    private Text RoleLevelText;

    [SerializeField]
    private Text RoleSumDPSText;

    [SerializeField]
    private Text EnergyText;

    [SerializeField]
    private Text GemText;

    [SerializeField]
    private Text GoldText;

    public static UIGizmosRoleInfoView Instance; //简易单例
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
    public void SetUI(string roleHeadImg, string roleName,int roleVIP, 
        int roleLevel, int roleSumDPS,int currEnergy,int maxEnergy,int gem,int gold)
    {
        RoleHeadImg.overrideSprite = RoleManager.Instance.LoadRoleHeadImg(roleHeadImg);
        RoleNameText.text = roleName;
        RoleVIPText.text = "V"+roleVIP.ToString();
        RoleLevelText.text = "Lv."+roleLevel.ToString();
        RoleSumDPSText.text="战:"+roleSumDPS.ToString();
        EnergyText.text = currEnergy.ToString() + " / " + maxEnergy.ToString();
        GemText.text = gem.ToString();
        GoldText.text = gold.ToString();
    }

}
