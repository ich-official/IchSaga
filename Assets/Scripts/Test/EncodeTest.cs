//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-23 04:26:55
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using System.Text;

/// <summary>
/// 
/// </summary>
public class Leo_EncodeTest : MonoBehaviour {


	void Start () {
        People p = new People();
        p.str="测试test123";
        Debug.Log(p.str);
	}
}
public class People
{
    public string str;
}