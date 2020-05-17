//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-15 11:22:32
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
    /// 框架文件，组件
    /// </summary>
    public class TimeComponent : IchComponentBase,IUpdateComponent
    {
        protected override void OnAwake()
        {
            GameEntry.RegisterUpdate(this);     //注册一个update，统一在gameEntry的update中执行
            base.OnAwake();
        }

        public void OnUpdate()
        {

        }
        public override void Shutdown()
        {

        }

    }
}