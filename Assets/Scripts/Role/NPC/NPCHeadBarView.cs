//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-08 15:50:24
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// NPC头顶名字条的view
/// </summary>
public class NPCHeadBarView : MonoBehaviour {

    [SerializeField]
    private Text PlayerNameLabel;

    /// <summary>
    /// 模型上的出生点
    /// </summary>
    private Transform mTarget;

    private RectTransform mRect;

    void Start()
    {
        mRect = GetComponent<RectTransform>();
    }


    void Update()
    {
        if (mTarget == null || mRect == null) return;
        //得到屏幕坐标
        Vector2 screenPos = Camera.main.WorldToScreenPoint(mTarget.position);
        //接受UI的世界坐标
        Vector3 pos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(mRect, screenPos, UICameraSupport.Instance.camera, out pos))
        {
            transform.position = pos;
        }
        else
        {
            transform.position = pos;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">UI的出生点</param>
    /// <param name="username">初始化值，roleinfo中的属性</param>
    public void Init(Transform target, string nickName)
    {
        mTarget = target;
        PlayerNameLabel.text = nickName;
    }

}
