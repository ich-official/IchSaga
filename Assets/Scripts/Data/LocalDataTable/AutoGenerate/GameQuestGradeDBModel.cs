
//===================================================
//Author：Ich
//CreateTime：2020-09-29 02:06:38
//Version: 1.0.0
//ProjectURL: https://github.com/ich-official/IchSaga
//Contact_Me: ich_official@163.com//Warning: This is an auto-generate file, please don't modify it casually !
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// GameQuestGrade数据管理
/// </summary>
public partial class GameQuestGradeDBModel : AbstractDBModel<GameQuestGradeDBModel, GameQuestGradeEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "GameQuestGrade.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override GameQuestGradeEntity MakeEntity(GameDataTableParser parse)
    {
        GameQuestGradeEntity entity = new GameQuestGradeEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.GameLevelId = parse.GetFieldValue("GameLevelId").ToInt();
        entity.Grade = parse.GetFieldValue("Grade").ToInt();
        entity.Desc = parse.GetFieldValue("Desc");
        entity.Type = parse.GetFieldValue("Type").ToInt();
        entity.Parameter = parse.GetFieldValue("Parameter");
        entity.ConditionDesc = parse.GetFieldValue("ConditionDesc");
        entity.Exp = parse.GetFieldValue("Exp").ToInt();
        entity.Gold = parse.GetFieldValue("Gold").ToInt();
        entity.CommendFighting = parse.GetFieldValue("CommendFighting").ToInt();
        entity.TimeLimit = parse.GetFieldValue("TimeLimit").ToFloat();
        entity.Star1 = parse.GetFieldValue("Star1").ToFloat();
        entity.Star2 = parse.GetFieldValue("Star2").ToFloat();
        entity.Equip = parse.GetFieldValue("Equip");
        entity.EquipImg = parse.GetFieldValue("EquipImg");
        entity.EquipDesc = parse.GetFieldValue("EquipDesc");
        entity.Item = parse.GetFieldValue("Item");
        entity.ItemImg = parse.GetFieldValue("ItemImg");
        entity.ItemDesc = parse.GetFieldValue("ItemDesc");
        entity.Material = parse.GetFieldValue("Material");
        entity.MaterialImg = parse.GetFieldValue("MaterialImg");
        entity.MaterialDesc = parse.GetFieldValue("MaterialDesc");
        return entity;
    }
}
