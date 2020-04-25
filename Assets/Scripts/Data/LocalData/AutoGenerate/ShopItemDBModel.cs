//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// ShopItem数据管理，自动生成，不要轻易改动
/// </summary>
public partial class ShopItemDBModel : AbstractDBModel<ShopItemDBModel, ShopItemEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Leo_ShopItem.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ShopItemEntity CreateEntity(GameDataTableParser parse)
    {
        ShopItemEntity entity = new ShopItemEntity();
        entity.ID = parse.GetFieldValue("ID").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Value = parse.GetFieldValue("Value").ToFloat();
        entity.PicName = parse.GetFieldValue("PicName");
        entity.Desc = parse.GetFieldValue("Desc");
        return entity;
    }
}
