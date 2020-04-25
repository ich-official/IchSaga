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
    /// <summary>
    /// 角色字典
    /// </summary>
  //  Dictionary<string, GameObject> mRoleDic = new Dictionary<string, GameObject>();

    public GameObject LoadRole(string name)
    {
        return ResourcesManager.Instance.Load(ResourcesManager.ResourceType.ROLE, name, true);

    }
   
    public override void Dispose()
    {

        base.Dispose();
    }
}
