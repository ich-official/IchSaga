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
/// <summary>
/// 玩家在城中（非战斗）的AI
/// </summary>
public class PlayerAI_City : MonoBehaviour,IRoleAI {

    public RoleController CurrentRole
    {
        get;
        set;
    }

    public void DoAI()
    {
       
    }

    public PlayerAI_City(RoleController controller)
    {
        CurrentRole = controller;
    }
}
