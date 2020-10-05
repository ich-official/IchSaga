//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-22 02:24:34
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


/// <summary>
/// 大厅场景的主控制器//=CitySceneCtrl,=WorldMapSceneCtrl
/// </summary>
public class SceneMainController : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;

    public Transform playerSpawnPoint;

    private UIRootMainCityView mMainCityView;

    [SerializeField]
    private GameObject[] NPCList;
    void Awake()
    {
        //Leo_UISceneManager.Instance.LoadSceneUI(Leo_UISceneManager.SceneUIType.MAIN);

        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
            FingerEvent.Instance.OnPlayerClick += OnPlayerClick;
            FingerEvent.Instance.OnFingerZoom += OnFingerZoom;
        }
    }

    void Start()
    {
        #region Old 早期克隆role prefab方式，现在弃用
        /*
        //Clone player，
        GameObject obj = null;
        obj = RoleManager.Instance.LoadRole("RoleModel_Cike");
        //obj.transform.position = playerSpawnPoint.position;
        //找到当前player
        GlobalInit.Instance.currentPlayer = obj.GetComponent<RoleController>();
#if UNITY_EDITOR
        if (GlobalInit.Instance.currentPlayerUsername == "" || GlobalInit.Instance.currentPlayerUsername == null)
        {
            GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, new RoleInfoBase() { username = "绫祈丿er", currentHP = 500, maxHP = 500 }, new PlayerAI_Battle(obj.GetComponent<RoleController>()));
        }
        else
        {
            GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, new RoleInfoBase() { username = GlobalInit.Instance.currentPlayerUsername, currentHP = 500, maxHP = 500 }, new PlayerAI_Battle(obj.GetComponent<RoleController>()));
        }
#else
        GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, new RoleInfoBase() { username = GlobalInit.Instance.currentPlayerUsername, currentHP = 500, maxHP = 500 }, new PlayerAI_Battle(obj.GetComponent<RoleController>()));
#endif
        */
        #endregion
        if (mMainCityView==null)
        {
            Debug.Log("main UI load done!");
            mMainCityView = UIRootController.Instance.LoadUIRoot(UIRootController.UIRootType.MAIN, OnLoadUIDone).GetComponent<UIRootMainCityView>();
        }

    }
    /// <summary>
    /// 该场景的UI加载完毕时
    /// </summary>
    private void OnLoadUIDone()
    {

        Debug.Log("main!");
        //46课以后异步累加加载后，判断后一个场景是否加载完毕
        if (DelegateDefine.Instance.OnSceneLoadDone != null)
        {
            
            DelegateDefine.Instance.OnSceneLoadDone();
        }

        RoleController.Instance.SetRoleInfoMainCity();
        SetNPCInfoMainCity();

        RoleManager.Instance.InitMyRole();  //现代版克隆角色方式
        if (GlobalInit.Instance.currentPlayer != null)
        {
            GlobalInit.Instance.currentPlayer.gameObject.transform.localPosition = playerSpawnPoint.position;
        }

        //Leo_UIPlayerInfo.Instance.SetPlayerInfo();

    }



    /// <summary>
    /// 设置主城NPC的信息
    /// </summary>
    private void SetNPCInfoMainCity()
    {
        //当前测试NPC的坐标是-20,3,-28
        for(int i = 0; i < NPCList.Length; i++)
        {
            NPCList[i].GetComponent<NPCBehaviour>().InitRoleHeadUI();
        }
    }

    //点击UI时，人物还是会移动，增加UICamera的判断，当点击UI时，不让人物移动
    void OnPlayerClick()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;  //UI挡住ray，防止角色点击UI同时走路
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitAll = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Role"));
        if (hitAll.Length > 0)
        {
            RoleBehaviour enemy = hitAll[0].collider.gameObject.GetComponent<RoleBehaviour>();
            if (enemy.currentRoleType == RoleType.ENEMY)
            {
                //如果射线碰撞到了敌人，就跑到攻击范围内进行攻击
                GlobalInit.Instance.currentPlayer.viewedEnemy = enemy;

            }
        }
        else
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Road")
                {
                    if (GlobalInit.Instance.currentPlayer != null)
                    {
                        GlobalInit.Instance.currentPlayer.viewedEnemy = null;
                        GlobalInit.Instance.currentPlayer.ToRun(hit.point);
                    }
                }

            }
        }
        //如果UICamera存在
        if (UICamera.currentCamera != null)
        {
            Ray rayUI = UICamera.currentCamera.ScreenPointToRay(Input.mousePosition);
            //检测rayUI是否碰撞了UI层
            if (Physics.Raycast(rayUI, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI")))
            {
                //发现是UI时，返回，不让角色移动 
                return;
            }
        }
        
        
    }

    /// <summary>
    /// 摄像机左右、上下旋转角度(x)
    /// 功能已更变，不再旋转摄像机，改为让角色前后左右移动
    /// </summary>
    /// <param name="dir"></param>
    void OnFingerDrag(FingerEvent.FingerDir dir)
    {
        switch (dir)
        {
            case FingerEvent.FingerDir.LEFT:
                CameraController.Instance.SetCameraRotate(-1);
                break;
            case FingerEvent.FingerDir.RIGHT:
                CameraController.Instance.SetCameraRotate(1);
                break;
            case FingerEvent.FingerDir.UP:
                CameraController.Instance.SetCameraUpAndDown(-1);
                break;
            case FingerEvent.FingerDir.DOWN:
                CameraController.Instance.SetCameraUpAndDown(1);
                break;
        }
    }

    /// <summary>
    /// 摄像机拉远、拉进
    /// </summary>
    /// <param name="zoomType"></param>

    void OnFingerZoom(FingerEvent.ZoomType zoomType)
    {
        switch (zoomType)
        {
            case FingerEvent.ZoomType.IN:
                CameraController.Instance.SetCameraZoom(-1);
                break;
            case FingerEvent.ZoomType.OUT:
                CameraController.Instance.SetCameraZoom(1);
                break;
        }
    }

    void OnDestroy()
    {
        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
            FingerEvent.Instance.OnPlayerClick -= OnPlayerClick;
            FingerEvent.Instance.OnFingerZoom -= OnFingerZoom;
        }

    }
}
