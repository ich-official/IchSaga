//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-25 11:42:47
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Ich单机自用，返回一个xml数据表完整一条数据的初始化数据
/// </summary>
public class DBTableTemplateS : SingletonBase<DBTableTemplateS> {

    public Dictionary<string, string> DBTemplate = new Dictionary<string, string>();
    
    
    public Dictionary<string,string> GetTemplate(string DBName)
    {
        switch (DBName)
        {
            case "Account":
                GetAccountTemplate();
                break;
            case "Role":
                GetRoleTemplate();
                break;
        }
        return DBTemplate;
    }
    
    
    
    /// <summary>
    /// 获取Account.xml的数据模板
    /// </summary>
    /// <returns></returns>
    private Dictionary<string,string> GetAccountTemplate()
    {
        if (DBTemplate.Count != 0) DBTemplate.Clear();
        DBTemplate.Add("Id", "-1");
        DBTemplate.Add("Username", "-1");
        DBTemplate.Add("Pwd", "-1");
        DBTemplate.Add("Email", "eg@sample.com");
        DBTemplate.Add("Gem", "0");
        DBTemplate.Add("ChannelId", "1");
        DBTemplate.Add("LastLoginServerId", "1");
        DBTemplate.Add("LastLoginServerName", "双线1服");
        DBTemplate.Add("LastLoginServerTime", "-1");
        DBTemplate.Add("LastLoginRoleId", "-1");
        DBTemplate.Add("LastLoginRoleName", "-1");
        DBTemplate.Add("LastLoginRoleClassId", "-1");
        DBTemplate.Add("CreateTime", DateTime.Now.ToString("u"));
        DBTemplate.Add("UpdateTime", DateTime.Now.ToString("u"));
        DBTemplate.Add("DeviceId", "-1");
        DBTemplate.Add("DeviceModel", "-1");
        return DBTemplate;
    }

    /// <summary>
    /// 获取Role.xml的数据模板
    /// </summary>
    /// <returns></returns>
    private Dictionary<string,string> GetRoleTemplate()
    {
        if (DBTemplate.Count != 0) DBTemplate.Clear();
        DBTemplate.Add("Id", "-1");
        DBTemplate.Add("Status", "1");
        DBTemplate.Add("GameServerId", "-1");
        DBTemplate.Add("AccountId", "-1");
        DBTemplate.Add("ClassId", "-1");
        DBTemplate.Add("NickName", "Ich");
        DBTemplate.Add("Gender", "1");
        DBTemplate.Add("Level", "1");
        DBTemplate.Add("VIPLevel", "1");
        DBTemplate.Add("TotalRechargeGem", "0");
        DBTemplate.Add("Gem", "0");
        DBTemplate.Add("Gold", "0");
        DBTemplate.Add("Exp", "0");
        DBTemplate.Add("CurrEnergy", "20");
        DBTemplate.Add("MaxEnergy", "20");
        DBTemplate.Add("MaxHP", "100");
        DBTemplate.Add("MaxMP", "20");
        DBTemplate.Add("CurrHP", "100");
        DBTemplate.Add("CurrMP", "20");
        DBTemplate.Add("Attack", "80");
        DBTemplate.Add("Defense", "40");
        DBTemplate.Add("Hit", "100");
        DBTemplate.Add("Dodge", "10");
        DBTemplate.Add("Cri", "5");
        DBTemplate.Add("Res", "0");
        DBTemplate.Add("SumDPS", "1000");
        DBTemplate.Add("LastPassGameQuestId", "1"); //上次通关的关卡ID
        DBTemplate.Add("LastInWorldMapId", "1");    //上次通关的世界地图ID
        DBTemplate.Add("LastInWorldMapPos", "1");   //上次角色所在世界地图的坐标
        DBTemplate.Add("CreateTime", DateTime.Now.ToString("u"));
        DBTemplate.Add("UpdateTime", DateTime.Now.ToString("u"));
        DBTemplate.Add("Equip_WeaponId", "0");
        DBTemplate.Add("Equip_PantsId", "0");
        DBTemplate.Add("Equip_ClothesId", "0");
        DBTemplate.Add("Equip_BeltId", "0");
        DBTemplate.Add("Equip_CuffId", "0");
        DBTemplate.Add("Equip_NecklaceId", "0");
        DBTemplate.Add("Equip_ShoesId", "0");
        DBTemplate.Add("Equip_RingsId", "0");

        return DBTemplate;
    }
}
