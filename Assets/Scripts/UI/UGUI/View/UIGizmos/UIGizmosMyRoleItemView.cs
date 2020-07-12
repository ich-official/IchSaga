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
/// ѡ��ɫ���������Ѵ����Ľ�ɫview��
/// </summary>
public class UIGizmosMyRoleItemView : MonoBehaviour {

    private int mRoleId;            //ְҵ��ID���ڱ���excel�ﶨ�壩

    [SerializeField]
    private Text mNickNameLabel;    //�ǳ�
    [SerializeField]
    private Text mLevelLabel;       //�ȼ�
    [SerializeField]
    private Text mClassType;        //ְҵ
    [SerializeField]
    private Image mClassHeadPic;        //ͷ��


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
