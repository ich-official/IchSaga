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
using UnityEngine.iOS;

/// <summary>
/// 全局设置，刚进入游戏时要做的各种必要赋值和操作
/// </summary>
public class GlobalInit : MonoBehaviour {

    public static GlobalInit Instance;
    [HideInInspector]
    public static bool isSingleMode=true;    //是否是单机模式，选择单机模式，则所有网络通信相关方法都将做改变

    [HideInInspector]
    public long InitServerTimeStamp;  //游戏开始时服务器时间戳

    //获取当前时间戳
    [HideInInspector]
    public long CurrentTimeStamp { 
        get { return InitServerTimeStamp + (long)RealTime.time; } 
    } 
    /// <summary>
    /// UITween曲线，NGUI也可以用
    /// </summary>
    public AnimationCurve LeoAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    [HideInInspector]
    public PlayerController currentPlayer;

    [HideInInspector]
    public string currentPlayerUsername;


    void Awake()
    {
        Instance = this;
        isSingleMode = true;
        SetLogLevel();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
        if (!isSingleMode)
        {
            HttpSimulator.Instance.DoPostSingle(ServerAPI.Time, "", OnTimeCallback);
        }
	}

    private void OnTimeCallback(CallbackArgs args)
    {
        if (!args.hasError)
        {
            InitServerTimeStamp = long.Parse(args.json);
        }
    }
    /// <summary>
    /// 设置打印日志等级
    /// </summary>
    private void SetLogLevel(LogUtil.LogLevel level = LogUtil.LogLevel.Error)
    {
#if DEBUG_LOG
        LogUtil.logLevel = LogUtil.LogLevel.Test;
#else
        Leo_LogUtil.logLevel = level;
#endif
    }

	void Update () {
	
	}

    
}
