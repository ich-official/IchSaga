
//===================================================
//Author：Ich
//CreateTime：2020-08-29 16:04:42
//Version: 1.0.0
//ProjectURL: https://github.com/ich-official/IchSaga
//Contact_Me: ich_official@163.com//Warning: This is an auto-generate file, please don't modify it casually !
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// GameQuest数据管理
/// </summary>
public partial class GameQuestDBModel : AbstractDBModel<GameQuestDBModel, GameQuestEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "GameQuest.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override GameQuestEntity MakeEntity(GameDataTableParser parse)
    {
        GameQuestEntity entity = new GameQuestEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.ChapterID = parse.GetFieldValue("ChapterID").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.SceneName = parse.GetFieldValue("SceneName");
        entity.SmallMapImg = parse.GetFieldValue("SmallMapImg");
        entity.isBoss = parse.GetFieldValue("isBoss").ToInt();
        entity.Ico = parse.GetFieldValue("Ico");
        entity.PosInMap = parse.GetFieldValue("PosInMap");
        entity.DlgPic = parse.GetFieldValue("DlgPic");
        entity.CameraRotation = parse.GetFieldValue("CameraRotation");
        entity.Audio_BG = parse.GetFieldValue("Audio_BG");
        return entity;
    }
}
