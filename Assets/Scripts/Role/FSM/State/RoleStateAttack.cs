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
/// 状态机：战斗状态
/// </summary>
public class RoleStateAttack : RoleStateBase
{
    public RoleStateAttack(RoleFSMStateManager fsmManager): base(fsmManager)
    {
        
    }

    public override void OnEnterState()
    {
        this.currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.ToAttack.ToString(), 1);
        if (currentFSMManager.currentplayerController.viewedEnemy != null)
        {
            currentFSMManager.currentplayerController.transform.LookAt(new Vector3(currentFSMManager.currentplayerController.viewedEnemy.transform.position.x, currentFSMManager.currentplayerController.transform.position.y, currentFSMManager.currentplayerController.viewedEnemy.transform.position.z));
        }

        base.OnEnterState();
    }

    public override void OnExecuteState()
    {
        this.currentAniStateInfo = currentFSMManager.currentplayerController.mAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentAniStateInfo.IsName(RoleAniName.PhyAttack1.ToString()))
        {
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), (int)RoleState.Attack);
            //动画执行了一遍，切换成战斗待机状态
            if (currentAniStateInfo.normalizedTime > 1)
            {
                currentFSMManager.currentplayerController.ToFight();
            }
        }else
        {
            //不是attack的状态，就设置成0，不执行动画
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), 0);
        }
        base.OnExecuteState();
    }

    public override void OnLeaveState()
    {
        this.currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.ToAttack.ToString(), 0);

        base.OnLeaveState();
    }
}
