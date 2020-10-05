//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-03 02:32:41
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制角色的各项数据,=PlayerCtrl
/// </summary>
public class RoleController : ControllerBase<RoleController>, ISystemController
{

    private UIPanelRoleMenuView mRoleMenuView;

    public void SetRoleInfoMainCity()
    {
        PlayerInfo infos = GlobalInit.Instance.myRoleInfo;
        string headPic = null;
        ClassEntity entity = ClassDBModel.Instance.Get(infos.ClassId);
        if (entity != null) headPic = entity.HeadPic;
        UIGizmosRoleInfoView.Instance.SetUI(headPic, infos.RoleNickName, infos.VIPLevel, infos.Level, infos.SumDPS,infos.CurrEnergy,infos.MaxEnergy,infos.Gem, infos.Gold); ;
    }


    public void OpenView(UIPanelType windowType)
    {
        switch (windowType)
        {
            case UIPanelType.RoleMenu:
                OpenRoleMenuView();
                break;

        }
    }


    //打开角色菜单页面
    public void OpenRoleMenuView()
    {
        mRoleMenuView = UIViewManagerNGUI.Instance.OpenWindowUI(UIPanelType.RoleMenu, true).GetComponent<UIPanelRoleMenuView>();
        mRoleMenuView.SetUI((PlayerInfo)GlobalInit.Instance.currentPlayer.currentRoleInfo);
        mRoleMenuView.SetRolePrefab(GlobalCache.Instance.CurrentRoleUIPrefab);
    }

}
