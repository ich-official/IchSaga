//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-28 23:25:22
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using System;

/// <summary>
/// 
/// </summary>
public class GameServerDBModelServer : DBModelServerBase {

    #region GameServerDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private GameServerDBModelServer()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static GameServerDBModelServer instance = null;
    public static GameServerDBModelServer Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new GameServerDBModelServer();
                    }
                }
            }
            return instance;
        }
    }
    #endregion



    #region 功能逻辑 获取新区信息/获取区服列表/获取全部区服信息/更新最后登录信息
    public CallbackArgs GetNewGameServer()
    {
        
        return null;
    }
    /// <summary>
    /// type=1,k获取游戏区服列表，仅列表
    /// </summary>
    /// <returns></returns>
    public CallbackArgs GetGameServerList()
    {
        CallbackArgs args = new CallbackArgs();
        //SqliteDataReader reader = SqliteHelper.Instance.Query("IchSagaDBAccount", "GameServer", new string[] { "*" });
        int ServerCount = XMLHelper.Instance.Query("GameServer.xml", "GameServer").Count;
        List<ClientEntityBase> list = new List<ClientEntityBase>();
        int pageIndex = (ServerCount / 10) + 1; //取最大页数，例如1-10服应该是第2页，
        GameServerListEntity entity = null;
        for (int i = 1; i <= ServerCount; i++)
        {
            //每10个服务器一组，处理每组第1个服务器
            if (i % 10 == 1)   //10个一组的第一个，数据库ID从1开始不是0
            {
                entity = new GameServerListEntity();
                entity.PageIndex = pageIndex;
                pageIndex--;
                entity.Name = i.ToString();
                list.Add(entity);
            }
            //每10个服务器一组，处理每组第10个服务器
            if (i % 10 == 0)
            {
                //10个一组的最后一个
                if (entity != null)
                {
                    entity.Name += " - " + i.ToString() + "服";

                }
            }
            //处理最后一组最后一个服务器
            if (i == ServerCount)
            {
                //10个一组的最后一个
                if (entity != null)
                {
                    entity.Name += " - " + i.ToString() + "服";
                }
            }
        }
        //大区列表一般从新到旧排列，这里把顺序翻转一下
        list.Reverse();
        args = GenerateSuccessMsg(list, "省略json，直接读list");
        return args;
    }

    /// <summary>
    /// type=2,获取全部区服信息，主要获取大区名、IP、端口号，其他次要
    /// </summary>
    /// <returns></returns>
    public CallbackArgs GetGameServerInfos()
    {
        CallbackArgs args = new CallbackArgs();
        List<Dictionary<string,string>> results = XMLHelper.Instance.Query("GameServer.xml", "GameServer");
        if (results == null)
        {
            //reader为null，查询失败
            args = GenerateSqlErrorMsg();
            return args;
        }
        if (results.Count==0) //没有查询到数据
        {
            args = GenerateErrorMsg(Constant.GAMESERVER_NO_SERVER_INFO, "网络错误，无区服信息！");
            return args;
        }
        else
        {
            List<ClientEntityBase> list = new List<ClientEntityBase>();
            for(int i = 0; i < results.Count; i++)
            {
                GameServerEntity entity = new GameServerEntity();
                entity.Id = int.Parse(results[i]["Id"]);
                entity.RunStatus = int.Parse(results[i]["RunStatus"]);
                entity.IsRecommend = bool.Parse(results[i]["IsRecommend"]);
                entity.IsNew = bool.Parse(results[i]["IsNew"]);
                entity.Name = results[i]["Name"];
                entity.Ip = results[i]["Ip"];
                entity.Port = int.Parse(results[i]["Port"]); 
                list.Add(entity);
            }
            args = GenerateSuccessMsg(list, "省略json，直接读list");
        }
        return args;
    }

    /// <summary>
    /// type=4,更新最后登录信息
    /// </summary>
    /// <returns></returns>
    public CallbackArgs UpdateLastLoginInfo(string Id, string lastLoginServerId, string lastLoginServerName)
    {
        CallbackArgs args = new CallbackArgs();
        bool isSuccess = false;
        //string[] columns = { "lastLoginServerId", "lastLoginServerName", "UpdateTime" };
        //string[] values = { lastLoginServerId.ToString(), lastLoginServerName, DateTime.Now.ToString("u") };
        Dictionary<string, string> targets = new Dictionary<string, string>();
        targets.Add("LastLoginServerId", lastLoginServerId);
        targets.Add("LastLoginServerName", lastLoginServerName);
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("Id", Id);
        //update Account set k1=v1,k2=v2,k3=v3 where Id=Id
        isSuccess = XMLHelper.Instance.Update("Account.xml", "Account", targets, conditions);
        if (isSuccess)
        {
            args = GenerateSuccessMsg(new ClientEntityBase(), "更新登陆信息成功！");


        }
        else
        {          
            //sql语句执行数据条数为0，注册失败
            args = GenerateErrorMsg(Constant.SQL_ERROR, "sql查询错误！");
            return args;
        }
        return args;
    }

    #endregion


    #region Old
    public CallbackArgs GetNewGameServerSqlite()
    {
        CallbackArgs args = new CallbackArgs();

        SqliteDataReader reader = SqliteHelper.Instance.Query("IchSagaDBAccount", "GameServer", new string[] { "*" }, new string[] { "IsNew" }, new string[] { "true" });
        if (reader == null)
        {
            //reader为null，查询失败
            args = GenerateSqlErrorMsg();
            return args;
        }
        if (!reader.HasRows) //没有查询到数据
        {
            args = GenerateErrorMsg(Constant.GAMESERVER_NO_SERVER_INFO, "网络错误，无区服信息！");
            return args;
        }
        else
        {
            List<ClientEntityBase> list = new List<ClientEntityBase>();
            GameServerEntity entity = new GameServerEntity();
            while (reader.Read())
            {
                entity.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                entity.RunStatus = reader.GetInt32(reader.GetOrdinal("RumStatus"));
                entity.Name = reader.GetString(reader.GetOrdinal("Name"));
                entity.Ip = reader.GetString(reader.GetOrdinal("Ip"));
                entity.Port = reader.GetInt32(reader.GetOrdinal("Port"));
                list.Add(entity);
                break;  //读取最新的一个就可以
            }
            args = GenerateSuccessMsg(list, "省略json，直接读list");
        }
        return args;
    }
    /// <summary>
    /// type=1,获取游戏区服列表，仅列表
    /// </summary>
    /// <returns></returns>
    public CallbackArgs GetGameServerListSqlite()
    {
        CallbackArgs args = new CallbackArgs();
        //SqliteDataReader reader = SqliteHelper.Instance.Query("IchSagaDBAccount", "GameServer", new string[] { "*" });
        int ServerCount = SqliteHelper.Instance.GetRowCount("IchSagaDBAccount", "select count(1) from GameServer");
        List<ClientEntityBase> list = new List<ClientEntityBase>();
        int pageIndex = (ServerCount / 10) + 1; //取最大页数，例如1-10服应该是第2页，
        GameServerListEntity entity = null;
        for (int i = 1; i <= ServerCount; i++)
        {
            //每10个服务器一组，处理每组第1个服务器
            if (i % 10 == 1)   //10个一组的第一个，数据库ID从1开始不是0
            {
                entity = new GameServerListEntity();
                entity.PageIndex = pageIndex;
                pageIndex--;
                entity.Name = i.ToString();
                list.Add(entity);
            }
            //每10个服务器一组，处理每组第10个服务器
            if (i % 10 == 0)
            {
                //10个一组的最后一个
                if (entity != null)
                {
                    entity.Name += " - " + i.ToString() + "服";

                }
            }
            //处理最后一组最后一个服务器
            if (i == ServerCount)
            {
                //10个一组的最后一个
                if (entity != null)
                {
                    entity.Name += " - " + i.ToString() + "服";
                }
            }
        }
        //大区列表一般从新到旧排列，这里把顺序翻转一下
        list.Reverse();
        args = GenerateSuccessMsg(list, "省略json，直接读list");
        return args;
    }
    /// <summary>
    /// type=2,获取全部区服信息，主要获取大区名、IP、端口号，其他次要
    /// </summary>
    /// <returns></returns>
    public CallbackArgs GetGameServerInfosSqlite()
    {
        CallbackArgs args = new CallbackArgs();
        SqliteDataReader reader = SqliteHelper.Instance.Query("IchSagaDBAccount", "GameServer", new string[] { "*" });
        if (reader == null)
        {
            //reader为null，查询失败
            args = GenerateSqlErrorMsg();
            return args;
        }
        if (!reader.HasRows) //没有查询到数据
        {
            args = GenerateErrorMsg(Constant.GAMESERVER_NO_SERVER_INFO, "网络错误，无区服信息！");
            return args;
        }
        else
        {
            List<ClientEntityBase> list = new List<ClientEntityBase>();
            while (reader.Read())
            {
                GameServerEntity entity = new GameServerEntity();
                entity.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                entity.RunStatus = reader.GetInt32(reader.GetOrdinal("RunStatus"));
                entity.IsRecommend = reader.GetBoolean(reader.GetOrdinal("IsRecommend"));
                entity.IsNew = reader.GetBoolean(reader.GetOrdinal("IsNew"));
                entity.Name = reader.GetString(reader.GetOrdinal("Name"));
                entity.Ip = reader.GetString(reader.GetOrdinal("Ip"));
                entity.Port = reader.GetInt32(reader.GetOrdinal("Port"));
                list.Add(entity);
            }
            args = GenerateSuccessMsg(list, "省略json，直接读list");
        }
        return args;
    }
    /// <summary>
    /// type=4,更新最后登录信息
    /// </summary>
    /// <returns></returns>
    public CallbackArgs UpdateLastLoginInfoSqlite(int Id, int lastLoginServerId, string lastLoginServerName)
    {
        CallbackArgs args = new CallbackArgs();
        int isSuccess = 0;
        string[] columns = { "lastLoginServerId", "lastLoginServerName", "UpdateTime" };
        string[] values = { lastLoginServerId.ToString(), lastLoginServerName, DateTime.Now.ToString("u") };
        //update Account set k1=v1,k2=v2,k3=v3 where Id=Id
        isSuccess = SqliteHelper.Instance.Update("IchSagaDBAccount", "Account", columns, values, "Id", Id.ToString());
        if (isSuccess == 0)
        {
            //sql语句执行数据条数为0，注册失败
            args = GenerateErrorMsg(Constant.SQL_ERROR, "sql查询错误！");
            return args;
        }
        else
        {
            args = GenerateSuccessMsg(new ClientEntityBase(), "更新登陆信息成功！");
        }
        return args;
    }
    #endregion

}
