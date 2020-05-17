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
    /// ����ļ���update������ͳһ�ӿ�
    /// </summary>
    public interface IUpdateComponent
    {

        int InstanceId { get; } //ע��update��ID�����ڽ��ע��ʱʹ��

        void OnUpdate();
    }

}
