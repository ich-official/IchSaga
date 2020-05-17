using UnityEngine;
using System.Collections;

public class Leo_UISceneLoginController : Leo_UISceneBase
{

    GameObject obj;

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void OnStart()
    {
        obj = Leo_UIWindowManager.Instance.OpenWindowUI(
               UIWindowType.LOGIN,
               /*   //优化后，只需传一个参数即可，其他都在Leo_UIWindowBase中配置好了
               Leo_AnchorPosition.CENTER,
               Leo_ShowWindowStyle.CENTER2BIG, */true);

        base.OnStart();
    }
    void Update()
    {

    }
}
