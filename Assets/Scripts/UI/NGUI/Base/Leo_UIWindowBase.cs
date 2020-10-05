using UnityEngine;
using System.Collections;

public class Leo_UIWindowBase : Leo_UIBase {

    [SerializeField]
    public AnchorPosition pos = AnchorPosition.CENTER;

    [SerializeField]
    public ShowWindowStyle style = ShowWindowStyle.NONE;

    [SerializeField]
    public float tweenDuration = 5f;

    /// <summary>
    /// 销毁窗口时不知道要销毁哪个窗口，在这里存一下窗口类型，以便告知destroy哪个
    /// </summary>
    [HideInInspector]
    public UIPanelType currentUIType;

    /// <summary>
    /// 点击按钮时，下一个要打开哪个窗口？记录在这个变量中
    /// </summary>
    protected UIPanelType mNextWindow = UIPanelType.NONE;

    /// <summary>
    /// close myself,oops....
    /// </summary>
    protected virtual void SelfClose()
    {
        UIViewManagerNGUI.Instance.CloseWindowUI(currentUIType);
    }

    protected override void LeoOnDestroy()
    {
        if (mNextWindow.Equals(UIPanelType.NONE)) return;
        UIViewManagerNGUI.Instance.OpenWindowUI(mNextWindow);
        Leo_UIDepthManager.Instance.CheckOpenedWindows();
        /* 使用上述方法更简洁，不用多次写Leo_WindowUIType.LOGIN了
        if (mNextWindow.Equals(Leo_WindowUIType.LOGIN))
        {
            GameObject obj = Leo_WindowUIManager.Instance.OpenWindowUI(Leo_WindowUIType.LOGIN);
        }
         * */
        base.LeoOnDestroy();
    }
}
