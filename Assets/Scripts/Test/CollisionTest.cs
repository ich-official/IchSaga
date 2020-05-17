using UnityEngine;
using System.Collections;

public class Leo_CollisionTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision info)
    {
        Debug.Log("enter");
        Debug.Log(info.collider.gameObject.tag);
    }
}
