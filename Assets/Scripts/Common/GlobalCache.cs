//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-05-02 03:53:20
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 临时储存一些需要全局使用的变量，使用完毕立即释放
/// </summary>
public class GlobalCache : SingletonBase<GlobalCache>
{
    public int Account_CurrentId;   //当前要登陆的账号ID
    public int Account_LastLoginServerId;   //用户上次登陆服务器的ID
    public string Account_LastLoginServerName;  //用户上次登陆服务器的名称


}
