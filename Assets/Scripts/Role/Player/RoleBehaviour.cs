//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
/// <summary>
/// 角色在场景中的控制器，控制摄像机跟随、移动、战斗参数等，还有显示头上血条等,=RoleCtrl
/// </summary>
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(FunnelModifier))]
public class RoleBehaviour : RoleBehaviourBase{

    Ray ray;
    RaycastHit hit;
    /// <summary>
    /// 模型头顶各种UI的出生点（昵称、血条、称号等）
    /// </summary>
    [SerializeField]
    private Transform modelHeadPoint;

    /// <summary>
    /// 模型头顶的真正UI
    /// </summary>
    private  GameObject modelHeadUI;
    [HideInInspector]
    public Vector3 targetPos;   //移动的目标点，其他类需要这个属性


    public int moveSpeed; 

    [HideInInspector]
    public CharacterController CharacterController;


    public RoleType currentRoleType = RoleType.NONE;

    [SerializeField]
    public Animator mAnimator;

    [HideInInspector]
    public RoleInfoBase currentRoleInfo = null;

    [HideInInspector]
    public IRoleAI currentPlayerAI;

    [HideInInspector]
    public RoleFSMStateManager currentPlayerFSM;

    public RoleHeadBarView headBar = null;

    /// <summary>
    /// 角色出生点
    /// </summary>
    [HideInInspector]
    public Vector3 SpawnPosition;

    /// <summary>
    /// 视野范围，1-10表示搜索玩家的范围，0为不搜索
    /// </summary>
    public float viewRange;

    /// <summary>
    /// 巡逻范围
    /// </summary>
    public float patrolRange;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float attackRange;
    /// <summary>
    /// 视野范围内发现的敌人，
    /// </summary>
    public RoleBehaviour viewedEnemy;

    /// <summary>
    /// 角色受伤扣血，委托修改玩家UI上的血条
    /// </summary>
    public delegate void RoleHurt();
    public RoleHurt OnRoleHurt;

    public System.Action<RoleBehaviour> OnRoleDie;

    #region 寻路相关变量
    private Seeker mSeeker; //寻路核心组件
    [HideInInspector]
    public ABPath astarPath;    //走到目标位置的一条路径
    [HideInInspector]
    public int astarCurrentNodeIndex = 1;  //当前要去的目标位置，以关键节点计数，0是玩家的位置，1是最近一个要移动的位置，=AStarCurrWayPointIndex

    #endregion





    void Start () {
        CharacterController = GetComponent<CharacterController>();
        mSeeker = GetComponent<Seeker>();   //上方已定义RequireComponent，此处一定可找到
        if (currentRoleType == RoleType.PLAYER)
        {
            CameraController.Instance.InitData();
        }
        currentPlayerFSM = new RoleFSMStateManager(this); //状态机只实例化一次
        ToFight();
        InitRoleHeadUI();
        
	}
    /// <summary>
    /// 这个角色刚载入到场景中时，需要初始化如下参数：roleType,roleInfo,AI
    /// </summary>
    public void Init(RoleType roleType, RoleInfoBase roleInfo, IRoleAI AI)
    {
        currentRoleType = roleType;
        currentRoleInfo = roleInfo;
        currentPlayerAI = AI;
    }

    /// <summary>
    /// 克隆头顶UI的prefab，然后给prefab赋值  
    /// </summary>
    private void InitRoleHeadUI()
    {
        if (UIRoleHeadItems.Instance != null &&
            currentRoleInfo!=null &&
            modelHeadPoint!=null)
        {
            //克隆部分
            modelHeadUI = ResourcesManager.Instance.Load(ResourcesManager.ResourceType.UIOTHER, "RoleHeadUGUI");
            modelHeadUI.transform.parent = UIRoleHeadItems.Instance.gameObject.transform;
            modelHeadUI.transform.localScale = Vector3.one;
            modelHeadUI.transform.localPosition = Vector3.zero;
            headBar = modelHeadUI.GetComponent<RoleHeadBarView>();

            //赋值部分，最后一个参数使用特别的写法，等同于使用一个变量经过判断赋值后传参
            headBar.Init(modelHeadPoint, currentRoleInfo.RoleNickName,isShowHP:(currentRoleType==RoleType.PLAYER?false:true));
        }
    }

    void Update()
    {
        if (currentPlayerFSM != null)
        {
            currentPlayerFSM.OnUpdate();
        }
        DoAI();
        //MovePlayer();
        DestroyBox();
        CameraAutoFollow(); 

    }

    void DoAI()
    {
        if (currentPlayerAI != null)
        {
            currentPlayerAI.DoAI();
        }
    }

    #region Camera跟随相关
    /// <summary>
    /// 让摄像机自动跟随player
    /// </summary>
    void CameraAutoFollow()
    {
        if (currentRoleType != RoleType.PLAYER) return; //是player时，摄像机才跟随
        if (CameraController.Instance == null) return;
        CameraController.Instance.transform.position = this.transform.position;
        CameraController.Instance.AutoLookAtPlayer(this.transform.position);
        CameraRotate();
        CameraUpAndDown();
        CameraZoom();
    }

