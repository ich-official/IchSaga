//-----------------------------------------------------------
//	Author: #AuthorName
//  CreateTime: #CreateTime
//  Version: #Version
//  ProjectURL: #ProjectURL
//  Contact_Me: #Email
//	OICQ_Group: #OICQ
//-----------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 模仿服务器上Role脚本
/// </summary>
public class RoleS  {
    #region 基本属性
    /// <summary>
    /// 角色帐号
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// 渠道号
    /// </summary>
    public int ChannelId { get; set; }

    /// <summary>
    /// 当前角色Id
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// 角色昵称
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    /// 角色等级
    /// </summary>
    public int Level { get; set; }

    /// <summary>
    /// 角色职业
    /// </summary>
    public int ClassId { get; set; }

    public int MaxHP { get; set; }
    public int CurrHP { get; set; }
    public int MaxMP { get; set; }
    public int CurrMP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int Hit { get; set; }
    public int Dodge { get; set; }
    public int Cri { get; set; }
    public int Res { get; set; }
    public int SumDPS { get; set; }

    /// <summary>
    /// 玩家拥有的元宝
    /// </summary>
    public int Gem { get; set; }

    /// <summary>
    /// 玩家拥有的金币
    /// </summary>
    public int Gold { get; set; }
    #endregion
    /*
    public ClientSocket Client_Socket { get; set; }

    #region 位置相关
    /// <summary>
    /// 上一个所在场景编号 用于跳转场景时候 给旧场景玩家发消息 告诉他们我离开
    /// </summary>
    public int PrevWorldMapId { get; set; }

    /// <summary>
    /// 最后进入的世界地图编号
    /// </summary>
    public int LastInWorldMapId { get; set; }

    /// <summary>
    /// 最后所在世界地图坐标
    /// </summary>
    public string LastInWorldMapPos { get; set; }

    /// <summary>
    /// y旋转
    /// </summary>
    public float YAngle { get; set; }
    #endregion

    #region UpdateLastInWorldMap 更新玩家在世界地图信息
    /// <summary>
    /// 更新玩家在世界地图信息
    /// </summary>
    public void UpdateLastInWorldMap()
    {
        Dictionary<string, object> param = new Dictionary<string, object>();

        param["@Id"] = RoleId;
        param["@LastInWorldMapId"] = LastInWorldMapId;
        param["@LastInWorldMapPos"] = LastInWorldMapPos;

        RoleCacheModel.Instance.Update("LastInWorldMapId=@LastInWorldMapId, LastInWorldMapPos=@LastInWorldMapPos", "Id=@Id", param);
    }
    #endregion

    #region Recharge 充值
    /// <summary>
    /// 充值
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="rechargeProductId"></param>
    public void Recharge(short channelId, int rechargeProductId)
    {
        //1.根据rechargeProductId 查询到充值产品信息
        RechargeProductEntity rechargeProductEntity = RechargeProductCacheModel.Instance.GetEntity(string.Format("[ChannelType]={0} and [ProductId]={1}", channelId, rechargeProductId));
        int virtualMoney = rechargeProductEntity.Virtual;

        bool rechargeSuccess = false;
        byte rechargeErrorType = 0;
        int remainDays = 0;

        //2.添加充值记录
        //⑴.检查充值的合法性
        //⑵.进行充值记录的添加

        //⑴.检查充值的合法性
        //如果是月卡 检查到期时间 是否可以购买
        //如果是礼包 检查是否已经购买过
        //如果是普通计费点 但是没有购买过 首充双倍

        RechargeRecordEntity monthCard = null;//定义月卡

        if (rechargeProductEntity.ProductType == 1)
        {
            //月卡
            //是否购买过月卡 或者购买时间超过一个月了
            MFReturnValue<List<RechargeRecordEntity>> retValue = RechargeRecordCacheModel.Instance.GetPageList(condition: string.Format("[RoleId]={0} and [ProductId]={1}", RoleId, rechargeProductId), orderby: "UpdateTime", pageSize: 1, pageIndex: 1);

            if (retValue.Value == null || retValue.Value.Count == 0)
            {
                rechargeSuccess = true;
            }
            else
            {
                //说明购买过月卡
                monthCard = retValue.Value[0];

                DateTime lastUpdateTime = retValue.Value[0].UpdateTime; //最后一次购买时间
                remainDays = (DateTime.Now - lastUpdateTime).Days;

                if (remainDays > 30)
                {
                    //超过30天 可以购买
                    rechargeSuccess = true;
                }
                else
                {
                    remainDays = 30 - remainDays - 1;
                    rechargeErrorType = 1;
                }
            }

        }
        else if (rechargeProductEntity.ProductType == 2)
        {
            //礼包
            RechargeRecordEntity rechargeRecordEntity = RechargeRecordCacheModel.Instance.GetEntity(string.Format("[RoleId]={0} and [ProductId]={1}", RoleId, rechargeProductId));
            if (rechargeRecordEntity == null) //没有买过礼包
            {
                rechargeSuccess = true;
            }
            else
            {
                rechargeErrorType = 2;
            }
        }
        else if (rechargeProductEntity.ProductType == 3)
        {
            rechargeSuccess = true;

            //检查是否已经购买过 没购买过就 首充双倍
            RechargeRecordEntity rechargeRecordEntity = RechargeRecordCacheModel.Instance.GetEntity(string.Format("[RoleId]={0} and [ProductId]={1}", RoleId, rechargeProductId));
            if (rechargeRecordEntity == null) //没有买过普通计费点
            {
                virtualMoney *= 2; //双倍
            }
        }

        //⑵.添加充值记录
        if (rechargeSuccess)
        {
            //查询是否有月卡记录 有的话先删除旧的
            if (monthCard != null)
            {
                RechargeRecordCacheModel.Instance.Delete(monthCard.Id);
            }

            RechargeRecordEntity rechargeRecordEntity = new RechargeRecordEntity();
            rechargeRecordEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
            rechargeRecordEntity.RoleId = RoleId;
            rechargeRecordEntity.ProductId = rechargeProductId;
            rechargeRecordEntity.CreateTime = DateTime.Now;
            rechargeRecordEntity.UpdateTime = DateTime.Now;

            RechargeRecordCacheModel.Instance.Create(rechargeRecordEntity);
        }


        //3.给玩家添加元宝 发放道具
        Dictionary<string, object> param = new Dictionary<string, object>();

        param["@Id"] = RoleId;


        RoleEntity entity = RoleCacheModel.Instance.GetEntity(RoleId);
        int currMoney = entity.Money;//角色当前身上的元宝
        currMoney += virtualMoney;//加上充值后 获得的元宝

        param["@Money"] = currMoney;
        param["@AddMoney"] = virtualMoney;

        this.Money += virtualMoney;

        RoleCacheModel.Instance.Update("[TotalRechargeMoney]=[TotalRechargeMoney]+@AddMoney, [Money]= @Money", "Id=@Id", param); //更新数据库

        //4.通知客户端元宝到账
        RoleData_RechargeReturnProto proto = new RoleData_RechargeReturnProto();
        proto.IsSuccess = rechargeSuccess;
        if (!rechargeSuccess)
        {


            switch (rechargeErrorType)
            {
                case 0:
                default:
                    proto.ErrorCode = 102002;
                    break;
                case 1:
                    proto.ErrorCode = 102004; //月卡购买失败
                    proto.RemainDay = remainDays;
                    break;
                case 2:
                    proto.ErrorCode = 102005; //礼包购买失败
                    break;
            }
        }
        else
        {
            proto.RechargeProductId = rechargeProductId;
            proto.RechargeProductType = rechargeProductEntity.ProductType;
            proto.Money = currMoney;
            proto.RemainDay = 29;
        }

        Client_Socket.SendMsg(proto.ToArray());
    }
    #endregion
    */
}
