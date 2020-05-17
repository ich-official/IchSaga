//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-29 00:00:02
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class GetTypeTest : MonoBehaviour {


	void Start () {
        List<ClientEntityBase> list = new List<ClientEntityBase>();
        GameServerEntity e1 = new GameServerEntity();
        GameServerListEntity e2 = new GameServerListEntity();
        list.Add(e1);
        list.Add(e2);
        Debug.Log(list[0].GetType());
        ClientEntityBase base1 = e1;
	}
	

	void Update () {
	
	}
}
