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
/// 状态机：待机状态
/// </summary>
public class RoleStateIdle : RoleStateBase
{

    public RoleStateIdle(RoleFSMStateManager fsmManager)
        : base(fsmManager)
    {
        
    }
    public override void OnEnterState()
    {
        //this.currentFSMManager.currentplayerController.mAnimator.SetBool(Leo_RoleAniChangeCondition.ToIdleNormal.ToString(), true);
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToIdleNormal.ToString(), true);

        base.OnEnterState();

    }

    public override void OnExecuteState()
    {
        this.currentAniStateInfo = currentFSMManager.currentplayerController.mAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentAniStateInfo.IsName(RoleAniName.Idle_Fight.ToString()))
        {
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), (int)RoleState.Fight);
        }else if (currentAniStateInfo.IsName(RoleAniName.Idle_Normal.ToString()))
        {
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), (int)RoleState.Idle);
            this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToIdleNormal.ToString(), false);

        }
        base.OnExecuteState();
    }

    public override void OnLeaveState()
    {
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToFight.ToString(), false);

        base.OnLeaveState();
    }
}
