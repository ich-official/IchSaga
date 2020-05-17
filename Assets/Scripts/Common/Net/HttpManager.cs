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
using System.Collections.Generic;
/// <summary>
/// 使用HTTP通信管理类
/// </summary>
public class HttpManager : MonoBehaviour
{
    public delegate void mHttpCallback(CallbackArgs args);
    public mHttpCallback Callback;

    public CallbackArgs mCallbackArgs;

    private bool isRequested=false;   //请求是否已经发出

    public bool IsRequested
    {
        get { return isRequested; }
    }
    #region  //单例，继承MonoBehaviour时的单例写法，返回的是一个组件
    private static HttpManager instance;
    public static HttpManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("HttpManager");
                DontDestroyOnLoad(obj);
                instance = obj.SafeGetComponent<HttpManager>();
            }
            return instance;
        }
    }
    #endregion

    void Start()
    {
        mCallbackArgs = new CallbackArgs();
    }


    #region 发送一次web数据
    /// <summary>
    /// 发送web数据
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    /// <param name="isPost"></param>
    /// <param name="GetCallback"></param>
    public void SendMessages(string url, string json, bool isPost, mHttpCallback GetCallback)
    {
        if (isRequested) return;

        isRequested = true;
        Callback += GetCallback;
        if (isPost)
        {
            //do post request
            PostUrl(url, json);
        }
        else
        {
            GetUrl(url);
        }
    }
    #endregion
    private void GetUrl(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(Request(www));
    }

    private void PostUrl(string url, string json){
        WWWForm form = new WWWForm();
        form.AddField("", "");

        WWW www = new WWW(url,form);
        StartCoroutine(Request(www));
    }

    /// <summary>
    /// 发送一次请求
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    private IEnumerator Request(WWW www)
    {
        yield return www;

        isRequested = false;//服务器返回了数据，一次请求完成了
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            if (Callback != null)
            {
                mCallbackArgs.hasError = true;
                mCallbackArgs.errorMsg = www.error;
                mCallbackArgs.json = "";
                Callback(mCallbackArgs);
            }
        }
        else
        {
            Debug.Log(www.text);
            //如果返回的json为空或者无数据，也认为是错误的
            if (www.text == "" || www.text == "null")
            {
                if (Callback != null)
                {
                    mCallbackArgs.hasError = true;
                    mCallbackArgs.errorMsg = "no data response!";
                    mCallbackArgs.json = "";
                    Callback(mCallbackArgs);
                }
            }
            else
            {
                if (Callback != null)
                {
                    mCallbackArgs.hasError = false;
                    mCallbackArgs.errorMsg = "";
                    mCallbackArgs.json = www.text;
                    Callback(mCallbackArgs);    //服务器有数据返回，执行回调
                }
            }

        }
    }


}

/// <summary>
/// web请求回来的数据，单独用类来表示
/// </summary>
public class  CallbackArgs{
    public bool hasError;//是否报错
    public int errorCode;//错误码
    public string errorMsg;//错误信息
    public string json;//服务器返回的json串
    public ClientEntityBase obj;    //服务器传回一个entity
    public List<ClientEntityBase> objList;//服务器传回多个entity实体信息的话，传回此list，省去读json的麻烦
}