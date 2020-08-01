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

        #region 寻路的部分
        //没有找到路径
        if (currentFSMManager.currentplayerController.astarPath == null)
        {
            currentFSMManager.currentplayerController.ToIdle();
            return;
        }
        //要去的目标点>=A*数组中存储的节点个数，说明路已经走完了。
        if(currentFSMManager.currentplayerController.astarCurrentNodeIndex>= currentFSMManager.currentplayerController.astarPath.vectorPath.Count)
        {
            currentFSMManager.currentplayerController.astarPath = null;
            currentFSMManager.currentplayerController.ToIdle();
            return;
        }

        //方向，通过A*后，每次移动方向是按节点发生变化的，每次取新节点都要重新计算
        Vector3 direction = Vector3.zero;
        //取一下A*里路径节点的向量，其中Y轴按角色的Y轴计算，取的是下一个要走的目标点的那个节点
        Vector3 tempPathNode = new Vector3(
            currentFSMManager.currentplayerController.astarPath.vectorPath[currentFSMManager.currentplayerController.astarCurrentNodeIndex].x,
            currentFSMManager.currentplayerController.gameObject.transform.position.y,
            currentFSMManager.currentplayerController.astarPath.vectorPath[currentFSMManager.currentplayerController.astarCurrentNodeIndex].z);
        //方向三步走，1、计算A*方向向量  2、单位化 3、加上速度
        direction = tempPathNode - currentFSMManager.currentplayerController.transform.position;
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

        //角色当前和目标节点的距离
        float tempDis = Vector3.Distance(currentFSMManager.currentplayerController.transform.position,tempPathNode);
        //判断是否已走到目标节点处
        if (tempDis <= direction.magnitude + 0.1f)
        {
            currentFSMManager.currentplayerController.astarCurrentNodeIndex++;//向下一个节点移动
        }
        currentFSMManager.currentplayerController.CharacterController.Move(direction);

        #endregion
        #region #Old# Move straightly is obsoleted, using A-star path finding now.
        /*
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

            currentFSMManager.currentplayerController.ToIdle();
            isRotateDone = false;
        }
        */
        #endregion


    }

    public override void OnLeaveState()
    {
        base.OnLeaveState();
        this.currentFSMManager.currentplayerController.mAnimator.SetBool(RoleAniChangeCondition.ToRun.ToString(), false);


    }
}
