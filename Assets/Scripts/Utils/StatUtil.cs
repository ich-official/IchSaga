//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-24 15:18:22
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// 数据统计，本项目接入友盟
/// </summary>
public class StatUtil  {

    public static void Init()
    {
        //appid
    }

    #region 游戏内行为事件

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickName"></param>
    public static void Reg(int userId, string nickName)
    {

    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickName"></param>
    public static void Login(int userId, string nickName)
    {

    }

    /// <summary>
    /// 修改昵称
    /// </summary>
    /// <param name="nickName"></param>
    public static void ChangeNickName(string nickName)
    {

    }

    /// <summary>
    /// 角色升级
    /// </summary>
    /// <param name="level"></param>
    public static void UpLevel(int level)
    {

    }

    //======================================任务

    /// <summary>
    /// 任务开始
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="taskName"></param>
    public static void TaskBegin(int taskId, string taskName)
    {

    }

    /// <summary>
    /// 任务结束
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="taskName"></param>
    /// <param name="status"></param>
    public static void TaskEnd(int taskId, string taskName, int status)
    {

    }

    //======================================关卡

    /// <summary>
    /// 关卡开始
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="gameLevelName"></param>
    public static void GameLevelBegin(int gameLevelId, string gameLevelName)
    {

    }

    /// <summary>
    /// 关卡结束
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="gameLevelName"></param>
    /// <param name="status"></param>
    /// <param name="star">星级</param>
    public static void GameLevelEnd(int gameLevelId, string gameLevelName, int status, int star)
    {

    }

    /// <summary>
    /// 充值开始
    /// </summary>
    /// <param name="orderId">订单号</param>
    /// <param name="productId">产品编号</param>
    /// <param name="money">重置金额</param>
    /// <param name="type">币种</param>
    /// <param name="virtualMoney">虚拟货币</param>
    /// <param name="channelID">渠道号</param>
    public static void ChargeBegin(string orderId, string productId, double money, string type, double virtualMoney, string channelID)
    {

    }

    /// <summary>
    /// 充值结束
    /// </summary>
    public static void ChargeEnd()
    {
        //参数从上个方法取
    }

    /// <summary>
    /// 购买道具
    /// </summary>
    /// <param name="itemId">道具编号</param>
    /// <param name="itemName">名称</param>
    /// <param name="price">价格</param>
    /// <param name="count">数量</param>
    public static void BuyItem(int itemId, string itemName, int price, int count)
    {

    }

    /// <summary>
    /// 使用道具
    /// </summary>
    /// <param name="itemId">道具编号</param>
    /// <param name="itemName">名称</param>
    /// <param name="count">数量</param>
    /// <param name="usedType">用途</param>
    public static void ItemUsed(int itemId, string itemName, int count, int usedType)
    {

    }

    /// <summary>
    /// 自定义事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddEvent(string key, string value)
    {

    }

    #endregion
}
