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
/// 状态机：状态基类
/// </summary>
public abstract class RoleStateBase  {
    /* 状态切换要经历几个步骤
     * 1、离开当前状态
     * 2、进入新状态
     * 3、执行新状态
     */

    public RoleFSMStateManager currentFSMManager { get; private set; }

    //当前动画状态信息
    public AnimatorStateInfo currentAniStateInfo { get; set; }
    public RoleStateBase(RoleFSMStateManager roleFSMManager) {
        currentFSMManager = roleFSMManager;
    }

    public virtual void OnEnterState() { }

    public virtual void OnExecuteState() { }

    public virtual void OnLeaveState() { }
}
