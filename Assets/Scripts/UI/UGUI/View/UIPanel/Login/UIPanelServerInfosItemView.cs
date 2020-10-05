//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-30 20:18:09
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/// <summary>
/// �������飬
/// </summary>
public class UIPanelServerInfosItemView : MonoBehaviour {

    [SerializeField]
    private Sprite[] mServerStatusList;  //ȫ������״̬   0��ά��  1������  2������
    [SerializeField]    
    private Image mServerStatus;  //����������״̬
    [SerializeField]
    private Text mServerName;   //����������

    private GameServerEntity entity = new GameServerEntity();   //�����item����Ϣ���ؿ��ٵ�½�����õ�
    public Action<GameServerEntity> OnServerInfoItemClick;   //�������ҳ��ѡ���Ӧ����
    private string mIp;
    private int mPort;
	void Start () {
        GetComponent<Button>().onClick.AddListener(ThisItemClick);
	}

    //���һ������������buttonʱ
    public void ThisItemClick()
    {
        if (OnServerInfoItemClick != null)
        {
            OnServerInfoItemClick(entity);
        }
    }

	void Update () {
	
	}

    public void SetUI(GameServerEntity entity)
    {
        this.entity = entity;
        mServerStatus.overrideSprite = mServerStatusList[entity.RunStatus];
        mServerName.text = entity.Name;
        mIp = entity.Ip;
        mPort = entity.Port;
    }
}
