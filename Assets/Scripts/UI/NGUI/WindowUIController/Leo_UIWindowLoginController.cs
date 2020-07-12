using UnityEngine;
using System.Collections;
/// <summary>
/// 原名UILogOnCtrl
/// </summary>
public class Leo_UIWindowLoginController : Leo_UIWindowBase {
    //private Leo_WindowUIType mNextWindow = Leo_WindowUIType.NONE;

    [SerializeField]
    private UIInput mInputUsername;
    [SerializeField]
    private UIInput mInputPassword;
    [SerializeField]
    private UILabel mLoginHintLabel;
    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);
        switch (obj.name)
        {
            case "ButtonLoginOK":
                Debug.Log("OK");
                BtnLogin();
                break;
            case "ButtonGotoReg":
                Debug.Log("reg");
                ToRegWindow();
                break;
        }
    
    }

    void ToRegWindow()
    {
        //Destroy(this.gameObject);
        //Leo_WindowUIManager.Instance.CloseWindowUI(Leo_WindowUIType.LOGIN);//把关闭自身的方法提取到父类中，使用下面的方法更简便
        this.SelfClose();
        mNextWindow = UIWindowType.REG;
       // obj.transform.parent = GameObject.Find("Container_Center").transform;//temp,different from video
       // obj.transform.localPosition = Vector3.zero;
       // obj.transform.localScale = Vector3.one;
    }
    /// <summary>
    /// 实现登陆功能
    /// </summary>
    void BtnLogin()
    {
        string username = this.mInputUsername.value.Trim();
        string password = this.mInputPassword.value.Trim();
        
        //获取上次登陆时储存的用户名和密码
        string lastUsername=PlayerPrefs.GetString(Constant.MIR2_USERNAME);
        string lastPassword = PlayerPrefs.GetString(Constant.MIR2_PASSWORD);
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)//用户名密码判空
            || lastUsername != username || lastPassword != password)   //用户名密码校验
        {
            mLoginHintLabel.gameObject.SetActive(true);
            mLoginHintLabel.text = "[ff0000]用户名或密码错误！[-]";
            return;
        }
        GlobalInit.Instance.currentPlayerUsername = username;
        ScenesManager.Instance.LoadMainScene();
    }

}
