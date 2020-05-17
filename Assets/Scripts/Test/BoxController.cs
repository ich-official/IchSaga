using UnityEngine;
using System.Collections;

public class Leo_BoxController : MonoBehaviour {


    private static Leo_BoxController _BoxInstance;
    public static Leo_BoxController BoxInstance
    {
        get
        {
            if (_BoxInstance == null)
            {
                GameObject boxGameObject = new GameObject("Leo_BoxController");
                _BoxInstance = boxGameObject.AddComponent<Leo_BoxController>();
                DontDestroyOnLoad(boxGameObject);
            }
            return _BoxInstance;
        }
        set { _BoxInstance = value; }
    }

    [SerializeField]
    private Transform boxAreaPlane; //boxes will create in this area

    [SerializeField]
    private Transform boxParent;    //boxes will created under this object

    private GameObject boxPrefab;



    private int cloneCount; //clone box count



    public Leo_BoxController GetInstance()
    {
        BoxInstance = this;
        return BoxInstance;
    }
    void Awake()
    {
        this.GetInstance();
    }
    // register delegates
	void Start () {
        cloneCount = 0;
        boxPrefab = Resources.Load("LeoPrefabs/Box") as GameObject;

	}

    //unregister delegates

	// Update is called once per frame
	void Update () {
        GenerateBox();	
	}




    void GenerateBox()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject box = Instantiate(boxPrefab);
            box.transform.position = new Vector3(boxAreaPlane.position.x + cloneCount * 2, boxAreaPlane.position.y + 1, boxAreaPlane.position.z);
            box.transform.parent = boxParent;
            // box.name = "box" + cloneCount.ToString();
            box.transform.localScale = Vector3.one;
            box.GetComponent<Leo_BoxEvent>().OnClickBox = DoClickBox;
            cloneCount++;
        }
    }
    private void DoClickBox(GameObject obj)
    {
        Debug.Log("click a box!");
        GameObject.Destroy(obj);
    }
}
