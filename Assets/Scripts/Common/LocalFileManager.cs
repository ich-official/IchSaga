//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-22 02:24:34
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using UnityEngine;
using System.Collections;
using System.IO;

/// <summary>
/// 本地文件管理，把一个文件转换成字节数组，目前用于处理本地excel数据表
/// </summary>
public class LocalFileManager : SingletonBase<LocalFileManager>
{
#if UNITY_EDITOR   //编辑器调试时的路径
    public readonly string localFilePath = Application.dataPath + "/../" + "AssetBundles/";
#elif UNITY_ANDROID ||UNITY_STANDALONE_WIN  || UNITY_IPHONE
    public readonly string localFilePath = Application.persistentDataPath + "/";
#endif


    /// <summary>
    /// 把文件转化成一个字节数组，目前用于AB资源转化
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path,FileMode.Open))
        {
            buffer =new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
