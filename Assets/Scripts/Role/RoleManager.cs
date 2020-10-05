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
                GameObject myRoleObj = Object.Instantiate(GlobalInit.Instance.mClassDic[GlobalInit.Instance.myRoleInfo.ClassId]);//excel里编号从1开始，xml数据库保持和xls一致
                myRoleObj.transform.eulerAngles = new Vector3(0, 90, 0);
                Object.DontDestroyOnLoad(myRoleObj);
                GlobalInit.Instance.currentPlayer = myRoleObj.GetComponent<RoleBehaviour>();
                Debug.Log("currentPlayer:"+ GlobalInit.Instance.currentPlayer);

                GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, GlobalInit.Instance.myRoleInfo, new PlayerAI_Battle(GlobalInit.Instance.currentPlayer));
            }
            isInitMyRole = true;

        }
    }


    public GameObject LoadRole(string name)
    {
        return ResourcesManager.Instance.Load(ResourcesManager.ResourceType.ROLE, name, true);

    }

    public Sprite LoadRoleHeadImg(string HeadImgName)
    {
        return GameUtil.LoadSprite(Constant.PATH_HeadImg+ HeadImgName);
        //return Resources.Load(string.Format("UIPrefabs/UIResources/HeadImg/{0}", HeadImgName), typeof(Sprite)) as Sprite;
    }
   
    public override void Dispose()
    {

        base.Dispose();
    }
}
