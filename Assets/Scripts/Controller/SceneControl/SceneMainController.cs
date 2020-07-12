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

//原名CitySceneCtrl
/// <summary>
/// 大厅场景的主控制器
/// </summary>
public class SceneMainController : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;

    public Transform playerSpawnPoint;
    void Awake()
    {
        Leo_UISceneManager.Instance.LoadSceneUI(Leo_UISceneManager.SceneUIType.MAIN);

        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
            FingerEvent.Instance.OnPlayerClick += OnPlayerClick;
            FingerEvent.Instance.OnFingerZoom += OnFingerZoom;
        }
    }

    void Start()
    {
        //Clone player
        GameObject obj = null;
        obj = RoleManager.Instance.LoadRole("RoleModel_Cike");
        obj.transform.position = playerSpawnPoint.position;
        //找到当前player
        GlobalInit.Instance.currentPlayer = obj.GetComponent<RoleController>();
#if UNITY_EDITOR
        if (GlobalInit.Instance.currentPlayerUsername == "" || GlobalInit.Instance.currentPlayerUsername == null)
        {
            GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, new RoleInfoBase() { username = "绫祈丿er",currentHP=500,maxHP=500 }, new PlayerAI_Battle(obj.GetComponent<RoleController>()));
        }
        else
        {
            GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, new RoleInfoBase() { username = GlobalInit.Instance.currentPlayerUsername, currentHP = 500, maxHP = 500 }, new PlayerAI_Battle(obj.GetComponent<RoleController>()));
        }
#else
        GlobalInit.Instance.currentPlayer.Init(RoleType.PLAYER, new RoleInfoBase() { username = GlobalInit.Instance.currentPlayerUsername, currentHP = 500, maxHP = 500 }, new PlayerAI_Battle(obj.GetComponent<RoleController>()));
#endif

        Leo_UIPlayerInfo.Instance.SetPlayerInfo();

        //46课以后异步累加加载后，判断后一个场景是否加载完毕
        if (DelegateDefine.Instance.OnSceneLoadDone != null)
        {
            DelegateDefine.Instance.OnSceneLoadDone();
        }
    }

    //点击UI时，人物还是会移动，增加UICamera的判断，当点击UI时，不让人物移动
    void OnPlayerClick()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hitAll = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Role"));
        if (hitAll.Length > 0)
        {
            RoleController enemy = hitAll[0].collider.gameObject.GetComponent<RoleController>();
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
                if (hit.collider.gameObject.tag == "Plane")
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
    /// 摄像机左右、上下旋转角度
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
