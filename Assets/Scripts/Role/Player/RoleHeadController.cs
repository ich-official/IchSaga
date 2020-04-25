//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
/// <summary>
/// 控制角色头顶名字、血条等UI的
/// </summary>
public class RoleHeadController : MonoBehaviour {

    [SerializeField]
    private UILabel PlayerNameLabel;

    /// <summary>
    /// 第三方上飘UI插件
    /// </summary>
    [SerializeField]
    private HUDText hudText;
    /// <summary>
    /// 模型上的出生点
    /// </summary>
    private Transform mTarget;

    [SerializeField]
    private UISlider HP;

	void Start () {
	
	}
	
	
	void Update () {
        //如果敌人死了，模型被销毁，mTarget也会为null，需要判断
        if (Camera.main == null || UICamera.mainCamera == null&&mTarget==null) return;
        //把世界坐标点 转换成视口坐标(即viewport)
        Vector3 pos = Camera.main.WorldToViewportPoint(mTarget.position);
        //然后用UI相机把viewport再转换成世界坐标
        Vector3 uiPos = UICamera.mainCamera.ViewportToWorldPoint(pos);

        gameObject.transform.position = uiPos;
         
	}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">UI的出生点</param>
    /// <param name="username">初始化值，roleinfo中的属性</param>
    public void Init(Transform target,string username,bool isShowHP)
    {
        mTarget = target;
        PlayerNameLabel.text = username;
        NGUITools.SetActive(HP.gameObject, isShowHP);
    }

    /// <summary>
    /// 上飘UILabel
    /// </summary>
    /// <param name="value"></param>
    public void SetHUDTextAndHPBar(int value,float HPPercent=1f)
    {
        hudText.Add("-"+value,Color.red,0.1f);
        HP.value = HPPercent;
    }
}
