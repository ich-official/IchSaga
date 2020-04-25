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
/// 状态机管理器，需要把角色传进来，才能取到对应角色的状态信息
/// </summary>
public class RoleFSMStateManager  {


    
    public PlayerController currentplayerController
    {
        get;
        private set;    //private set表示只能在本类才能赋值， 不能通过调用赋值
    }
    
     
    //角色状态枚举
    public RoleState currentRoleStateEnum { get; private set; }

    private RoleStateBase currentRoleState = null;

    private Dictionary<RoleState, RoleStateBase> roleStateDic = null;
    
    /// <summary>
    /// 构造函数，实例化manager对象时，同时实例化字典，添加所有状态
    /// </summary>
    /// <param name="playercontroller"></param>
    public RoleFSMStateManager(PlayerController playercontroller)
    {
        currentplayerController = playercontroller;
        roleStateDic = new Dictionary<RoleState, RoleStateBase>();
        roleStateDic[RoleState.Idle]= new RoleStateIdle(this);
        roleStateDic[RoleState.Run] = new RoleStateRun(this);
        roleStateDic[RoleState.Attack] = new RoleStateAttack(this);
        roleStateDic[RoleState.Hurt] = new RoleStateHurt(this);
        roleStateDic[RoleState.Die] = new RoleStateDie(this);
        roleStateDic[RoleState.Fight] = new RoleStateFight(this);

        if (roleStateDic.ContainsKey(currentRoleStateEnum))
        {
            currentRoleState = roleStateDic[currentRoleStateEnum];
        }
    }



    //状态应该每帧都执行
    public void OnUpdate()
    {
        if (currentRoleState != null)
        {
            currentRoleState.OnExecuteState();
        }
    }

    /// <summary>
    /// 实现状态动画切换的功能
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(RoleState newState)
    {
        //切换状态和当前相同时，返回不做操作
        if (currentRoleStateEnum == newState) return;
        if (currentRoleState != null)
        {
            //执行状态切换三步走
            currentRoleState.OnLeaveState();
        }
            //把当前状态改成新传的值
            currentRoleStateEnum = newState;
            currentRoleState = roleStateDic[currentRoleStateEnum];

            currentRoleState.OnEnterState();
        
    }
}
