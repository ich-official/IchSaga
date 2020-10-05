using UnityEngine;
using System.Collections;
/// <summary>
/// 原名UILogOnCtrl
/// </summary>
public class Leo_UIWindowRegController : Leo_UIWindowBase
{
    //private Leo_WindowUIType mNextWindow = Leo_WindowUIType.NONE;
    [SerializeField]
    private UIInput mInputUsername;
    [SerializeField]
    private UIInput mInputPassword;
    [SerializeField]
    private UIInput mInputPasswordVerify;
    [SerializeField]
    private UILabel mRegHintLabel;
    protected override void OnBtnClick(GameObject obj)
    {
        base.OnBtnClick(obj);
        switch (obj.name)
        {
            case "ButtonRegOK":
                Debug.Log("OK");
                BtnReg();
                break;
            case "ButtonRegCancel":
                Debug.Log("login");
                ToLoginWindow();
                break;
        }

    }
    void ToLoginWindow()
    {
        //Destroy(this.gameObject);

        //Leo_WindowUIManager.Instance.CloseWindowUI(Leo_WindowUIType.REG);
        this.SelfClose();
        mNextWindow = UIPanelType.LOGIN;
    }
    void BtnReg()
    {
        string mUsername = mInputUsername.value;
        string mPassword = mInputPassword.value;
        string mPassword2 = mInputPasswordVerify.value;
        if (string.IsNullOrEmpty(mUsername) || string.IsNullOrEmpty(mPassword) || string.IsNullOrEmpty(mPassword2))
        {
            mRegHintLabel.gameObject.SetActive(true);
            mRegHintLabel.text = "[ff0000]参数为空！[-]";
            return;
        }
        if (mPassword != mPassword2)
        {
            mRegHintLabel.gameObject.SetActive(true);
            mRegHintLabel.text = "[ff0000]密码输入不一致！[-]";
            return;
        }
        PlayerPrefs.SetString(Constant.MIR2_USERNAME, mUsername);
        PlayerPrefs.SetString(Constant.MIR2_PASSWORD, mPassword);

        GlobalInit.Instance.currentPlayerUsername = mUsername;
        ToLoginWindow();
    }
}
