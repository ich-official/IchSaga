//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-28 17:32:32
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
/// 选角色界面中我已创建的角色view，
/// </summary>
public class UIGizmosMyRoleItemView : MonoBehaviour {

    private int mRoleId;            //职业的ID（在本地excel里定义）

    [SerializeField]
    private Text mNickNameLabel;    //昵称
    [SerializeField]
    private Text mLevelLabel;       //等级
    [SerializeField]
    private Text mClassType;        //职业
    [SerializeField]
    private Image mClassHeadPic;        //头像


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetUI(int roleId,string nickName, int level, byte classId ,Sprite headPic)
    {
        mRoleId = roleId;
        mNickNameLabel.text = nickName;
        mLevelLabel.text ="Lv."+ level.ToString();
        mClassType.text = ClassDBModel.Instance.Get(classId).Name;
        mClassHeadPic.overrideSprite = headPic;
    }

    private void OnDestroy()
    {
        mNickNameLabel = null;
        mLevelLabel = null;
        mClassType = null;
        mClassHeadPic = null;
    }
}
