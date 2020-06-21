//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-22 01:46:42
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using System;

/// <summary>
/// 
/// </summary>
public class AccountDBModelServer : DBModelServerBase  {
    Dictionary<string, object> dic = new Dictionary<string, object>();
    private string[] AccountDBField = { "Id", "UserName", "Pwd", "Phone", "Email",
          "Gem", "ChannelId", "LastLoginServerId","LastLoginServerName","LastLoginServerTime",
          "LastLoginRoleId","LastLoginRoleName","LastLoginRoleClassId","CreateTime","UpdateTime",
          "DeviceIdentifier","DeviceModel"};  //Account表所有字段

    #region AccountDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private AccountDBModelServer()
    {
        //把数据库字段名和数据实体一一对应到字典里
    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static AccountDBModelServer instance = null;
    public static AccountDBModelServer Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new AccountDBModelServer();
                    }
                }
            }
            return instance;
        }
    }
    #endregion


    #region API方法 登陆/注册

    //1.用户名是否存在
    //2.如果不存在 添加数据库
    public CallbackArgs Register(Dictionary<string, string> targets)
    {
        CallbackArgs args = new CallbackArgs();
        bool isSuccess = false;
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("Username", targets["Username"]);
        List<Dictionary<string, string>> result = XMLHelper.Instance.Query("Account.xml", "Account", null, dic);
        if (result == null)
        {
            //reader为null，查询失败
            args = GenerateErrorMsg(Constant.SQL_ERROR, "sql查询错误！");
            return args;
        }
        if (result.Count!=0) //查询到有数据，注册失败
        {
            args = GenerateErrorMsg(Constant.ACCOUNT_USER_EXIST, "account exist! 已存在");
            return args;
        }
        else
        {
            isSuccess = XMLHelper.Instance.Add("Account.xml", "Account", targets);
            if (isSuccess == false)
            {
                //操作XML报错
                args = GenerateErrorMsg(Constant.SQL_ERROR, "sql查询错误注册失败！");
                return args;
            }
            else
            {
                //注册成功，把这条注册的用户信息返回去
                result = XMLHelper.Instance.Query("Account.xml", "Account", null, dic);
                int temp = -1;
                AccountEntity entity = new AccountEntity();
                entity.Id = int.Parse(result[0]["Id"]);
                entity.UserName = targets["Username"];
                entity.Gem = (int.Parse(result[0]["Gem"])==-1)?0:temp;
                entity.LastLoginServerId = (int.Parse(result[0]["LastLoginServerId"])==-1)?1:temp;
                entity.LastLoginServerName = result[0]["LastLoginServerName"];
                //entity.LastLoginServerName = "双线1服";    //注册后立即查询没有有效信息，暂时写死
                entity.LastLoginServerIp = "127.0.0.1"; //单机版用不到，暂时写死
                entity.LastLoginServerPort = 1000 + entity.LastLoginServerId - 1; //简易写，Id从1开始，但Port从1000开始
                args = GenerateSuccessMsg(entity);

                return args;
            }
        }
    }

    //1.验证用户名是否存在
    //2.验证密码是否和用户名匹配
    public CallbackArgs Login(Dictionary<string,string> conditions)
    {
        //把这里改写成XMLHelper格式即可，只改持久层，其他逻辑层代码不用动
        CallbackArgs args = new CallbackArgs();
        List<Dictionary<string, string>> result=XMLHelper.Instance.Query("Account.xml", "Account", null, conditions);
        if (result == null)
        {
            //reader为null，查询失败
            args = GenerateSqlErrorMsg();
            return args;
        }
        if (result.Count==0) //没有用户名，登陆失败
        {
            args = GenerateErrorMsg(Constant.ACCOUNT_USER_NOT_EXIST, "account not exist! 用户不存在");
            return args;
        }
        else
        {
            List<ClientEntityBase> list = new List<ClientEntityBase>();
            string dbUsername = "";
            string dbPwd = "";
            string dbId = "-1";
            //开始验证用户名和密码
            for(int i = 0; i < result.Count; i++)
            {
                dbUsername = result[i]["Username"];
                dbPwd= result[i]["Pwd"];
                if(conditions["Username"].Equals(dbUsername) && conditions["Pwd"].Equals(dbPwd))
                {
                    //用户名和密码匹配
                    AccountEntity entity = new AccountEntity();
                    entity.Id = int.Parse(result[i]["Id"]);
                    entity.UserName = dbUsername;
                    //使用三目运算，解决value为""时赋值报错的问题
                    entity.Gem = int.Parse(result[i]["Gem"]);
                    entity.LastLoginServerId = int.Parse(result[i]["LastLoginServerId"]); 
                    entity.LastLoginServerName = result[i]["LastLoginServerName"]; 
                    entity.LastLoginServerIp = "127.0.0.1"; //单机版用不到，暂时写死
                    entity.LastLoginServerPort = 1000 + entity.LastLoginServerId - 1; //简易写，Id从1开始，但Port从1000开始
                    args = GenerateSuccessMsg(entity);
                    break;
                }
                else
                {
                    //密码错误
                    args = GenerateErrorMsg(Constant.ACCOUNT_PWD_WRONG, "Password error! 用户名或密码错误");
                    break;
                }
            }
            return args;
        }
    }


    #endregion



    #region Old
    /// <summary>
    /// 安全的获取reader中的字段和值，防止出现因为返回null而报错的问题
    /// </summary>
    /// <param name="reader"></param>
    private AccountEntity GetAccountEntitySafe(SqliteDataReader reader)
    {
        AccountEntity entity = new AccountEntity();

        return entity;
    }


    //1.用户名是否存在
    //2.如果不存在 添加数据库
    public CallbackArgs Registersqlite(string username, string pwd, string channelId)
    {
        //Leo_ReturnValues values = new Leo_ReturnValues();
        CallbackArgs args = new CallbackArgs();
        string[] columns = { "Username", "Pwd", "ChannelId", "CreateTime", "UpdateTime" };
        //两种日期写法都正确，第二种写法更符合sqlite的格式
        string[] values = { username, pwd, channelId, DateTime.Now.ToString("u"), DateTime.Now.ToString("u") };
        //string[] values = { username, pwd, channelId, "datetime('now','localtime')", "datetime('now','localtime')" };
        int isSuccess = 0;
        //1.验证用户名是否存在
        //select Id from Account where username=username
        SqliteDataReader reader = SqliteHelper.Instance.Query("IchSagaDBAccount", "Account", new string[] { "Id" }, new string[] { "Username" }, new string[] { username });
        if (reader == null)
        {
            //reader为null，查询失败
            args = GenerateErrorMsg(Constant.SQL_ERROR, "sql查询错误！");
            return args;
        }
        if (reader.HasRows) //查询到有数据，注册失败
        {
            args = GenerateErrorMsg(Constant.ACCOUNT_USER_EXIST, "account exist! 已存在");
            return args;
        }
        else
        {
            isSuccess = SqliteHelper.Instance.Add("IchSagaDBAccount", "Account", columns, values);
            if (isSuccess == 0)
            {
                //sql语句执行数据条数为0，注册失败
                args = GenerateErrorMsg(Constant.SQL_ERROR, "sql查询错误注册失败！");
                return args;
            }
            else
            {
                //注册成功
                SqliteDataReader tempReader = SqliteHelper.Instance.Query("IchSagaDBAccount", "Account", new string[] { "*" }, new string[] { "Username" }, new string[] { username });
                while (tempReader.Read())
                {
                    AccountEntity entity = new AccountEntity();
                    entity.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    entity.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                    //使用三目运算，解决value为""时赋值报错的问题
                    entity.Gem = (reader.GetValue(reader.GetOrdinal("Gem")) == "") ? reader.GetInt32(reader.GetOrdinal("Gem")) : 0;
                    entity.LastLoginServerId = (reader.GetValue(reader.GetOrdinal("LastLoginServerId")) == "") ? reader.GetInt32(reader.GetOrdinal("LastLoginServerId")) : -1;
                    entity.LastLoginServerName = (reader.GetValue(reader.GetOrdinal("LastLoginServerName")) == "") ? reader.GetString(reader.GetOrdinal("LastLoginServerName")) : "";
                    entity.LastLoginServerIp = "127.0.0.1";
                    entity.LastLoginServerPort = 1000 + entity.LastLoginServerId - 1; //Id从1开始，但Port从1000开始
                    args = GenerateSuccessMsg(entity);
                    break;
                }
                return args;
            }
        }
    }



    //1.验证用户名是否存在
    //2.验证密码是否和用户名匹配
    public CallbackArgs LoginSqlite(string username, string pwd)
    {
        CallbackArgs args = new CallbackArgs();
        string[] columns = { "Username", "Pwd" };
        string[] values = { username, pwd };
        SqliteDataReader reader = SqliteHelper.Instance.Query("IchSagaDBAccount", "Account", new string[] { "*" }, new string[] { "Username" }, new string[] { username });
        if (reader == null)
        {
            //reader为null，查询失败
            args = GenerateSqlErrorMsg();
            return args;
        }
        if (!reader.HasRows) //没有用户名，登陆失败
        {
            args = GenerateErrorMsg(Constant.ACCOUNT_USER_NOT_EXIST, "account not exist! 用户不存在");
            return args;
        }
        else
        {
            List<ClientEntityBase> list = new List<ClientEntityBase>();
            string dbPwd = "";
            int Id = -1;
            //开始验证密码
            while (reader.Read())
            {
                dbPwd = reader.GetString(reader.GetOrdinal("Pwd"));
                Id = reader.GetInt32(reader.GetOrdinal("Id"));
                if (pwd.Equals(dbPwd))
                {
                    //密码正确
                    AccountEntity entity = new AccountEntity();
                    entity.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                    entity.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                    //使用三目运算，解决value为""时赋值报错的问题
                    entity.Gem = (reader.GetValue(reader.GetOrdinal("Gem")).ToString() != "") ? reader.GetInt32(reader.GetOrdinal("Gem")) : 0;
                    entity.LastLoginServerId = (reader.GetValue(reader.GetOrdinal("LastLoginServerId")).ToString() != "") ? reader.GetInt32(reader.GetOrdinal("LastLoginServerId")) : -1;
                    entity.LastLoginServerName = (reader.GetValue(reader.GetOrdinal("LastLoginServerName")).ToString() != "") ? reader.GetString(reader.GetOrdinal("LastLoginServerName")) : "";
                    entity.LastLoginServerIp = "127.0.0.1";
                    entity.LastLoginServerPort = 1000 + entity.LastLoginServerId - 1; //Id从1开始，但Port从1000开始
                    args = GenerateSuccessMsg(entity);
                    break;
                }
                else
                {
                    //密码错误
                    args = GenerateErrorMsg(Constant.ACCOUNT_PWD_WRONG, "Password error! 密码错误");
                    break;
                }
            }

            return args;
        }
    }
    #endregion

}
