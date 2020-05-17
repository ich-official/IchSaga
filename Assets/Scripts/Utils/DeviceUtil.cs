//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 14:16:00
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 硬件设备信息工具
/// </summary>
public class DeviceUtil  {
    //获取设备ID
    public static string GetDeviceId()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }


    //获取设备型号
    public static string GetDeviceModel()
    {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR
        return SystemInfo.deviceModel;
#else 
        //iOS写法
        DeviceGeneration dg = Device.generation;
        return dg.ToString();
#endif
    }
}
