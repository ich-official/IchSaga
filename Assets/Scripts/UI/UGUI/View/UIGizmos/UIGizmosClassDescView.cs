//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-24 20:57:15
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
/// ѡ��ɫ���������ְҵ������view�ű�
/// </summary>
public class UIGizmosClassDescView : MonoBehaviour {
    [SerializeField]
    private Text mClassName;    //ְҵ���ƣ���xls���ȡ��
    [SerializeField]
    private Text mClassDesc;    //ְҵ��������xls���ȡ��

    public void SetUI(string className,string classDesc)
    {
        mClassName.text = className;
        mClassDesc.text = classDesc;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        mClassName = null;
        mClassDesc = null;
    }
}
