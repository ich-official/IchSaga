//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-11 12:42:15
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
    /// 框架文件，自己封装的所有组件的基类
    /// </summary>
    public abstract class IchComponentBase : IchComponent
    {
        protected override void OnAwake()
        {
            //基础组件注册
            GameEntry.RegisterComponent(this);
            base.OnAwake();
        }
        /// <summary>
        /// 关闭基础组件
        /// </summary>
        public abstract void Shutdown();
    }

}