    void CameraRotate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            CameraController.Instance.SetCameraRotate(1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            CameraController.Instance.SetCameraRotate(-1);
        }
    }

    void CameraUpAndDown()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            CameraController.Instance.SetCameraUpAndDown(1);
        }
        if (Input.GetKey(KeyCode.E))
        {
            CameraController.Instance.SetCameraUpAndDown(-1);
        }
    }

    void CameraZoom()
    {
        if (Input.GetKey(KeyCode.W))
        {
            CameraController.Instance.SetCameraZoom(1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            CameraController.Instance.SetCameraZoom(-1);
        }
    }
    #endregion

    /// <summary>
    /// 点击屏幕移动角色、插值转身、点击销毁箱子
    /// </summary>
    void MovePlayer()
    {
        if (!CharacterController.isGrounded)
        {
            CharacterController.Move(transform.position + new Vector3(0, -1000, 0) - transform.position);
        }
    }

    /// <summary>
    /// 以player为圆心检测周围碰撞体
    /// </summary>
    void VerifyColliderWithSphareRay()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Collider[] colliders = Physics.OverlapSphere(GameObject.FindGameObjectWithTag("Player").transform.position, 3);
            for (int i = 0; i < colliders.Length; i++)
            {
                Debug.Log("sphare collider:" + colliders[i].gameObject.name);
            }
        }
    }

    void DestroyBox()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit,1<<LayerMask.NameToLayer("Item")))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Item")
                {                    
                    Leo_BoxEvent boxEvent = hit.collider.GetComponent<Leo_BoxEvent>();
                    if (boxEvent != null)
                    {
                        boxEvent.ClickBox();
                    }
                }
            }

            
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       // Gizmos.DrawSphere(this.transform.position, 3);  //画实心球
        Gizmos.DrawWireSphere(this.transform.position, 3);//画空心球

    }
    void OnDestroy()
    {
        if (modelHeadUI != null)
        {
            Destroy(modelHeadUI);
        }
    }

    #region 角色动画控制
    public void ToIdle()
    {
        currentPlayerFSM.ChangeState(RoleState.Idle);
    }
    public void ToFight()
    {
        currentPlayerFSM.ChangeState(RoleState.Fight);   //现代版暂时如此修改
    }

    //MoveTo方法，引入寻路project后改造算法20.07.17
    public void ToRun(Vector3 target)
    {
        //if (targetPos == Vector3.zero) return;
        targetPos = target;
        //currentPlayerFSM.ChangeState(RoleState.Run);

        //使用A*计算路径
        mSeeker.StartPath(transform.position, target,(Path p)=>{
            if (!p.error)
            {
                //TODO:路径计算无问题
                astarPath = (ABPath)p;
                if(Vector3.Distance(astarPath.endPoint,new Vector3(astarPath.originalEndPoint.x, astarPath.endPoint.y, astarPath.originalEndPoint.z)) > 0.5f)
                {
                    //如果寻路的终点和寻路的endpoint>0.5f
                    //endPoint：玩家指定的目标点，该点可能无法到达
                    //originalEndPoint：A*算法算出距离目标点最近的一个位置，最好情况是originalEndPoint=endPoint
                    Debug.Log("不能到达目标点");
                    astarPath = null;
                }
                astarCurrentNodeIndex = 1;
                currentPlayerFSM.ChangeState(RoleState.Run);
            }
            else
            {
                //TODO：路径计算有问题
                Debug.Log("寻路错误!");
                astarPath = null;
            }
        });  //以角色自身作为起始点，以target作为结束点，外加一个匿名委托作为回调
    }
    public void ToAttack()
    {
        if (viewedEnemy == null ) return;
        //播放攻击动画，播放被攻击者受伤动画
        currentPlayerFSM.ChangeState(RoleState.Attack);
        viewedEnemy.ToHurt(20, 0.5f);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="attackValue">攻击力</param>
    /// <param name="delayTime">延时几秒触发伤害</param>
    public void ToHurt(int attackValue,float delayTime)
    {
        StartCoroutine(ToHurtCoroutine(attackValue, delayTime));
    }
    public IEnumerator ToHurtCoroutine(int attackValue, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        //此处根据需求计算最终伤害数值，此测试代码简单计算
        int hurtValue = attackValue - UnityEngine.Random.Range(1, 5);
        currentRoleInfo.currentHP -= hurtValue;
        headBar.SetHUDTextAndHPBar(hurtValue,(float)currentRoleInfo.currentHP/currentRoleInfo.maxHP);
        if (OnRoleHurt != null) OnRoleHurt();
        if (currentRoleInfo.currentHP <= 0) {
            currentPlayerFSM.ChangeState(RoleState.Die);
        }
        else
        {
            currentPlayerFSM.ChangeState(RoleState.Hurt);
        }

    }

    public void ToDie()
    {
        currentPlayerFSM.ChangeState(RoleState.Die);
    }
    #endregion
}
