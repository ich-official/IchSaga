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
/// 状态机：奔跑状态
/// </summary>
public class RoleStateRun : RoleStateBase
{

    float rotateSpeed = 0.2f;
    Quaternion m_TargetQuat;
    bool isRotateDone;
    public RoleStateRun(RoleFSMStateManager fsmManager)
        : base(fsmManager)
    {
        
    }
    public override void OnEnterState()
    {
        base.OnEnterState();
        currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToRun.ToString(), true);

    }

    public override void OnExecuteState()
    {
        base.OnExecuteState();
        this.currentAniStateInfo = currentFSMManager.currentplayerController.mAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentAniStateInfo.IsName(RoleAniName.Run.ToString()))
        {
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), (int)RoleState.Run);
        }
        else
        {
            //不是run的状态，就设置成0，不让run
            currentFSMManager.currentplayerController.mAnimator.SetInteger(RoleAniChangeCondition.CurrentState.ToString(), 0);
        }
        if (Vector3.Distance(
            new Vector3(currentFSMManager.currentplayerController.targetPos.x,
                        0, 
                        currentFSMManager.currentplayerController.targetPos.z), 
            new Vector3(currentFSMManager.currentplayerController.transform.position.x,
                        0,
                        currentFSMManager.currentplayerController.transform.position.z)) > 0.1f)
        {
            Vector3 direction = currentFSMManager.currentplayerController.targetPos - currentFSMManager.currentplayerController.transform.position;
            direction = direction.normalized;
            direction = direction * Time.deltaTime * currentFSMManager.currentplayerController.moveSpeed;
            direction.y = 0;


            if (!isRotateDone)
            {
                rotateSpeed += 2.6f;
                m_TargetQuat = Quaternion.LookRotation(direction); //rotate slowly
                currentFSMManager.currentplayerController.transform.rotation = Quaternion.Lerp(currentFSMManager.currentplayerController.transform.rotation, m_TargetQuat, rotateSpeed * Time.deltaTime);
                if (Quaternion.Angle(m_TargetQuat, currentFSMManager.currentplayerController.transform.rotation) < 1f)
                {
                    rotateSpeed = 0;
                }
            }

            currentFSMManager.currentplayerController.CharacterController.Move(direction);
        }
        //移动结束，把动画状态切回Idle状态
        else
        {
            currentFSMManager.currentplayerController.ToFight();
            isRotateDone = false;
        }

    }

    public override void OnLeaveState()
    {
        base.OnLeaveState();
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToRun.ToString(), false);


    }
}
