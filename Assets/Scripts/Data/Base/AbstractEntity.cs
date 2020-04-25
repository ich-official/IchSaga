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
/// 数据实体抽象基类
/// </summary>
public abstract class AbstractEntity  {
    public int ID;  //方便DBModel操作，将ID这个属性放在基类，这就要求所有excel表都要有ID的属性且必须是int类型
}
