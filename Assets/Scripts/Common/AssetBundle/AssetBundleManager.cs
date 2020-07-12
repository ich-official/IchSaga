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

public class AssetBundleManager : SingletonBase<AssetBundleManager>
{
    /// <summary>
    /// 从路径中加载AB资源，通过loader读出来
    /// </summary>
    /// <param name="shortPath"></param>
    /// <param name="ABName"></param>
    /// <returns></returns>
    public GameObject LoadAB(string shortPath,string ABName)
    {
        Debug.Log("shortpath:" + shortPath);
        AssetBundleLoader loader = new AssetBundleLoader(shortPath);
        GameObject obj = loader.LoadAssetBundle<GameObject>(ABName);
        return obj;
#if DISABLE_AB

#else

#endif

    }

    public GameObject CloneAB(string shortPath, string ABName)
    {
        AssetBundleLoader loader = new AssetBundleLoader(shortPath);
        GameObject obj = loader.LoadAssetBundle<GameObject>(ABName);

        return Object.Instantiate(obj);
    }

    public AssetBundleLoaderAsync LoadABAsync(string shortPath, string ABName)
    {
        GameObject obj = new GameObject("LoadABObj");
        AssetBundleLoaderAsync async = obj.SafeGetComponent<AssetBundleLoaderAsync>();
        async.Init(shortPath, ABName);
        return async;
    }
}
