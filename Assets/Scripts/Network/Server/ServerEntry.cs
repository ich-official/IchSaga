//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-20 01:45:10
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ģ�������Main��ڷ���
/// Ŀǰ�Ĺ����У������з�������Ҫ�õ�Э����Ӽ���
/// </summary>
public class ServerEntry : MonoBehaviour {


    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        InitAllServerController();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        ReleaseAllServerController();
    }

    /// <summary>
    /// �����з�������controller����һ�£�ͬ�������������߼�
    /// </summary>
    private void InitAllServerController()
    {
        RoleCtrlrS.Instance.Init();
    }

    private void ReleaseAllServerController()
    {
        RoleCtrlrS.Instance.MyDispose();
    }
}
