//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-15 11:29:04
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
    public class UIComponent : IchComponentBase,IUpdateComponent
    {
        protected override void OnAwake()
        {
            GameEntry.RegisterUpdate(this);
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
