
//===================================================
//Author：Ich
//CreateTime：2020-06-22 18:26:24
//Version: 1.0.0
//ProjectURL: https://github.com/ich-official/IchSaga
//Contact_Me: ich_official@163.com//Warning: This is an auto-generate file, please don't modify it casually !
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// NPC数据管理
/// </summary>
public partial class NPCDBModel : AbstractDBModel<NPCDBModel, NPCEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "NPC.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override NPCEntity MakeEntity(GameDataTableParser parse)
    {
        NPCEntity entity = new NPCEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.HalfBodyPic = parse.GetFieldValue("HalfBodyPic");
        entity.Talk = parse.GetFieldValue("Talk");
        return entity;
    }
}
