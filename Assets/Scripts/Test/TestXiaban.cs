using UnityEngine;
using System.Collections;
//小明老婆类，用于接收小明派发的下班信息
public class Leo_TestXiaban : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Leo_TestEventDispatcher.instance.xiaban += ReceiveXiaban;
        Leo_TestEventDispatcher.Instance.AddEventListener(1, ReceiveXiaban);//1代表监听event的编号（要和发布者发布消息的那个一一对应），后面是委托执行后调用哪个方法
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDestroy()
    {
        //Leo_TestEventDispatcher.instance.xiaban -= ReceiveXiaban;
        Leo_TestEventDispatcher.Instance.RemoveEventListener(1, ReceiveXiaban);

    }

    void ReceiveXiaban()
    {
        Debug.Log("received xiaban");
    }
    void ReceiveXiaban(params object[] obj)
    {
        Debug.Log("received xiaban:"+obj[0]);
    }

}
