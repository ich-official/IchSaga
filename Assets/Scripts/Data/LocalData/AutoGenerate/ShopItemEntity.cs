//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using System.Collections;

/// <summary>
/// ShopItem实体，自动生成，不要轻易修改
/// </summary>
public partial class ShopItemEntity : AbstractEntity
{
    /// <summary>
    /// 商品名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 商品效果
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// 商品图片
    /// </summary>
    public string PicName { get; set; }

    /// <summary>
    /// 商品描述
    /// </summary>
    public string Desc { get; set; }

}
