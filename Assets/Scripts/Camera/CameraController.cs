//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 22:24:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public static CameraController Instance;

    [SerializeField]
    private GameObject cameraRoot;

    [SerializeField]
    private GameObject upAndDown;
    
    [SerializeField]
    private GameObject zoom;

    [SerializeField]
    private GameObject container;

    [SerializeField]
    private int moveSpeed;

    [SerializeField]
    private int rotateSpeed;

    [SerializeField]
    private int zoomSpeed;

	void Start () {
        Instance = this;
	}
	

	void Update () {
	
	}
    public void InitData()
    {
        upAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(upAndDown.transform.localEulerAngles.z, 30f, 80f));
        container.transform.localPosition = new Vector3(Mathf.Clamp(container.transform.localPosition.x, -5f, 5f), 0, 0);
    }
    public void SetCameraUpAndDown(int type) //type=1 up     type=-1 down
    {
        upAndDown.transform.Rotate(new Vector3(0, 0, moveSpeed * Time.deltaTime * type));
        upAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(upAndDown.transform.localEulerAngles.z, 30f, 80f));
    }

    public void SetCameraRotate(int type)   //type=1 left   type=-1 right
    {
        this.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime * type,0));
    }

    public void SetCameraZoom(int type)     //type=1 out     type=-1 in
    {
        container.transform.Translate(Vector3.forward*zoomSpeed*type*Time.deltaTime);
        container.transform.localPosition = new Vector3(Mathf.Clamp(container.transform.localPosition.x, -8f, 5f), 0, 0);

    }
    public void AutoLookAtPlayer(Vector3 pos)
    {
        container.transform.LookAt(pos);
    }
}
