//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-24 10:30:13
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 选择角色界面用于拖拽移动角色位置的UI脚本
/// </summary>
public class UIGizmosSelectRoleDragView : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

    private Vector2 mBeginDragPos = Vector2.zero;
    private Vector2 mEndDragPos = Vector2.zero;
    public System.Action<int> OnDragEnable; //通知摄像机向左（-1）或者右（1）移动90度



    public void OnBeginDrag(PointerEventData eventData)
    {
        mBeginDragPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        mEndDragPos = eventData.position;

        float x = mBeginDragPos.x - mEndDragPos.x;
        //注意：手机操作习惯为向左拖拽时item是向右移动的，所以这里传参时是和实际值相反的
        if (x >= 30)
        {
            //TODO:摄像机向右移动90度，
            OnDragEnable(-1);
        }
        else if (x <= -30)
        {
            //TODO:摄像机向左移动90度，
            OnDragEnable(1);
        }
    }
    void OnDestroy()
    {
        OnDragEnable = null;
    }
}
