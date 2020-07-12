//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-21 18:40:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 选择角色场景的主控制器
/// </summary>
public class SceneSelectRoleController : MonoBehaviour {

    private Dictionary<int, GameObject> mClassDic=new Dictionary<int, GameObject>();  //所有职业的字典，通过读本地数据表class.xls获取
    private List<ClassEntity> mClassList;
    private Dictionary<int, RoleController> mRoleCtrlDic = new Dictionary<int, RoleController>();    //角色prefab的字典

    public Transform[] CreateRoleContainers;    //新建角色时的角色容器

    public Transform[] RoleSpawnPoints;     //角色生成时的柱子台
    private UIRootSelectRoleView mUISelectRoleView;
    private List<Account_LoginGameServerRespProto.RoleItem> mMyRoleList;    //我已有的角色列表
    GameObject selectRoleObj;

    private int mCurrentSelectedRoleId=-1;   //当前选择的角色Id，
    private int mCurrentSelectedClassId = 1;//当前选择的职业ID，默认是1（刺客）
    private bool mIsCreate;         //当前是否是新建角色的界面
    void Awake()
    {
        selectRoleObj = UIRootController.Instance.LoadUIRoot(UIRootController.UIRootType.SELECT_ROLE);
        mUISelectRoleView = selectRoleObj.GetComponent<UIRootSelectRoleView>();
    }


