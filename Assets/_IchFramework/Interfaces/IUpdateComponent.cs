//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-17 15:06:30
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

namespace IchFramework
{

    /// <summary>
    /// 框架文件，update方法，统一接口
    /// </summary>
    public interface IUpdateComponent
    {

        int InstanceId { get; } //注册update的ID，用于解除注册时使用

        void OnUpdate();
    }

}
