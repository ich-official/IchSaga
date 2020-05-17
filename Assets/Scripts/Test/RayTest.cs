using UnityEngine;
using System.Collections;

public class Leo_RayTest : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        //Debug.Log(Mathf.Pow(2, 3));
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            
            ray= Camera.main.ScreenPointToRay(Input.mousePosition);
           // Debug.DrawLine(Camera.main.transform.position,Input.mousePosition,  Color.red,1f);
            
            //只碰撞一个物体   
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "Item") {
                    Destroy(hit.collider.gameObject);
                    Debug.Log("destroyed");
                }
            }
             
            //射线可穿透，碰撞一条线上多个物体
            /*
            RaycastHit[] hits = Physics.RaycastAll(ray,Mathf.Infinity,1<<LayerMask.NameToLayer("Plane"));
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length;i++) {
                    Debug.Log(hits[i].collider.gameObject.name);
                }
                    
            }
            */
        }
	}
}
