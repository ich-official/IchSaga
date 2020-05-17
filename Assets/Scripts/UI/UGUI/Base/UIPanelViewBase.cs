//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
/// <summary>
/// UIWindowViewBase，panel基类
/// </summary>
public class UIPanelViewBase :UIViewBase {
    [SerializeField]
    public AnchorPosition pos = AnchorPosition.CENTER;

    [SerializeField]
    public ShowWindowStyle style = ShowWindowStyle.NONE;

    [SerializeField]
    public float tweenDuration = 1f;

    /// <summary>
    /// UGUI适用，OnDestroy中调用，
    /// </summary>
    //public System.Action OnViewClose;

    /// <summary>
    /// 下一个要打开的UI类型
    /// </summary>
    private UIWindowType mNextOpenUIType;

    /// <summary>
    /// 是否打开下一个窗口
    /// </summary>
    //private bool isOpenNext = false;
    /// <summary>
    /// 销毁窗口时不知道要销毁哪个窗口，在这里存一下窗口类型，以便告知destroy哪个
    /// </summary>
    [HideInInspector]
    public UIWindowType currentUIType;

    /// <summary>
    /// 点击按钮时，下一个要打开哪个窗口？记录在这个变量中
    /// </summary>

    /// <summary>
    /// 临时方法，是这个方法，关闭自身的
    /// </summary>
    /// <param name="obj"></param>
    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);
        if (obj.name.Equals("ClosePanelButton",System.StringComparison.CurrentCultureIgnoreCase))
        {
            SelfClose();
        }
    }

    /// <summary>
    /// close myself,oops....
    /// 增加bool值，判断是否有下一个打开的窗口
    /// </summary>
    public virtual void SelfClose()
    {
       // isOpenNext = OpenNext;
        Leo_UIWindowManager.Instance.CloseWindowUI(currentUIType);
    }
    public virtual void SelfCloseAndOpenNext(UIWindowType next)
    {
        SelfClose();
        mNextOpenUIType = next;
    }

    /// <summary>
    /// 销毁一个窗口时，打开下一个窗口，这种方法在UGUI后不适用，改用委托执行
    /// </summary>
    protected override void BeforeOnDestroy()
    {
        Leo_UIDepthManager.Instance.CheckOpenedWindows();
        //if (isOpenNext)
       // {
        //    if (OnViewClose != null) OnViewClose();
       // }
        if ( mNextOpenUIType != UIWindowType.NONE )   //需要打开下一个UI
        {
            Leo_UIWindowManager.Instance.OpenWindowUI(mNextOpenUIType);
        }        
        
        base.BeforeOnDestroy();
    }

}
