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
/// 角色AI接口
/// </summary>
public interface IRoleAI  {

    /// <summary>
    /// 当前控制的角色（主角、敌军、友军、其他等）
    /// </summary>
    PlayerController CurrentRole
    {
        get;
        set;
    }

    void DoAI();
}
