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
using System.Collections.Generic;
/// <summary>
/// 角色控制器，load角色的功能
/// </summary>
public class RoleManager : SingletonBase<RoleManager>
{

    bool isInitMyRole=false;  //是否已经初始化了我的角色
    public void InitMyRole()
    {
        if (!isInitMyRole)
        {
            if (GlobalInit.Instance.myRoleInfo != null)
            {
                GameObject myRoleObj = Object.Instantiate(GlobalInit.Instance.mClassDic[GlobalInit.Instance.myRoleInfo.ClassId+1]);//excel里编号从1开始，这里+1，下标和excel保持一致
                Object.DontDestroyOnLoad(myRoleObj);
                GlobalInit.Instance.currentPlayer = myRoleObj.GetComponent<RoleController>();
                GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, GlobalInit.Instance.myRoleInfo, new PlayerAI_Battle(GlobalInit.Instance.currentPlayer));
            }
            isInitMyRole = true;

        }
    }


    public GameObject LoadRole(string name)
    {
        return ResourcesManager.Instance.Load(ResourcesManager.ResourceType.ROLE, name, true);

    }
   
    public override void Dispose()
    {

        base.Dispose();
    }
}
