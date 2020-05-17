using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

/// <summary>
/// 加载窗口、设置窗口挂点、设置窗口打开动画方式，原名WindowUIMgr
/// </summary>
public class Leo_UIWindowManager : SingletonBase<Leo_UIWindowManager>
{   
   // Dictionary<Leo_UIWindowType, Leo_UIWindowBase> mWindowDic = new Dictionary<Leo_UIWindowType, Leo_UIWindowBase>();
    Dictionary<UIWindowType, UIPanelViewBase> mWindowDic = new Dictionary<UIWindowType, UIPanelViewBase>();
   //记录字典中存了几个对象，相当于场景中打开了几个窗口
    public int OpenedWindowsCount
    {
        get
        {
            return mWindowDic.Count;
        }
    }
    #region 打开与关闭窗口
    public GameObject OpenWindowUI(UIWindowType windowType,
        //以下2个参数不应该被随便修改，把他们移到Leo_UIWindowBase中统一配置
       /* Leo_AnchorPosition pos = Leo_AnchorPosition.CENTER,
        Leo_ShowWindowStyle style = Leo_ShowWindowStyle.NONE,*/bool isUGUI=true, bool isOpenWindow = true)
    {
        GameObject obj = null;
        //判断场景是否已经打开了一个相同场景
        if (mWindowDic.ContainsKey(windowType))
        {
            Debug.Log("已经有一个" + windowType + "在场景中！");
            //temp, Leo solution,UGUI后此方案已过时，新方案为统一设置obj后再统一进行层级管理
            //return mWindowDic[windowType].gameObject;   
            obj = mWindowDic[windowType].gameObject;

        }
        else
        {
            //判断当前设置是否是不打开其他窗口
            if (windowType.Equals(UIWindowType.NONE))
            {
                Debug.Log("已设置为不打开窗口!");
                return null;
            }

            #region UGUI后Leo自行修改
            if (isUGUI)
            {
                //生成一个预制体的精简写法，需保证格式化内容与预制体名一致
                obj = ResourcesManager.Instance.
                            Load(ResourcesManager.ResourceType.UIPanel,
                            string.Format("Panel_{0}UGUI", windowType.ToString().ToLower()), true);
            }
            else
            {
                //生成一个预制体的精简写法，需保证格式化内容与预制体名一致
                obj = ResourcesManager.Instance.
                            Load(ResourcesManager.ResourceType.UI_WINDOW,
                            string.Format("Panel_{0}", windowType.ToString().ToLower()), true);
            }
            #endregion
            #region 使用上述写法后，switch弃用。但需注意Panel_后的内容必须为小写，创建预制体需注意。
            /* 
            switch (windowType)
            {
                case Leo_WindowUIType.LOGIN:
                    obj = Leo_ResourcesManager.Instance.
                        Load(Leo_ResourcesManager.ResourceType.UI_WINDOW, "Panel_Login", true);
                    break;
                case Leo_WindowUIType.REG:
                    obj = Leo_ResourcesManager.Instance.
                       Load(Leo_ResourcesManager.ResourceType.UI_WINDOW, "Panel_Reg", true);
                    break;
            }
             * */
            #endregion
            //把当前类的对象存入字典，方便操作
            //Leo_UIWindowBase windowBase = obj.GetComponent<Leo_UIWindowBase>();
            UIPanelViewBase panelBase = obj.GetComponent<UIPanelViewBase>();
            if (panelBase == null || obj == null) return null;

            //层级管理，后打开的窗口显示在最前方,UGUI后改叫SetOrderInLayer(obj);
            //Leo_UIDepthManager.Instance.SetDepth(obj);

            mWindowDic.Add(windowType, panelBase);
            //设置父类属性，要打开哪个窗口传进去
            panelBase.currentUIType = windowType;
            Transform transParent = null;
            //选择锚点位置
            switch (panelBase.pos)
            {
                case AnchorPosition.CENTER:
                    //transParent = Leo_UISceneManager.Instance.currentScene.containerCenter;
                    transParent = UIRootController.Instance.currentScene.containerCenter;
                    break;
                case AnchorPosition.TOP_LEFT:
                    break;
                case AnchorPosition.TOP_RIGHT:
                    break;
                case AnchorPosition.BOTTOM_LEFT:
                    break;
                case AnchorPosition.BOTTOM_RIGHT:
                    break;
            }
            //设置窗口transform参数
            obj.transform.parent = transParent;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            //生成完先关闭，在下面实现窗口动画时根据需求再开启
            //NGUITools.SetActive(obj, false);
            obj.SetActive(false);
            //ShowWindow(obj, windowBase.style, isOpenWindow);
            //优化后的showWindow，传一个父类对象即可操作，很多属性都在父类中写好了
            ShowWindow(panelBase, isOpenWindow);
        }
        //UGUI层级管理方法，同时优化NGUI时自己的旧写法
        Leo_UIDepthManager.Instance.SetOrderInLayer(obj);
        return obj;
    }

