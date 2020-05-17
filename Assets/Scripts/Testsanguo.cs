using UnityEngine;
using System.Collections;

public class Testsanguo : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public GameObject cube;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                Vector3 direction = hit.point - cube.transform.position;
               // Debug.Log(direction);
            }
        }
	}


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position,-Input.mousePosition);//画射线

    }
}
