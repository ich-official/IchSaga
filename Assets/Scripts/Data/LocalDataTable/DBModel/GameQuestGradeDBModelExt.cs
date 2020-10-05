//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-09-28 13:44:32
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// GameQuestGradeDBModel��չ��������
/// </summary>
public partial class GameQuestGradeDBModel {

    /// <summary>
    /// beta�汾ֻȡgradeΪ0
    /// </summary>
    /// <param name="levelId"></param>
    /// <returns></returns>
    public GameQuestGradeEntity GetEntityByGameLevelId(int levelId)
    {
        for (int i = 0; i < mList.Count; i++)
        {
            if (mList[i].GameLevelId == levelId && mList[i].Grade == 0)
            {
                return mList[i];
            }
        }

        return null;
    }
}
