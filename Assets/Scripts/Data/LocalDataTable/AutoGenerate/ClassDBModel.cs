//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-06-21 18:40:52
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	Warning: This is an auto-generate file, please don't modify it casually !
//-----------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Class数据管理
/// </summary>
public partial class ClassDBModel : AbstractDBModel<ClassDBModel, ClassEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Class.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ClassEntity MakeEntity(GameDataTableParser parse)
    {
        ClassEntity entity = new ClassEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.JobPic = parse.GetFieldValue("JobPic");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.Desc = parse.GetFieldValue("Desc");
        entity.Attack = parse.GetFieldValue("Attack").ToInt();
        entity.Defense = parse.GetFieldValue("Defense").ToInt();
        entity.Hit = parse.GetFieldValue("Hit").ToInt();
        entity.Dodge = parse.GetFieldValue("Dodge").ToInt();
        entity.Cri = parse.GetFieldValue("Cri").ToInt();
        entity.Res = parse.GetFieldValue("Res").ToInt();
        entity.UsedPhyAttackIds = parse.GetFieldValue("UsedPhyAttackIds");
        entity.UsedSkillIds = parse.GetFieldValue("UsedSkillIds");
        return entity;
    }
}
