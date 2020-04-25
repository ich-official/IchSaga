//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 23:47:11
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
/// <summary>
/// 手指滑动事件
/// </summary>
public class FingerEvent : MonoBehaviour {
    public static FingerEvent Instance;

    public delegate void FingerDragEvent(FingerDir dir);
    public FingerDragEvent OnFingerDrag;

    public System.Action OnPlayerClick;

    public System.Action<ZoomType> OnFingerZoom;
    private Vector2 mLastFingerPos; //last finger tap position

    private Vector2 mDir;   //finger drag direction

    private int mPrevFinger = -1;   //last finger action(flag)
    public enum FingerDir
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }   //finger move direction 
    public enum ZoomType
    {
        IN,
        OUT
    }   //finger zoom type

    private Vector2 mFinger0PosMobile;
    private Vector2 mFinger1PosMobile;
    private Vector2 mFinger0LastPosMobile;
    private Vector2 mFinger1LastPosMobile;
    void Awake()
    {
        Instance = this;
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //向上滚
            if (OnFingerZoom != null)
            {
               OnFingerZoom(ZoomType.IN);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //向下滚
            if (OnFingerZoom != null)
            {
               OnFingerZoom(ZoomType.OUT);
            }
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 1)   //touching with over one finger
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved) //1st or 2nd finger is moving
            {
                mFinger0PosMobile = Input.GetTouch(0).position;
                mFinger1PosMobile = Input.GetTouch(1).position;
                if(Vector2.Distance(mFinger0LastPosMobile,mFinger1LastPosMobile)<Vector2.Distance(mFinger0PosMobile,mFinger1PosMobile)){
                    OnFingerZoom(ZoomType.IN);
                    mFinger0LastPosMobile = mFinger0PosMobile;
                    mFinger1LastPosMobile = mFinger1PosMobile;
                }else{
                     OnFingerZoom(ZoomType.OUT);
                }
            }
        }
#endif



    }

    void OnEnable()
    {
        //启动时调用，这里开始注册手势操作的事件。

        //按下事件： OnFingerDown就是按下事件监听的方法，这个名子可以由你来自定义。方法只能在本类中监听。下面所有的事件都一样！！！
        FingerGestures.OnFingerDown += OnFingerDown;
        //抬起事件
        FingerGestures.OnFingerUp += OnFingerUp;
        //开始拖动事件
        FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
        //拖动中事件...
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
        //拖动结束事件
        FingerGestures.OnFingerDragEnd += OnFingerDragEnd;
        //按下事件后调用一下三个方法
        FingerGestures.OnFingerStationaryBegin += OnFingerStationaryBegin;

    }

    void OnDisable()
    {
        //关闭时调用，这里销毁手势操作的事件
        //和上面一样
        FingerGestures.OnFingerDown -= OnFingerDown;
        FingerGestures.OnFingerUp -= OnFingerUp;
        FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
        FingerGestures.OnFingerDragEnd -= OnFingerDragEnd;
        FingerGestures.OnFingerStationaryBegin -= OnFingerStationaryBegin;
    }

    void OnFingerDown(int fingerIndex,Vector2 fingerPos)
    {
        mPrevFinger = 1;
    }

    void OnFingerUp(int fingerIndex, Vector2 fingerPos, float timeHeldDown)
    {
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

        if (mPrevFinger == 1)
        {
            mPrevFinger = -1;
            if (OnPlayerClick != null)
            {
                OnPlayerClick();
            }

        }
    }

    void OnFingerDragBegin(int fingerIndex, Vector2 fingerPos, Vector2 startPos)
    {
        mLastFingerPos = fingerPos;
        mPrevFinger = 2;
    }

    void OnFingerDragMove(int fingerIndex, Vector2 fingerPos, Vector2 delta)
    {
        mDir = fingerPos - mLastFingerPos;
        mPrevFinger = 3;
        if (mDir.y < mDir.x && mDir.y > -mDir.x)
        {
            //right
            if (OnFingerDrag != null)
            {
                OnFingerDrag(FingerDir.RIGHT);
            }
        }
        else if (mDir.y > mDir.x && mDir.y < -mDir.x)
        {
            //left
            if (OnFingerDrag != null)
            {
                OnFingerDrag(FingerDir.LEFT);
            }
        }
        else if (mDir.y > mDir.x && mDir.y> -mDir.x)
        {
            //up
            OnFingerDrag(FingerDir.UP);
        }
        else
        {
            //down
            OnFingerDrag(FingerDir.DOWN);
        }

    }

    void OnFingerDragEnd(int fingerIndex, Vector2 fingerPos)
    {
        mPrevFinger = 4;
    }

    void OnFingerStationaryBegin(int fingerIndex, Vector2 fingerPos)
    {

    }

}
