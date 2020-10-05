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
/// 敌人战斗AI
/// </summary>
public class EnemyAI_Battle : MonoBehaviour,IRoleAI  {

    private float mNextPatrolTime = 0; //下次巡逻时间

    private float mNextAttackTime = 0; //下次攻击时间
    public RoleBehaviour CurrentRole
    {
        get;
        set;
    }
    public void DoAI()
    {
        //敌人死了，不执行下面的逻辑
        if (CurrentRole.currentPlayerFSM.currentRoleStateEnum == RoleState.Die) return;


        //在视野范围内没发现敌人
        if (CurrentRole.viewedEnemy == null)
        {
            //如果是fight状态（不是run状态时）
            if (CurrentRole.currentPlayerFSM.currentRoleStateEnum == RoleState.Fight)
            {
                //如果时间大于下次巡逻时间，满足以上3条，开始巡逻
                if (Time.time > mNextPatrolTime)
                {
                    mNextPatrolTime = Time.time + Random.Range(3f, 6f);
                    CurrentRole.ToRun(new Vector3(
                    CurrentRole.SpawnPosition.x + Random.Range(CurrentRole.patrolRange * -1, CurrentRole.patrolRange * 1)
                    , CurrentRole.SpawnPosition.y,
                    CurrentRole.SpawnPosition.z + Random.Range(CurrentRole.patrolRange * -1, CurrentRole.patrolRange * 1)));

                }
            }
            if (Vector3.Distance(CurrentRole.transform.position, GlobalInit.Instance.currentPlayer.transform.position) <= CurrentRole.viewRange)
            {
                CurrentRole.viewedEnemy =GlobalInit.Instance.currentPlayer;
            }
        }
        else  //视野内有敌人且距离:1、和敌人距离大于视野范围，取消锁定
                                //2、小于视野范围、小于攻击范围，开始攻击
                                //3、小于视野范围、大于攻击范围，开始追击
        {
            if (CurrentRole.viewedEnemy.currentRoleInfo.currentHP <= 0)
            {
                CurrentRole.viewedEnemy = null;
                return;
            }
            if (Vector3.Distance(CurrentRole.transform.position, GlobalInit.Instance.currentPlayer.transform.position) > CurrentRole.viewRange)
            {
                //1，取消锁定
                CurrentRole.viewedEnemy = null;
                return;
            }
            else if (Vector3.Distance(CurrentRole.transform.position, GlobalInit.Instance.currentPlayer.transform.position) <= CurrentRole.attackRange)
            {
                if (Time.time > mNextAttackTime && CurrentRole.currentPlayerFSM.currentRoleStateEnum!=RoleState.Attack)
                {
                    //2，攻击
                    CurrentRole.ToAttack();
                    mNextAttackTime = Time.time + 2f;
                }
               
            }
            else
            {
                //3，追击
                if (CurrentRole.currentPlayerFSM.currentRoleStateEnum != RoleState.Fight)
                {
                    CurrentRole.ToRun(new Vector3(CurrentRole.viewedEnemy.transform.position.x + UnityEngine.Random.Range(CurrentRole.attackRange * -1, CurrentRole.attackRange), CurrentRole.SpawnPosition.y, CurrentRole.viewedEnemy.transform.position.z + UnityEngine.Random.Range(CurrentRole.attackRange * -1, CurrentRole.attackRange)));
                }
            }
            
        }

    }

    public EnemyAI_Battle(RoleBehaviour controller)
    {
        CurrentRole = controller;
    }
    
}
