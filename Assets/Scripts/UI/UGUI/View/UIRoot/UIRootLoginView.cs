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
/// 登陆场景的总UIRoot
/// </summary>
public class UIRootLoginView : UIRootViewBase
{

    GameObject obj;
    protected override void OnStart()
    {    
        base.OnStart();
        StartCoroutine(OpenLoginPanel());
    }

    private IEnumerator OpenLoginPanel()
    {
        yield return new WaitForSeconds(0.5f);
        LoginController.Instance.OpenLoginPanel();
    }

}
