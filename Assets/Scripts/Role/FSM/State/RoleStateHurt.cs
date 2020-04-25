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
/// 状态机：受伤状态
/// </summary>
public class RoleStateHurt : RoleStateBase
{

    public RoleStateHurt(RoleFSMStateManager fsmManager)
        : base(fsmManager)
    {
        
    }

    public override void OnEnterState()
    {
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToHurt.ToString(), true);

        base.OnEnterState();
    }

    public override void OnExecuteState()
    {
        this.currentAniStateInfo = currentFSMManager.currentplayerController.mAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentAniStateInfo.IsName(RoleAniName.Hurt.ToString()))
        {
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), (int)RoleState.Hurt);
            if (currentAniStateInfo.normalizedTime > 1)
            {
                currentFSMManager.currentplayerController.ToFight();
            }
        }
        base.OnExecuteState();
    }

    public override void OnLeaveState()
    {
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToHurt.ToString(), false);

        base.OnLeaveState();
    }
}
