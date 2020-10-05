//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-24 15:16:01
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
/// <summary>
/// 选择角色界面左上角职业头像区域的view
/// </summary>
public class UIGizmosClassItemView : MonoBehaviour {

    [SerializeField]
    private int ClassId;    //职业的编号

    [SerializeField]
    private int RotateAngle;  //角色旋转角度

    public delegate void OnClickClassImgHandler(int classId,int rotateAngle);
    public OnClickClassImgHandler OnClickClassImgEvent;

    private Vector3 mMoveToTarget;  //左侧职业头像动画，移动的位置

    // Use this for initialization
    void Start () {
        this.GetComponent<Button>().onClick.AddListener(OnButtonClick);
        mMoveToTarget = this.transform.localPosition + new Vector3(50, 0, 0);
        transform.DOLocalMove(mMoveToTarget, 0.2f)
            .SetAutoKill(false)
            .SetEase(GlobalInit.Instance.CommonAnimationCurve)
            .Pause();

    }
	
    public void SetSelectedTween(int selectedClassId)
    {
        if (selectedClassId == ClassId)
        {
            transform.DOPlayForward();
        }
        else
        {
            transform.DOPlayBackwards();
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonClick()
    {
        if (OnClickClassImgEvent != null)
        {
            OnClickClassImgEvent(ClassId, RotateAngle);
        }
    }

    private void OnDestroy()
    {
        OnClickClassImgEvent = null;
    }
}
