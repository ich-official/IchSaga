using UnityEngine;
using System.Collections;

/// <summary>
/// loading场景控制器
/// </summary>
public class Leo_UISceneLoadingController : Leo_UISceneBase {
    [SerializeField]
    private UIProgressBar mProcessBar;

    [SerializeField]
    private UILabel mProcessLabel;  //显示进度数值

    [SerializeField]
    private Transform mProcessThumb;    //手动控制thumb位置,    //-370~360
    public void SetProgressValue(float value)
    {
        if (mProcessBar == null || mProcessLabel == null || mProcessThumb == null) return;
        mProcessBar.value = value;
        mProcessLabel.text = string.Format("{0}%", (int)(value * 100));
        mProcessThumb.localPosition = new Vector3(730 * value - 370, -230, 0);
    }

    protected override void LeoOnDestroy()
    {
        base.LeoOnDestroy();
        mProcessBar = null;
        mProcessLabel = null;
        mProcessThumb = null;
    }
}
