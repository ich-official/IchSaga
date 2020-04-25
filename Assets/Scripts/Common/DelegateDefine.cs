//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 23:47:11
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------
using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 委托汇总
/// </summary>
public class DelegateDefine : SingletonBase<DelegateDefine>
{
    //场景加载完毕时
    public Action OnSceneLoadDone;
	
}
