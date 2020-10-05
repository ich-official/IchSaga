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
/// 选角色场景里控制职业描述的view脚本
/// </summary>
public class UIGizmosClassDescView : MonoBehaviour {
    [SerializeField]
    private Text mClassName;    //职业名称（从xls里获取）
    [SerializeField]
    private Text mClassDesc;    //职业描述（从xls里获取）

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
