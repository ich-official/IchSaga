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
/// 玩家战斗AI，可以用于挂机
/// </summary>
public class PlayerAI_Battle : MonoBehaviour, IRoleAI{
    public RoleBehaviour CurrentRole
    {
        get;
        set;
    }

    private float mNextAttackTime = 0; //下次攻击时间
    public PlayerAI_Battle(RoleBehaviour controller)
    {
        CurrentRole = controller;
    }

    /// <summary>
    /// 玩家AI判断条件
    /// 如果点击到了敌人：是否在攻击范围内？  1、是：攻击   2、否：移动到敌人身边
    /// 移动中判定：
    /// </summary>
    public void DoAI()
    {
        if (CurrentRole.viewedEnemy != null )
        {
            if (CurrentRole.viewedEnemy.currentRoleInfo.currentHP <= 0)
            {
                //敌人HP<0，解除锁定，不再攻击
                CurrentRole.viewedEnemy = null;
                return;
            }
            if( CurrentRole.currentPlayerFSM.currentRoleStateEnum != RoleState.Attack){

            }
            if (Vector3.Distance(CurrentRole.viewedEnemy.transform.position, CurrentRole.transform.position) <= CurrentRole.attackRange)
            {
                //攻击
                if (Time.time > mNextAttackTime )
                {
                    //2，攻击
                    CurrentRole.ToAttack();
                    mNextAttackTime = Time.time + 0.6f;
                }
            }
            else
            {
                //移动
            }
        }
    }
}
