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
    /// ����ļ����Լ���װ����������Ļ���
    /// </summary>
    public abstract class IchComponentBase : IchComponent
    {
        protected override void OnAwake()
        {
            //�������ע��
            GameEntry.RegisterComponent(this);
            base.OnAwake();
        }
        /// <summary>
        /// �رջ������
        /// </summary>
        public abstract void Shutdown();
    }

}
