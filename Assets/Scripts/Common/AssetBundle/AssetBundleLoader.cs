//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-21 22:24:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

public class AssetBundleLoader : IDisposable{

    private AssetBundle bundle = null;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shortPath">Application.dataPath + "/../" + "AssetBundles/省去这段以外的部分叫做短路径</param>
    public AssetBundleLoader(string shortPath)
    {
        string fullPath = LocalFileManager.Instance.localFilePath + shortPath;

        //1.从路径获取AB资源文件，并转成byte数组
        byte[] buffer = LocalFileManager.Instance.GetBuffer(fullPath);
        //2.把byte[]数组再转成AB资源
        bundle = AssetBundle.CreateFromMemoryImmediate(buffer);

    }

    public T LoadAssetBundle<T>(string ABName) where T:UnityEngine.Object
    {
        if(bundle==null) return default (T);
        T t= bundle.LoadAsset(ABName) as T;
        return t;
    }



    public void Dispose()
    {
        if (bundle != null)
        {
            bundle.Unload(false);
        }
    }
}
