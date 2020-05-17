using UnityEngine;
using System.Collections;
//这个就是小明类，发布者，发布消息的
public class Leo_TestDelegate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
    void OnDestroy()
    {
      
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Leo_TestEventDispatcher.Instance.Xiaban();
            Leo_TestEventDispatcher.Instance.Dispatch(1, "下班了");
        }
	}


}
