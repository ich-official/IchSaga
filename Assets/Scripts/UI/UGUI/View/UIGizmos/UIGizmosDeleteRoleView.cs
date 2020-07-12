//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-30 14:44:21
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
/// <summary>
/// 删除角色弹出的view
/// </summary>
public class UIGizmosDeleteRoleView : UIViewBase {
    [SerializeField]
    private Text TipsLabel;

    private Vector3 mMoveTargetPos; //从视野外移动到目标点

    public Action OnDeleteClick;    //点击删除按钮的委托，在主控制器里监听并实现功能

    protected override void OnAwake()
    {
        this.transform.localPosition = new Vector3(0, 1000, 0); //初始化自身位置（视野外
        base.OnAwake();
    }

    protected override void OnStart()
    {

        base.OnStart();
        //设置一个动画，并暂停执行，后续可以使用DOPlayForward、DOPlayBackwards控制播放
        transform.DOLocalMove(Vector3.zero, 0.2f).SetAutoKill(false).SetEase(GlobalInit.Instance.CommonAnimationCurve).Pause();
    }

    protected override void OnBtnClick(GameObject obj)
    {
        switch (obj.name)
        {
            case "btnDeleteRoleOK":
                DeleteRole();
                break;

            case "btnDeleteRoleCancel":
                Close();
                break;

        }
        base.OnBtnClick(obj);
    }

    /// <summary>
    /// 删除角色第四步，把删除角色的UI打开，不是真打开是把UI移动到视野内，并给nickname和OK按钮赋值
    /// </summary>
    public void Show(string nickName,Action onDeleteClick)
    {
        //Debug.Log("4");

        TipsLabel.text = string.Format("您确定要删除<color=#ff0000ff>{0}</color>吗？", nickName);
        OnDeleteClick = onDeleteClick;
        transform.DOPlayForward();
    }
    /// <summary>
    /// 关闭本页面，不是真关闭而是移动到视野外
    /// </summary>
    public void Close()
    {
        transform.DOPlayBackwards();
    }
    /// <summary>
    /// 删除角色第五步，点击OK按钮，到主控制器走网络交互真正执行删除功能
    /// </summary>
    private void DeleteRole()
    {
        if (OnDeleteClick != null) OnDeleteClick();
        Account_DeleteRoleReqProto proto = new Account_DeleteRoleReqProto();
        //proto.RoleId=
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        UIGC();
    }

    /// <summary>
    /// 把UI上有引用的都释放掉，这样GC就可以回收他们了
    /// </summary>
    private void UIGC()
    {
        TipsLabel = null;
        OnDeleteClick = null;
    }
}
