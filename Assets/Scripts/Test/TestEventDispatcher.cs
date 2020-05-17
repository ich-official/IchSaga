using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leo_TestEventDispatcher : SingletonBase<Leo_TestEventDispatcher>
{
    //老师的例子
    //public delegate void OnActionHandler(); //一般形式
    //public delegate void OnActionHandler(int eventID,params object[] obj);    //通过if判断eventID解决多种操作的形式
    public delegate void OnActionHandler(params object[] obj);  //观察者模式形式，和下列字典配合使用
    public OnActionHandler OnAction;

    public Dictionary<int, List<OnActionHandler>> dic = new Dictionary<int, List<OnActionHandler>>();

    public delegate void TestDel();
    public TestDel del1;
    public TestDel xiaban;

    //public static Leo_TestEventDispatcher instance;



    void Awake()
    {
        //instance = this;   
    }
    /*
    void Start()
    {
        Dictionary<int, string> mydic = new Dictionary<int, string>();
        mydic.Add(1, "aaa");
        if (mydic.ContainsKey(2))
        {

        }
        else
        {
            //mydic[2] = "bbb";
            //Debug.Log("OK:"+mydic[2]);
            mydic.Add(2, "ccc");
            Debug.Log("OK:" + mydic[2]);
        }
    }
     * */
    /// <summary>
    /// 自己默写的，错误已修改
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="myDelegate"></param>
    //public void AddEventListener(int eventID, List<OnActionHandler> myDelegate) 第二个参数不是list而是委托本身
    public void AddEventListener(int eventID, OnActionHandler actionHandler) 
    {
        if (dic.ContainsKey(eventID))
        {
            /*
            for (int i = 0; i < myDelegate.Count; i++)  //只传一个委托，不用重复赋值了
            {
                dic[eventID].Add(myDelegate[i]);
            }
             * */
            dic[eventID].Add(actionHandler);
        }
        else
        {
            List<OnActionHandler> list = new List<OnActionHandler>();
            list.Add(actionHandler);
            //dic.Add(eventID, list);自测时此写法无误，暂时按下方老师的写法写
            dic[eventID] = list;
        }
    }

    public void RemoveEventListener(int eventID, OnActionHandler actionHandler)
    {
        if (dic.ContainsKey(eventID)){
            List<OnActionHandler> list = dic[eventID];
            list.Remove(actionHandler);
            if (list.Count == 0)
            {
                dic.Remove(eventID);    //此ID下已经没有委托了，就把ID从字典中移除
            }
        }
    }

    public void Xiaban()//这个方法就是Dispatch，派发消息的
    {
        if (xiaban != null)
        {
            xiaban();
        }
    }
    /// <summary>
    /// 这个是dispatch完整版，xiaban只执行一个delegate，这个是执行整个list的delegate
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="obj"></param>
    public void Dispatch(int eventID, params object[] obj)
    {
        if (dic.ContainsKey(eventID))//先判断有没有ID，有才能执行
        {

            //List<OnActionHandler> list = dic[eventID];    //老师单独用一个list来处理，我是直接处理元数据，目前没发现问题，有问题时再修改
            if (dic[eventID] != null)//判断list是否为空
            {
                for (int i = 0; i < dic[eventID].Count; i++)//循环执行list中的delegate
                {
                    if (dic[eventID][i] != null)//常规判断，delegate不为空
                    {
                        dic[eventID][i](obj);//开始执行delegate，带上params参数
                    }
                    
                }
            }
        }
    }
 
}