    public void CloseWindowUI(UIWindowType windowType, bool isOpenWindow = false)
    {
        if (mWindowDic.ContainsKey(windowType))
        {
            ShowWindow(mWindowDic[windowType], false);
        }
    }
    #endregion

    #region 窗口动画实现 精简参数后，只传一个windowBase的对象即可
    private void ShowWindow(Leo_UIWindowBase windowBase, bool isOpenWindow)
    {
        switch (windowBase.style)
        {
            case ShowWindowStyle.NONE:
                DoNone(windowBase.gameObject, isOpenWindow);
                break;
            case ShowWindowStyle.CENTER2BIG:
                DoCenter2Big(windowBase.gameObject, isOpenWindow);
                break;
            case ShowWindowStyle.TOP2BOTTOM:
                DoOthers(windowBase.gameObject, 0, isOpenWindow);
                break;
            case ShowWindowStyle.BOTTOM2TOP:
                DoOthers(windowBase.gameObject,1, isOpenWindow);
                break;
            case ShowWindowStyle.LEFT2RIGHT:
                DoOthers(windowBase.gameObject, 2,isOpenWindow);
                break;
            case ShowWindowStyle.RIGHT2LEFT:
                DoOthers(windowBase.gameObject, 3,isOpenWindow);
                break;
        }
    }

    /// <summary>
    /// UGUI后，重载一个用于UGUI的函数，不删除原有的
    /// </summary>
    /// <param name="windowBase"></param>
    /// <param name="isOpenWindow"></param>
    private void ShowWindow(UIPanelViewBase panelBase, bool isOpenWindow)
    {
        switch (panelBase.style)
        {
            case ShowWindowStyle.NONE:
                DoNone(panelBase.gameObject, isOpenWindow);
                break;
            case ShowWindowStyle.CENTER2BIG:
                DoCenter2Big(panelBase.gameObject, isOpenWindow);
                break;
            case ShowWindowStyle.TOP2BOTTOM:
                DoOthers(panelBase.gameObject, 0, isOpenWindow);
                break;
            case ShowWindowStyle.BOTTOM2TOP:
                DoOthers(panelBase.gameObject,1, isOpenWindow);
                break;
            case ShowWindowStyle.LEFT2RIGHT:
                DoOthers(panelBase.gameObject,2, isOpenWindow);
                break;
            case ShowWindowStyle.RIGHT2LEFT:
                DoOthers(panelBase.gameObject,3, isOpenWindow);
                break;
        }
    }
    //没动画
    private void DoNone(GameObject obj, bool isOpenWindow)
    {
        //NGUITools.SetActive(obj, isOpenWindow);
        if (isOpenWindow)
        {
            //NGUITools.SetActive(obj, true);
            obj.SetActive(true);
        }
        else
        {
            DestroyWindow(obj.GetComponent<UIPanelViewBase>());
        }
    }

    //销毁窗口并从dic中移除
    private void DestroyWindow(UIPanelViewBase windowBase)
    {
        mWindowDic.Remove(windowBase.currentUIType);
        GameObject.Destroy(windowBase.gameObject);       
    }

