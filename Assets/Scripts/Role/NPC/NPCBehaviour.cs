//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-08-08 15:06:02
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// NPC运行时控制器
/// </summary>
public class NPCBehaviour : MonoBehaviour {

    /// <summary>
    /// NPC头顶显示名字的point
    /// </summary>
    [SerializeField]
    private Transform modelHeadPoint;

    /// <summary>
    /// NPC头顶的显示名字的UI
    /// </summary>
    private GameObject NPCHeadUI;

    [SerializeField]
    private int NPCId;

    private string NPCName;
    public NPCHeadBarView headBar = null;

    // Use this for initialization
    void Start () {
        Init();
        

    }
	
    public void Init()
    {
        List<NPCEntity> mNPCList = NPCDBModel.Instance.GetAllData();
        for(int i = 0; i < mNPCList.Count; i++)
        {
            if(mNPCList[i].Id== NPCId)
            {
                NPCName = mNPCList[i].Name;
                break;
            }
        }
        Debug.Log("NPC name:" + NPCName);
    }
	// Update is called once per frame
	void Update () {
		
	}



    /// <summary>
    /// 克隆头顶UI的prefab，然后给prefab赋值  
    /// </summary>
    public void InitRoleHeadUI()
    {
        if (NPCHeadUI != null)
        {
            Debug.Log("NPC head已生成！");
            return;
        }
        if (UIRoleHeadItems.Instance != null  &&modelHeadPoint != null)
        {
            //克隆部分
            NPCHeadUI = ResourcesManager.Instance.Load(ResourcesManager.ResourceType.UIOTHER, "NPCHeadUGUI");
            NPCHeadUI.transform.parent = UIRoleHeadItems.Instance.gameObject.transform;
            NPCHeadUI.transform.localScale = Vector3.one;
            NPCHeadUI.transform.localPosition = Vector3.zero;
            headBar = NPCHeadUI.GetComponent<NPCHeadBarView>();

            headBar.Init(modelHeadPoint, NPCName);

        }
    }
}
