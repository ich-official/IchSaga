using UnityEngine;
using System.Collections;

/// <summary>
/// UI层级管理器,原名LayerUIMgr
/// </summary>
public class Leo_UIDepthManager : SingletonBase<Leo_UIDepthManager>
{
    private int mPanelDepth=50; //Panel层级深度 
    private int mWindowDepth; //Panel层级深度 

    public void ResetDepth()
    {
        mPanelDepth = 50;
    }
    //打开的窗口<=0个，就说明场景没有窗口，把深度值重置一下，下次从默认值开始计算
    public void CheckOpenedWindows()
    {
        if (Leo_UIWindowManager.Instance.OpenedWindowsCount <= 0)
        {
            ResetDepth();
        }
    }
    /// <summary>
    /// NGUI用的
    /// </summary>
    /// <param name="obj"></param>
    public void SetDepth(GameObject obj)
    {
        //调用一次方法，就把深度+1，保证新创建的窗口在最前面
        mPanelDepth += 1;
        UIPanel[] panels = obj.GetComponentsInChildren<UIPanel>();
        if (panels.Length > 0)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].depth += mPanelDepth;
            }
        }
    }
    /// <summary>
    /// UGUI用的层级管理
    /// </summary>
    /// <param name="obj"></param>
    public void SetOrderInLayer(GameObject obj)
    {
        mPanelDepth += 1;
        Canvas canvas = obj.GetComponent<Canvas>();
        canvas.sortingOrder = mPanelDepth;
    }
}
