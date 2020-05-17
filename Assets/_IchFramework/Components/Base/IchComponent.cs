//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-17 17:30:43
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
    /// 
    /// </summary>
    public class IchComponent : MonoBehaviour
    {
        /// <summary>
        /// 组件对象的ID，每个组件的ID都不同且唯一
        /// </summary>
        private int mInstanceId;

        private void Awake()
        {
            //游戏初始化时，先注册组件
            mInstanceId = GetInstanceID();
            
            OnAwake();
        }

        protected virtual void OnAwake() { }

        public int InstanceId { get { return mInstanceId; } }

    }

}
