//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 14:19:30
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 日志分级打印工具
/// </summary>
public class LogUtil{
    public enum LogLevel
    {
        Off=0,    //关闭打印
        Error=1,  //打印报错
        Warning=2,//打印报错、警告
        Msg=3,    //打印报错、警告、常规信息
        Test=4,   //打印报错、警告、常规信息、测试内容
    }
    public static LogLevel logLevel = LogLevel.Error;    //默认打印错误log

    public static void LogError(object message){
        if(logLevel>=LogLevel.Error)     Debug.LogError("IchSaga,Error:"+message);
    }

    public static void LogWarning(object message)
    {
        if (logLevel >= LogLevel.Warning) Debug.LogWarning("IchSaga,Warning:"+message);
    }

    public static void Log(object message)
    {
        if (logLevel >= LogLevel.Msg) Debug.Log("IchSaga,Log:"+message);
    }

    public static void LogTest(object message)
    {
        if (logLevel >= LogLevel.Test)  Debug.Log("IchSaga,Test:"+message);
    }
}
