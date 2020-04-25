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
/// ��־�ּ���ӡ����
/// </summary>
public class LogUtil{
    public enum LogLevel
    {
        Off=0,    //�رմ�ӡ
        Error=1,  //��ӡ����
        Warning=2,//��ӡ��������
        Msg=3,    //��ӡ�������桢������Ϣ
        Test=4,   //��ӡ�������桢������Ϣ����������
    }
    public static LogLevel logLevel = LogLevel.Error;    //Ĭ�ϴ�ӡ����log

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
