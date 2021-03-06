
//===================================================
//Author：Ich
//CreateTime：2020-08-27 16:05:04
//Version: 1.0.0
//ProjectURL: https://github.com/ich-official/IchSaga
//Contact_Me: ich_official@163.com//Warning: This is an auto-generate file, please don't modify it casually !
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Chapter数据管理
/// </summary>
public partial class ChapterDBModel : AbstractDBModel<ChapterDBModel, ChapterEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Chapter.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ChapterEntity MakeEntity(GameDataTableParser parse)
    {
        ChapterEntity entity = new ChapterEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.ChapterName = parse.GetFieldValue("ChapterName");
        entity.GameLevelCount = parse.GetFieldValue("GameLevelCount").ToInt();
        entity.BG_Pic = parse.GetFieldValue("BG_Pic");
        entity.Uvx = parse.GetFieldValue("Uvx").ToFloat();
        entity.Uvy = parse.GetFieldValue("Uvy").ToFloat();
        return entity;
    }
}