    // Use this for initialization
    void Start () {
        #region 客户端操作
        if (DelegateDefine.Instance.OnSceneLoadDone != null)
        {
            DelegateDefine.Instance.OnSceneLoadDone();
        }
        if (mUISelectRoleView != null){
            mUISelectRoleView.DragView.OnDragEnable = OnDragEnable;
        }

        if(mUISelectRoleView!=null && mUISelectRoleView.ClassItems.Length > 0)
        {
            for(int i =0;i< mUISelectRoleView.ClassItems.Length; i++)
            {
                mUISelectRoleView.ClassItems[i].OnClickClassImgEvent = OnClickClassImgCallback;
            }
        }

        mUISelectRoleView.OnStartGameClick = OnStartGameClick;
        mUISelectRoleView.OnDeleteButtonClick = OnDeleteButtonClick;
        mUISelectRoleView.OnCreateButtonClick = OnCreateButtonClick;
        #endregion

        #region 与服务器交互
        //监听服务器返回登陆信息的协议，里面包含角色信息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_LoginGameServerRespProto, OnLoginGameServerResp);
        //监听服务器返回创建角色的协议
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_AddRoleRespProto, OnAddRoleServerResp);
        //监听服务器返回点击开始游戏按钮后是否成功进入游戏的回调
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_EnterGameRespProto, OnEnterGameServerResp);
        //监听服务器返回删除角色是否成功的回调（假删，改角色状态）
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDefine.Account_DeleteRoleRespProto, OnDeleteRoleServerResp);
        LoadClassObj();

        LoginGameServer();
        #endregion
    }






    #region 与服务器交互
    /// <summary>
    /// 向服务器请求当前AccountId下的所有RoleId
    /// </summary>
    private void LoginGameServer()
    {
        Account_LoginGameServerReqProto proto = new Account_LoginGameServerReqProto();

#if UNITY_EDITOR
        proto.AccountId = 2;    //开发阶段测试数据
        proto.GameServerId = GlobalCache.Instance.Account_LastLoginServerId;    //20.07.10Ich新增区服判断
#else
        proto.AccountId = GlobalCache.Instance.Account_CurrentId;       //参数是当前登陆的AccountId
        proto.GameServerId = GlobalCache.Instance.Account_LastLoginServerId;    //20.07.10Ich新增区服判断
#endif
        SocketManager.Instance.SendMessageToLocalServer(proto.ToArray());   //向服务器发送一个登陆信息的协议
    }

    /// <summary>
    /// 监听服务器返回的登陆消息，里面包含角色信息
    /// </summary>
    /// <param name="buffer"></param>
    private void OnLoginGameServerResp(byte[] buffer)
    {
        Account_LoginGameServerRespProto proto = Account_LoginGameServerRespProto.GetProto(buffer);
        int roleCount = proto.RoleCount;
        Debug.Log("角色数量：" + roleCount);
        if (roleCount == 0) //没角色，进入新建角色界面
        {
            //新建角色界面，修改开始游戏按钮标记，切换一下UI、克隆所有角色、加载角色描述、自动赋予一个随机名字
            mIsCreate = true;
            mUISelectRoleView.SetUIObjShow(true);
            SetRoleSpawnPointsShow(true);
            CloneAllRoles();
            SetCurrentClassDesc(mCurrentSelectedClassId);
            mUISelectRoleView.RandomNameClick();    //新建界面自动赋值一个随机名字
            mUISelectRoleView.CreateRoleButton.gameObject.SetActive(false); //创建角色界面把该按钮隐藏
            Debug.Log("clone done!");
        }
        else
        {
            //选择角色界面，修改开始游戏按钮标记，切换一下UI，加载已有角色信息、克隆对应角色
            mIsCreate = false;
            mUISelectRoleView.SetUIObjShow(false);
            SetRoleSpawnPointsShow(false);
            mUISelectRoleView.CreateRoleButton.gameObject.SetActive(false); //把创建角色按钮隐藏
            if (proto.RoleList!= null)
            {
                mMyRoleList = proto.RoleList;
                mCurrentSelectedRoleId = proto.RoleList[0].RoleId;  //我的游戏只允许创建一个角色，所以固定取第一个角色的ID为当前选择的角色ID
                mUISelectRoleView.SetRoleList(proto.RoleList);
                SetMyRolePrefab(proto.RoleList[0].RoleId);
            }
            
        }
        GlobalCache.Instance.Role_CurrentRoleId = mCurrentSelectedRoleId;   //把当前的roleID缓存到全局
    }

    /// <summary>
    /// 服务器返回创建角色的消息
    /// </summary>
    /// <param name="p"></param>
    private void OnAddRoleServerResp(byte[] buffer)
    {
        Account_AddRoleRespProto proto = Account_AddRoleRespProto.GetProto(buffer);
        if (proto.IsSuccess)
        {
            //创建角色成功，开始登录进游戏的逻辑
            Debug.Log("add role success!!!");
            //UIDialogController.Instance.Show("创建角色成功");
            EnterGameReq(); //角色创建成功后直接发送进去游戏req
        }
        else
        {
            //创建角色失败，弹出提示框
            UIDialogController.Instance.Show("创建角色失败！错误码" + proto.MsgCode);
            Debug.Log("创建角色失败！错误码" + proto.MsgCode);
        }
    }
    /// <summary>
    /// 服务器返回进入游戏是否成功消息
    /// </summary>
    /// <param name="buffer"></param>
    private void OnEnterGameServerResp(byte[] buffer)
    {
        Account_EnterGameRespProto proto = Account_EnterGameRespProto.GetProto(buffer);
        if (proto.IsSuccess)
        {
            //TODO:成功就真正切换到主城场景
            Debug.Log("进入游戏成功！");
        }
        else
        {
            UIDialogController.Instance.Show("进入游戏失败！" + proto.MsgCode);
        }
    }

    private void OnDeleteRoleServerResp(byte[] buffer)
    {
        Account_EnterGameRespProto proto = Account_EnterGameRespProto.GetProto(buffer);
        if (proto.IsSuccess)
        {
            //TODO:把当前选择的角色重置为-1，UI上删除面板、删除按钮、角色头像去掉，创建角色按钮打开，场景里把角色prefab删除，
            mCurrentSelectedRoleId = -1;
            GlobalCache.Instance.Role_CurrentRoleId = mCurrentSelectedRoleId;
            mUISelectRoleView.DeleteView.Close();
            mUISelectRoleView.DeleteRoleChangeUI();
            DeleteMyRolePrefab();
            Debug.Log("删除角色成功！");
        }
        else
        {
            UIDialogController.Instance.Show("删除角色失败！" + proto.MsgCode);
        }
    }


    private void EnterGameReq()
    {

        Account_EnterGameReqProto proto = new Account_EnterGameReqProto();
        proto.RoleId = mCurrentSelectedRoleId;
        SocketManager.Instance.SendMessageToLocalServer(proto.ToArray());
    }


    /// <summary>
    /// 点击开始游戏的委托执行，做3件事。1、判断当前是否有角色  2、跳转场景进入主城  3、和服务器交互   
    /// </summary>
    private void OnStartGameClick()
    {

        if (mIsCreate)
        {
            //1、新建角色的情况：先新建，再登录
            Account_AddRoleReqProto proto = new Account_AddRoleReqProto();
            proto.ClassId = (byte)mCurrentSelectedClassId;
            proto.RoleNickName = mUISelectRoleView.NickName.text;
            proto.GameServerId = GlobalCache.Instance.Account_LastLoginServerId;    //Ich新增区服判断
            if (proto.RoleNickName.Equals("") || proto.RoleNickName == null)
            {
                UIDialogController.Instance.Show("请输入昵称！");
                return;
            }
            SocketManager.Instance.SendMessageToLocalServer(proto.ToArray());
        }
        else
        {
            //2、已有角色的情况：直接登录
            if (mCurrentSelectedRoleId == -1)
            {
                UIDialogController.Instance.Show("请选择一个角色");
                return;
            }
            else
            {
                EnterGameReq();
            }
        }



    }

    /// <summary>
    /// 删除角色第二步，因需要获取当前role的nickName和OK按钮的委托，故跑到主场景控制器取一波数据
    /// </summary>
    private void OnDeleteButtonClick()
    {
        //Debug.Log("2");

        mUISelectRoleView.DeleteRoleOKClick(GetRoleItem(mCurrentSelectedRoleId).RoleNickName, OnDeleteRoleClickCallback);
    }


    /// <summary>
    /// 删除角色第六步，从这里真正开始删除角色的操作
    /// </summary>
    private void OnDeleteRoleClickCallback()
    {
        //Debug.Log("6");
        Debug.Log("开始删除角色！");
        //TODO: 删除角色的具体实现逻辑
        Account_DeleteRoleReqProto proto = new Account_DeleteRoleReqProto();
        proto.RoleId = mCurrentSelectedRoleId;
        SocketManager.Instance.SendMessageToLocalServer(proto.ToArray());

    }


    #endregion


    #region 客户端操作

    #region 通用
    /// <summary>
    /// 通用：加载全部职业的prefab
    /// </summary>
    private void LoadClassObj()
    {
        mClassList = ClassDBModel.Instance.GetAllData();
        for (int i = 0; i < mClassList.Count; i++)
        {
            string path;
#if SINGLE_MODE && UNITY_EDITOR
            path = string.Format("Android/download/prefab/roleprefab/player/{0}.assetbundle", mClassList[i].PrefabName);
#elif SINGLE_MODE && UNITY_ANDROID
            path = string.Format(@"/download\prefab\roleprefab\player\{0}.assetbundle", mClassList[i].PrefabName);
#endif
            //把class.xls里的职业全部读出，并在assetbundle下找到对应的prefab加载
            GameObject obj = AssetBundleManager.Instance.LoadAB(path, mClassList[i].PrefabName);
            if (obj != null)
            {
                mClassDic.Add(mClassList[i].Id, obj);
            }
        }
    }


    /// <summary>
    /// 通用：设置3个台柱子是否显示
    /// </summary>
    /// <param name="isActive"></param>
    public void SetRoleSpawnPointsShow(bool isActive)
    {
        if (RoleSpawnPoints != null && RoleSpawnPoints.Length > 0)
        {
            for (int i = 0; i < RoleSpawnPoints.Length; i++)
            {
                RoleSpawnPoints[i].gameObject.SetActive(isActive);
            }
        }
    }

    #endregion


    #region 创建角色


    [SerializeField]
    private GameObject RotateTarget;    //旋转的目标
    private bool isRotating = false;    //是否在旋转中
    private float rotateAngle = 90f;    //一次旋转多少角度，暂定90
    private float targetAngle = 0;
    [SerializeField]
    private float mSpeed = 200f;

    /// <summary>
    /// 创建角色：拖拽令摄像机旋转
    /// </summary>
    /// <param name="direct"></param>
    private void OnDragEnable(int direct)
    {
        //摄像机旋转
        isRotating = true;
        targetAngle = RotateTarget.transform.eulerAngles.y + rotateAngle * direct;
    }

    /// <summary>
    /// 创建角色：把class.xls里所有职业的prefab全部克隆到场景中
    /// </summary>
    private void CloneAllRoles()
    {
        if (CreateRoleContainers == null && CreateRoleContainers.Length < 4) return;
        for (int i = 0; i < mClassList.Count; i++)
        {
            GameObject roleObj = Instantiate(mClassDic[mClassList[i].Id]);
            roleObj.transform.parent = CreateRoleContainers[i];
            roleObj.transform.localScale = Vector3.one;
            roleObj.transform.localPosition = Vector3.zero;
            roleObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            RoleController roleCtrl = roleObj.GetComponent<RoleController>();
            if (roleCtrl != null)
            {
                mRoleCtrlDic.Add(mClassList[i].Id, roleCtrl);   //把rolecontroller添加进字典，实际就是把角色prefab添加进去
            }
        }
    }


    /// <summary>
    /// 创建角色：点击职业头像委托的回调，功能是旋转到对应角色上，并加载职业描述
    /// </summary>
    /// <param name="classId"></param>
    /// <param name="rotateAngle"></param>
    private void OnClickClassImgCallback(int classId, int rotateAngle)
    {
        mCurrentSelectedClassId = classId;
        isRotating = true;
        targetAngle = rotateAngle;
        SetCurrentClassDesc(classId);
    }



    /// <summary>
    /// 创建角色：职业描述加载到UI上
    /// </summary>
    private void SetCurrentClassDesc(int classId)
    {
        //1.先从list里找对应ID的职业
        for (int i = 0; i < mClassList.Count; i++)
        {
            if (mClassList[i].Id == classId)
            {
                //2.根据ID取对应的职业描述
                mUISelectRoleView.descView.SetUI(mClassList[i].Name, mClassList[i].Desc);
                break;
            }
        }
        //2.显示一个选定高亮的动画
        for (int i = 0; i < mUISelectRoleView.ClassItems.Length; i++)
        {
            mUISelectRoleView.ClassItems[i].SetSelectedTween(classId);
        }

    }
    /// <summary>
    /// UI上点击创建角色按钮之后场景里的操作，这里和网络交互里判断roleCount=0的操作是一样的
    /// </summary>
    public void OnCreateButtonClick()
    {
        //新建角色界面，修改开始游戏按钮标记，切换一下UI、克隆所有角色、加载角色描述、自动赋予一个随机名字
        mIsCreate = true;
        mUISelectRoleView.SetUIObjShow(true);
        SetRoleSpawnPointsShow(true);
        CloneAllRoles();
        SetCurrentClassDesc(mCurrentSelectedClassId);
        mUISelectRoleView.RandomNameClick();    //新建界面自动赋值一个随机名字
        Debug.Log("clone done!");
    }


    #endregion

    #region 选择角色
    /// <summary>
    /// 选择角色：从roleitemList里取出所有我已有的角色信息，条件是roleID符合传入的id
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    private Account_LoginGameServerRespProto.RoleItem GetRoleItem(int roleId)
    {
        if (mMyRoleList != null)
        {
            for (int i = 0; i < mMyRoleList.Count; i++)
            {
                if (mMyRoleList[i].RoleId == roleId)
                {
                    return mMyRoleList[i];
                }
            }
        }
        return default(Account_LoginGameServerRespProto.RoleItem); //结构体不能为null，故返回他默认的值
    }

    /// <summary>
    /// 选择角色：加载我已有的角色prefab
    /// </summary>
    public void SetMyRolePrefab(int roleId)
    {
        Account_LoginGameServerRespProto.RoleItem item = GetRoleItem(roleId);
        //根据角色的职业ID，克隆对应的prefab

        CloneMyRole(item);
    }

    //选择角色：
    private void CloneMyRole(Account_LoginGameServerRespProto.RoleItem roleItem)
    {
        GameObject roleObj = Instantiate(mClassDic[roleItem.RoleClass]);
        roleObj.transform.parent = CreateRoleContainers[0]; //加载已有角色时默认使用第一个台柱子
        roleObj.transform.localScale = Vector3.one;
        roleObj.transform.localPosition = Vector3.zero;
        roleObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        RoleController roleCtrl = roleObj.GetComponent<RoleController>();
    }


    #endregion

    #region 删除角色
    /// <summary>
    /// 点击删除角色按钮后，场景里把prefab删除
    /// </summary>
    private void DeleteMyRolePrefab()
    {
        GameObject roleObj = CreateRoleContainers[0].GetChild(0).gameObject;
        Destroy(roleObj);
    }

    #endregion

    #endregion


    // Update is called once per frame
    void Update () {
        if (isRotating)
        {
            float rotatedEngle = Mathf.MoveTowardsAngle(RotateTarget.transform.eulerAngles.y, targetAngle, Time.deltaTime*mSpeed);
            RotateTarget.transform.eulerAngles = Vector2.up * rotatedEngle;
            if ((int)rotatedEngle ==rotateAngle)
            {
                RotateTarget.transform.eulerAngles = Vector2.up * rotatedEngle;
                isRotating = false;
            }
        }
	}

    /// <summary>
    /// 把当前场景监听的委托移除
    /// </summary>
    private void OnDestroy()
    {
        //监听服务器返回登陆信息的协议，里面包含角色信息
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_LoginGameServerRespProto, OnLoginGameServerResp);
        //监听服务器返回创建角色的协议
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_AddRoleRespProto, OnAddRoleServerResp);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_EnterGameRespProto, OnEnterGameServerResp);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDefine.Account_DeleteRoleRespProto, OnDeleteRoleServerResp);

    }
}
