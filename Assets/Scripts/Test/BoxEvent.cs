using UnityEngine;
using System.Collections;

public class Leo_BoxEvent : MonoBehaviour {

    public System.Action<GameObject> OnClickBox;


    public delegate void OnBoxChangeHandler();
    public OnBoxChangeHandler OnScaleChange;
    public OnBoxChangeHandler OnRotationChange;

	void Start () {
        this.OnScaleChange += ScaleChange;
        this.OnRotationChange += RotationChange;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ScaleChange()
    {
        transform.localScale += Vector3.one;
    }

    void RotationChange()
    {
        transform.Rotate(Vector3.one * 30);
    }


    public void ClickBox()
    {
        if (OnClickBox != null)
        {
            OnClickBox(gameObject);
        }
    }
    void ChangeBox()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //change box scale by delegate
            if (OnScaleChange != null)
            {
                OnScaleChange();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //change box rotation by delegate
            if (OnRotationChange != null)
            {
                OnRotationChange();
            }
        }
    }


    void OnDestroy()
    {
        this.OnScaleChange -= ScaleChange;
        this.OnRotationChange -= RotationChange;
    }
}
