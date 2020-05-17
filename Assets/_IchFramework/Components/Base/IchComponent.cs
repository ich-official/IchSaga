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
        /// ��������ID��ÿ�������ID����ͬ��Ψһ
        /// </summary>
        private int mInstanceId;

        private void Awake()
        {
            //��Ϸ��ʼ��ʱ����ע�����
            mInstanceId = GetInstanceID();
            
            OnAwake();
        }

        protected virtual void OnAwake() { }

        public int InstanceId { get { return mInstanceId; } }

    }

}
