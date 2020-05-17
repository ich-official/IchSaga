//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
/// <summary>
/// 敌人出生点设置
/// </summary>
public class EnemySpawnPoint : MonoBehaviour {

    [SerializeField]
    private int mMaxCount;

    private float mPrevSpawnTime = 0f;

    private int mCurrentCount = 0;

    private string mEnemyName = null;

    void Update()
    {
        if (mCurrentCount < mMaxCount)
        {
            if (Time.time > mPrevSpawnTime + Random.Range(1.5f, 3f))
            {
                mPrevSpawnTime = Time.time;
                //刷怪
                GameObject obj = RoleManager.Instance.LoadRole("RoleModel_Xiaobing1");

                obj.transform.parent = this.transform;
                obj.transform.position = this.transform.position;
    
                PlayerController controller = obj.GetComponent<PlayerController>();
                controller.SpawnPosition = obj.transform.position;

                EnemyInfo enemyInfo = new EnemyInfo();
                enemyInfo.roleLocalID = 1;
                enemyInfo.roleServerID = 106807008888;
                enemyInfo.currentHP = enemyInfo.maxHP = 100;
                enemyInfo.username = "敌人";

                EnemyAI_Battle enemyAI = new EnemyAI_Battle(controller);
                controller.Init(RoleType.ENEMY, enemyInfo, enemyAI);
                controller.OnRoleDie = OnRoleDie;
                mCurrentCount++;
            }
        }
        
    }
    public void OnRoleDie(PlayerController controller)
    {
        mCurrentCount--;
        Destroy(controller.gameObject);
    }
}
