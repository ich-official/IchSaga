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
/// ѡ���ɫ����������ק�ƶ���ɫλ�õ�UI�ű�
/// </summary>
public class UIGizmosSelectRoleDragView : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

    private Vector2 mBeginDragPos = Vector2.zero;
    private Vector2 mEndDragPos = Vector2.zero;
    public System.Action<int> OnDragEnable; //֪ͨ���������-1�������ң�1���ƶ�90��



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
        //ע�⣺�ֻ�����ϰ��Ϊ������קʱitem�������ƶ��ģ��������ﴫ��ʱ�Ǻ�ʵ��ֵ�෴��
        if (x >= 30)
        {
            //TODO:����������ƶ�90�ȣ�
            OnDragEnable(-1);
        }
        else if (x <= -30)
        {
            //TODO:����������ƶ�90�ȣ�
            OnDragEnable(1);
        }
    }
    void OnDestroy()
    {
        OnDragEnable = null;
    }
}
