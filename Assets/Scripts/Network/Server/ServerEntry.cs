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
/// 模拟服务器Main入口方法
/// 目前的功能有：把所有服务器需要用的协议添加监听
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
    /// 把所有服务器的controller加载一下，同真正服务器的逻辑
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
