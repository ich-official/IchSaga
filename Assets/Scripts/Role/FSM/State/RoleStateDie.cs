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
/// 状态机：死亡状态
/// </summary>
public class RoleStateDie : RoleStateBase
{

    public RoleStateDie(RoleFSMStateManager fsmManager)
        : base(fsmManager)
    {
        
    }


    public override void OnEnterState()
    {
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToDie.ToString(), true);

        base.OnEnterState();
    }

    public override void OnExecuteState()
    {
        this.currentAniStateInfo = currentFSMManager.currentplayerController.mAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentAniStateInfo.IsName(RoleAniName.Die.ToString()))
        {
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), (int)RoleState.Die);
        }
        //如果动画播过一遍了，do sth
        if (currentAniStateInfo.normalizedTime > 1)
        {
            currentFSMManager.currentplayerController.OnRoleDie(currentFSMManager.currentplayerController);
        }
        base.OnExecuteState();
    }

    public override void OnLeaveState()
    {
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToDie.ToString(), false);

        base.OnLeaveState();
    }
}