    //中央扩大
    private void DoCenter2Big(GameObject obj, bool isOpenWindow)
    {
        #region NGUI
        /*
        TweenScale ts = obj.GetComponentLeo<TweenScale>();

        ts.from = Vector3.zero;
        ts.to = Vector3.one;
        ts.duration = obj.GetComponent<Leo_UIPanelViewBase>().tweenDuration;
        ts.animationCurve = Leo_GlobalInit.Instance.LeoAnimationCurve;  //把全局设置中调好的tween拉取过来
        if (isOpenWindow)
        {
           // NGUITools.SetActive(obj, true);
            obj.SetActive(true);
           // ts.Play();
            ts.SetOnFinished(() => {
                
            });
        }
        else
        {
            ts.PlayReverse();
            ts.SetOnFinished(() =>{
                DestroyWindow(obj.GetComponent<Leo_UIPanelViewBase>());
            });
        }
         * */
        #endregion

        #region UGUI
        if (isOpenWindow)
        {
            obj.SetActive(true);
            
        }
        obj.transform.localScale = Vector3.zero;
        Tweener ts= obj.transform.DOScale(Vector3.one, obj.GetComponent<UIPanelViewBase>().tweenDuration).SetAutoKill(false).Pause().OnRewind(() => {             
            DestroyWindow(obj.GetComponent<UIPanelViewBase>());
        });
        if (isOpenWindow)
        {
            obj.transform.DOPlayForward();
        }
        else
        {
            obj.transform.DOPlayBackwards();
        }
        
        #endregion

    }
    //上下左右插♂入场景
    /// <summary>
    /// 从不同的方向打开、关闭窗口
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="direction">0：TOP2BOTTOM，1：BOTTOM2TOP，2：LEFT2RIGHT，3：RIGHT2LEFT</param>
    /// <param name="isOpenWindow"></param>
    private void DoOthers(GameObject obj,int direction, bool isOpenWindow)
    {
        #region NGUI
        /*
        TweenPosition tp = obj.GetComponentLeo<TweenPosition>();
        Vector3 from = Vector3.zero;
        switch (direction)
        {
            case 0:
                from = new Vector3(0, 1000, 0);
                break;
            case 1:
                from = new Vector3(0, -1000, 0);
                break;
            case 2:
                from = new Vector3(-1200, 0, 0);
                break;
            case 3:
                from = new Vector3(1200, 0, 0);
                break;
        }
        tp.duration = obj.GetComponent<Leo_UIPanelViewBase>().tweenDuration;
        tp.animationCurve = Leo_GlobalInit.Instance.LeoAnimationCurve;  //把全局设置中调好的tween拉取过来
        if (isOpenWindow)
        {
            NGUITools.SetActive(obj, true);
            // tp.Play();
            tp.SetOnFinished(() =>
            {

            });
        }
        else
        {
            tp.PlayReverse();
            tp.SetOnFinished(() =>
            {
                DestroyWindow(obj.GetComponent<Leo_UIPanelViewBase>());
            });
        }*/
        #endregion 

        #region UGUI
        if (isOpenWindow)
        {
            obj.SetActive(true);

        }
        Tweener ts = obj.transform.DOLocalMove(Vector3.zero, obj.GetComponent<UIPanelViewBase>().tweenDuration)
            .SetAutoKill(false)
            .SetEase(GlobalInit.Instance.LeoAnimationCurve)
            .Pause()
            .OnRewind(() => {DestroyWindow(obj.GetComponent<UIPanelViewBase>());});
        switch (direction)
        {
            case 0://从上到下
                obj.transform.localPosition = new Vector3(0, 600, 0);
                break;
            case 1://从下到上
                obj.transform.localPosition = new Vector3(0, -600, 0);
                break;
            case 2://从左到右
                obj.transform.localPosition = new Vector3(-1000, 0, 0);
                break;
            case 3://从右到左
                obj.transform.localPosition = new Vector3(1000, 0, 0);
                break;
        }
        if (isOpenWindow)
        {
            obj.transform.DOPlayForward();
        }
        else
        {
            obj.transform.DOPlayBackwards();
        }
        #endregion
    }
    #endregion
}
