using UnityEngine;
using System.Collections;

public class Leo_LifeCycleTest : MonoBehaviour {

    void Awake()
    {
        Debug.Log("awake");
    }

	// Use this for initialization
	void Start () {
        Debug.Log("Start");
        Leo_BoxController.BoxInstance.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time < 0.1)
        {
            Debug.Log("Update");
        }
        
	}
    /*
    void LateUpdate()
    {
        if (Time.time < 1)
        {
            Debug.Log("LateUpdate");
        }
        
    }
    void FixedUpdate()
    {
        if (Time.time < 1)
        {
            Debug.Log("FixedUpdate");
        }
        
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }
     */
}
