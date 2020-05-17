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
/// Ӳ���豸��Ϣ����
/// </summary>
public class DeviceUtil  {
    //��ȡ�豸ID
    public static string GetDeviceId()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }


    //��ȡ�豸�ͺ�
    public static string GetDeviceModel()
    {
#if UNITY_ANDROID || UNITY_STANDALONE_WIN || UNITY_EDITOR
        return SystemInfo.deviceModel;
#else 
        //iOSд��
        DeviceGeneration dg = Device.generation;
        return dg.ToString();
#endif
    }
}
