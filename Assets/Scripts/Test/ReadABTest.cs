using UnityEngine;
using System.Collections;

public class Leo_ReadABTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        /*
        #region 做实验时的代码
        //第一步，从路径获取AB资源文件，并转成byte数组
        byte[] buffer = Leo_LocalFileManager.Instance.GetBuffer(@"H:\UnityProjectSVN\SnowWorldBattle_Beta\AssetBundles\Android\Role\role_mainplayer.assetbundle");
        //老师的版本unity5.3，方法名叫LoadFromMemory()
        //第二步，把byte[]数组再转成AB资源
        AssetBundle bundle = AssetBundle.CreateFromMemoryImmediate(buffer);
        //第三步，把AB资源转换成obj，同时把AB资源卸载掉
        GameObject obj =bundle.LoadAsset("Role_MainPlayer") as GameObject;
        bundle.Unload(false);
        //第四步，把obj生成到场景中
        Instantiate(obj);
        #endregion
        */
        //封装以上代码后，下列写法功能和上方相同
        //TestLoader();
        //建立AssetBundleManager类以后，使用时有更简洁的写法
        //TestLoaderUpdate(@"\Android\Role\role_mainplayer.assetbundle", "Role_MainPlayer");
        //使用CloneAB方法，省去Instantiate的过程
        //TestLoaderUpdate2(@"\Android\Role\role_mainplayer.assetbundle", "Role_MainPlayer");

        //异步加载的方法
        TestLoaderAsync(@"\Android\Role\role_mainplayer.assetbundle", "Role_MainPlayer");
    }



    void TestLoader()
    {
        AssetBundleLoader loader = new AssetBundleLoader(@"\Android\Role\role_mainplayer.assetbundle");
        GameObject obj= loader.LoadAssetBundle<GameObject>("Role_MainPlayer");
        Instantiate(obj);
    }
    /// <summary>
    /// 建立AssetBundleManager类以后，使用时有更简洁的写法
    /// </summary>
    /// <param name="shortPath"></param>
    /// <param name="ABName"></param>
    void TestLoaderUpdate(string shortPath, string ABName)
    {
        GameObject obj= AssetBundleManager.Instance.LoadAB(shortPath, ABName);
        Instantiate(obj);

        
    }

    void TestLoaderUpdate2(string shortPath, string ABName)
    {
        AssetBundleManager.Instance.CloneAB(shortPath, ABName);
    }


    void TestLoaderAsync(string shortPath, string ABName)
    {
        AssetBundleManager.Instance.LoadABAsync(shortPath, ABName).OnABLoadComplete = (Object obj) => {
            Instantiate(obj as GameObject);
        };
    }
	// Update is called once per frame
	void Update () {
	
	}
}
