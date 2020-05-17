using UnityEngine;
using System.Collections;

public class Leo_UIBase : MonoBehaviour {

   // public static Leo_UIBase Instance;



    void Awake()
    {
       // Instance = this;
        UIButton[] btns = GetComponentsInChildren<UIButton>(true);  //true:including hiding buttons
        for (int i = 0; i < btns.Length; i++)
        {
            UIEventListener.Get(btns[i].gameObject).onClick += BtnClick; //统一定义委托
        }
       // containerCenter = GameObject.Find("Container_Center").transform;    //Leo solution
        OnAwake();
    }

    private void BtnClick(GameObject obj)
    {
        OnBtnClick(obj);
    }
    protected virtual void OnAwake()
    {

    }
    void Start()
    {

        OnStart();
    }
    protected virtual void OnStart()
    {

    }

    protected virtual void OnBtnClick(GameObject obj) { }

    void OnDestroy()
    {
        LeoOnDestroy();
    }

    protected virtual void LeoOnDestroy()
    {

    }
}
