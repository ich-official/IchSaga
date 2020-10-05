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
/// NPCͷ����������view
/// </summary>
public class NPCHeadBarView : MonoBehaviour {

    [SerializeField]
    private Text PlayerNameLabel;

    /// <summary>
    /// ģ���ϵĳ�����
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
        //�õ���Ļ����
        Vector2 screenPos = Camera.main.WorldToScreenPoint(mTarget.position);
        //����UI����������
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
    /// <param name="target">UI�ĳ�����</param>
    /// <param name="username">��ʼ��ֵ��roleinfo�е�����</param>
    public void Init(Transform target, string nickName)
    {
        mTarget = target;
        PlayerNameLabel.text = nickName;
    }

}
