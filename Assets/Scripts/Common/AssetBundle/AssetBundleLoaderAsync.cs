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

/// <summary>
/// 异步加载AB本质上是用协程加载
/// </summary>
public class AssetBundleLoaderAsync : MonoBehaviour {

    private string mFullPath;
    private string mABName;

    private AssetBundleCreateRequest request;
    private AssetBundle bundle;

    public System.Action<Object> OnABLoadComplete;

	// Use this for initialization
	void Start () {
        StartCoroutine(LoadAssetBundleAsync());
	}

    public void Init(string shortPath, string ABName)
    {
        mFullPath = LocalFileManager.Instance.localFilePath + shortPath;
        mABName = ABName;
    }

    private IEnumerator LoadAssetBundleAsync()
    {
        yield return null;
        byte[] buffer= LocalFileManager.Instance.GetBuffer(mFullPath);
        request = AssetBundle.LoadFromMemoryAsync(buffer);
        yield return request;

        bundle = request.assetBundle;
        if (OnABLoadComplete != null)
        {
            Debug.Log("异步加载完成！");
            OnABLoadComplete(bundle.LoadAsset(mABName));

            Destroy(this.gameObject);
        }

    }

    void OnDestroy()
    {
        if (bundle != null)
        {
            bundle.Unload(false);
            mFullPath = null;
        }
    }
}
