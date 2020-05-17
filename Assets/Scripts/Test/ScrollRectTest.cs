//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-30 00:34:53
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class ScrollRectTest : MonoBehaviour {

    private GameObject item;
    public GameObject grid;

	void Start () {
        item = Resources.Load("UIItemTest") as GameObject;
        if (item != null)
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject obj = Instantiate(item)as GameObject;
                obj.transform.parent = grid.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
            }
        }
	}
	

	void Update () {
	
	}
}
