//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 23:47:11
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using UnityEngine;
using System.Collections;

//原名LogOnSceneCtrl
/// <summary>
/// 登陆场景的主控制器
/// </summary>
public class SceneLoginController : MonoBehaviour
{

    GameObject loginObj;
    void Awake()
    {
        loginObj = UIRootController.Instance.LoadUIRoot(UIRootController.UIRootType.LOGIN);
    }
	void Start () {
	
	}

}
