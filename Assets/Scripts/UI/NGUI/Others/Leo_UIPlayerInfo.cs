using UnityEngine;
using System.Collections;

public class Leo_UIPlayerInfo : MonoBehaviour {

    public static Leo_UIPlayerInfo Instance;

    [SerializeField]
    private UILabel playerName;

    [SerializeField]
    private UISprite currentHP;

    [SerializeField]
    private UISprite currentMP;

    [SerializeField]
    private UISprite currentLevel;
    void Awake()
    {
        Instance = this;
    }
	void Start () {
        if (GlobalInit.Instance.currentPlayer != null)
        {
            GlobalInit.Instance.currentPlayer.OnRoleHurt += OnPlayerHurt;
        }
	}

    public void OnPlayerHurt()
    {
        currentHP.fillAmount = (float)GlobalInit.Instance.currentPlayer.currentRoleInfo.currentHP / GlobalInit.Instance.currentPlayer.currentRoleInfo.maxHP;
    }
    public void SetPlayerInfo()
    {
        playerName.text =GlobalInit.Instance.currentPlayer.currentRoleInfo.username;
    }
}
