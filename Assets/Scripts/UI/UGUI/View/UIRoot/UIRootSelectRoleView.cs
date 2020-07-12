//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-23 12:39:00
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 选择角色场景UI的主控制器
/// </summary>
public class UIRootSelectRoleView : UIRootViewBase
{
    [SerializeField]
    public UIGizmosSelectRoleDragView DragView;

    public UIGizmosClassItemView[] ClassItems;  //职业选项集合

    public UIGizmosClassDescView descView;  //当前选择的职业描述view

    public InputField NickName; //昵称输入框

    public Action OnStartGameClick; //点击开始游戏按钮的委托

    public Action OnDeleteButtonClick;    //点击删除角色按钮的委托

    public Action OnCreateButtonClick;      //点击创建角色按钮的委托
    [SerializeField]
    private  Transform[] UICreateViewObj;  //本场景下新建角色功能的UI

    [SerializeField]
    private Transform[] UISelectViewObj;  //本场景下选择角色功能的UI

    [SerializeField]
    private GameObject roleItemPrefab;  //我已有的角色prefab

    [SerializeField]
    private Transform roleItemContainer;    //我已有角色的UI挂载点

    [SerializeField]
    private Sprite[] roleHeadPics;  //职业头像集合，按roleId顺序排列

    [SerializeField]
    public UIGizmosDeleteRoleView DeleteView;  //删除角色弹出的view

    [SerializeField]
    public Button CreateRoleButton;  //创建角色按钮，单独拿出来
    protected override void OnStart()
    {
        base.OnStart();
    }

    /// <summary>
    /// 继承基类的点击按钮
    /// </summary>
    /// <param name="obj"></param>
    protected override void OnBtnClick(GameObject obj)
    {
        switch (obj.name)
        {
            case "btnRandomName":
                RandomNameClick();
                break;
            case "btnStartGame":
                StartGameClick();
                break;
            case "btnDeleteRole":
                DeleteButtonClick();
                break;
            case "btnCreateRole":
                CreateButtonClick();
                break;
        }
        base.OnBtnClick(obj);
    }

    public void RandomNameClick()
    {
        NickName.text = GameUtil.RandomName();
    }

    private void StartGameClick()
    {
        if (OnStartGameClick != null) OnStartGameClick();
    }

    /// <summary>
    /// 删除角色第一步，点击“删除”按钮的操作，起始操作
    /// </summary>
    public void DeleteButtonClick()
    {
        //Debug.Log("1");

        if (OnDeleteButtonClick != null) OnDeleteButtonClick();
    }
    /// <summary>
    /// 创建角色按钮点击，先判断是否已有角色，然后切换创建角色的UI、隐藏创建角色按钮、场景prefab都加载
    /// </summary>
    public void CreateButtonClick()
    {
        if (GlobalCache.Instance.Role_CurrentRoleId == -1)
        {
            //没有角色，允许创建
            SetUIObjShow(true);
            CreateRoleButton.gameObject.SetActive(false);
            if (OnCreateButtonClick != null) OnCreateButtonClick();
        }
        else
        {
            //已有角色，不允许创建
            UIDialogController.Instance.Show("您已拥有角色！");
        }
    }
    /// <summary>
    /// 删除角色第三步，从主场景控制器取到了需要的数据，继续走正常流程，开始调deleteView上的show方法显示UI
    /// /// </summary>
    /// <param name="nickName"></param>
    /// <param name="onDeleteClick"></param>
    public void DeleteRoleOKClick(string nickName, Action onDeleteClick)
    {
        //Debug.Log("3");
        DeleteView.Show(nickName, onDeleteClick);
    }
    /// <summary>
    /// 点击删除角色后，UI上的变化，具体为：选择角色的按钮组隐藏
    /// </summary>
    public void DeleteRoleChangeUI()
    {
        for (int i = 0; i < UISelectViewObj.Length; i++)
        {
            UISelectViewObj[i].gameObject.SetActive(false);
        }
        CreateRoleButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// 打开或关闭场景中需要灵活开关的obj，以判断是否是新建角色场景为例
    /// 新建角色、选择角色两个功能，UI显示的是2套，通过这个方法进行切换
    /// </summary>
    /// <param name="isActive"></param>
    public void SetUIObjShow(bool isCreate)
    {
        if(UICreateViewObj!=null && UICreateViewObj.Length>0 && UISelectViewObj != null && UISelectViewObj.Length>0)
        {
            for (int i = 0; i < UICreateViewObj.Length; i++)
            {
                UICreateViewObj[i].gameObject.SetActive(isCreate);
            }
            for(int j=0;j< UISelectViewObj.Length; j++)
            {
                UISelectViewObj[j].gameObject.SetActive(!isCreate);

            }
        }

    }

    /// <summary>
    /// 把已有角色的UI信息加载到场景中
    /// </summary>
    /// <param name="roleItems"></param>
    public void SetRoleList(List<Account_LoginGameServerRespProto.RoleItem> roleItems)
    {
        for (int i = 0; i < roleItems.Count; i++)
        {
            GameObject obj = Instantiate(roleItemPrefab);
            UIGizmosMyRoleItemView view = obj.GetComponent<UIGizmosMyRoleItemView>();
            if (view != null)
            {
                view.SetUI(roleItems[i].RoleId, 
                    roleItems[i].RoleNickName,
                    roleItems[i].RoleLevel, 
                    roleItems[i].RoleClass, 
                    roleHeadPics[roleItems[i].RoleClass-1]);   //角色的头像按角色id选取
            }
            obj.transform.parent = roleItemContainer;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = new Vector3(0, -100 * i, 0);
        }
        Debug.Log("有信息：" + roleItems[0].RoleNickName);
    }

    protected override void BeforeOnDestroy()
    {
        UIGC();
        base.BeforeOnDestroy();
    }
    /// <summary>
    /// 把UI上有引用的都释放掉，这样GC就可以回收他们了
    /// </summary>
    private void UIGC()
    {
        DragView = null;
        GameObjectUtil.SetNull(ClassItems);
        descView = null;
        NickName = null;
        OnStartGameClick = null;
        OnDeleteButtonClick = null;
        OnCreateButtonClick = null;
        GameObjectUtil.SetNull(UICreateViewObj);
        GameObjectUtil.SetNull(UISelectViewObj);
        roleItemPrefab = null;
        roleItemContainer = null;
        GameObjectUtil.SetNull(roleHeadPics);
        DeleteView = null;
        CreateRoleButton = null;

    }
}
