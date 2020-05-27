//-----------------------------------------------------------
//	Author: Ich
//  CreateTime: 2020-04-25 14:55:17
//  Version: 1.0.0
//  ProjectURL: https://github.com/ich-official/IchSaga
//  Contact_Me: ich_official@163.com
//	
//-----------------------------------------------------------

using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;
using System.Threading;

/// <summary>
/// 对sqlite数据库进行操作的帮助类
/// </summary>
public class SqliteHelper : SingletonBase<SqliteHelper>{
    private SqliteConnection connection;
    private SqliteCommand command;
    private SqliteDataReader dataReader;
    private SqliteParameter parameter;
    private SqliteDataAdapter adapter;
    private static readonly object obj = new object();
    #region 打开与关闭数据库
    //打开数据库
    public void Connect(string DBName)
    {
        try
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            //数据库存放在 Plugins->Android->assets
            //Android平台下persistentDataPath就是这个，但此文件夹不可读写，需要复制到其他位置进行操作，跟解压AB包类似
            string path = Application.dataPath + "/Plugins/Android/assets/" + DBName + ".db";
#elif UNITY_ANDROID
            //string Path  = jar:file://" + Application.dataPath + "!/assets/" + "你的文件";
            string path = Application.persistentDataPath + "/" + DBName+".db";
            //string Path=("URI=file:" + appDBPath);
#endif
            //新建数据库连接
            connection = new SqliteConnection("URI=file:" + path);
            //打开数据库
            connection.Open();
            Debug.Log("打开数据库" + DBName);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    //关闭数据库
    public void Disconnect()
    {

        if (command != null)
        {

            command.Dispose();

        }

        command = null;

        if (dataReader != null)
        {

            dataReader.Dispose();

        }

        dataReader = null;

        if (connection != null)
        {

            connection.Close();
            Debug.Log("Disconnected db.");

        }

        connection = null;



    }
    public void Dispose()
    {
        Disconnect();
    }

    #endregion


    /// <summary>
    /// 向数据库添加一条数据,insert into tableName(columns) values(values)
    /// </summary>
    /// <param name="DBName">数据库名</param>
    /// <param name="tableName">表名</param>
    /// <param name="obj">DBModle对象，用于获取一个DB模型，从而获取一条数据的全部字段值</param>
    /// <returns></returns>
    public int Add(string dbName, string tableName, string[] columns,string[] values)
    {
        if (connection == null)
        {
            Connect(dbName);
        }
        string sql = "INSERT INTO " + tableName + "(" + columns[0];
        for (int i = 1; i < columns.Length; ++i)
        {
            sql += ", " + columns[i];
        }

        sql += ") VALUES ('" + values[0]+"'";

        for (int i = 1; i < values.Length; ++i)
        {

            sql += ", " +"'" +values[i]+ "'";

        }

        sql += ")";
        int result=ExecuteNonQuery(sql);
        return result;
    }

    public SqliteDataReader Delete(string dbName, string tableName, string[] columns, string[] colsValues)
    {
        if (connection == null)
        {
            Connect(dbName);
        }
        string sql = "DELETE FROM " + tableName + " WHERE " + columns[0] + " = " + colsValues[0];

        for (int i = 1; i < colsValues.Length; ++i)
        {

            sql += " or " + columns[i] + " = " + colsValues[i];
        }
        return ExecuteQuery(sql);
    }

    public int Update(string dbName,string tableName, string[] columns, string[] colsValues, string conditions=null, string conValues=null)
    {
        if (connection == null)
        {
            Connect(dbName);
        }
        string sql = "UPDATE " + tableName + " SET " + columns[0] + " = " +"'" +colsValues[0]+"'";

        for (int i = 1; i < colsValues.Length; ++i)
        {

            sql += ", " + columns[i] + " =" +"'"+ colsValues[i]+"'";
        }
        if (conditions != null) sql += " WHERE " + conditions + " = " + conValues + " ";

        int result = ExecuteNonQuery(sql);
        return result;
    }

    /// <summary>
    /// 制造一句查询语句，示例：select [results] from [tableName] where [columns] = [colValues]
    /// </summary>
    /// <param name="dbName">数据库名</param>
    /// <param name="tableName">表名</param>
    /// <param name="results">select什么字段</param>
    /// <param name="conditions">where什么字段</param>
    /// <param name="conValues">where字段的值</param>
    /// <returns></returns>
    public SqliteDataReader Query(string dbName, string tableName, string[] results, string[] conditions=null, string[] conValues=null)
    {
        if(connection==null){
            Connect(dbName);
        }

        string query = "SELECT " + results[0];

        for (int i = 1; i < results.Length; ++i)
        {

            query += ", " + results[i];
 
        }
        query += " FROM " + tableName;
        //有查询条件
        if (conditions != null)
        {
            query += " WHERE " + conditions[0] + "=" + "'" + conValues[0] + "' ";

            for (int i = 1; i < conditions.Length; ++i)
            {

                query += " AND " + conditions[i] + "=" + "'" + conValues[0] + "' ";

            }
        }

        SqliteDataReader reader=ExecuteQuery(query);
        return reader;

    }
    /// <summary>
    /// 获取查询到数据的条数
    /// </summary>
    /// <returns></returns>
    public int GetRowCount(string dbName,string sql)
    {
        int RowCount = -1;
        if (connection == null)
        {
            Connect(dbName);
        }
        try
        {
            command = connection.CreateCommand();
            command.CommandText = sql;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                RowCount = dataReader.GetInt32(0);
            }
            
        }
        catch (Exception e)
        {
            Debug.Log("Sql 语句错误！");
            Debug.Log(e.Message);
        }
        return RowCount;
    }

    /// <summary>
    /// 执行一句sql查询语句
    /// </summary>
    /// <param name="sqlQuery">sql语句</param>
    /// <returns>查询结果的SqliteDataReader对象</returns>
    private SqliteDataReader ExecuteQuery(string sql)
    {
        try
        {
            command = connection.CreateCommand();
            command.CommandText = sql;
            dataReader = command.ExecuteReader();

        }
        catch(Exception e){
            Debug.Log("Sql 语句错误！");
            Debug.Log(e.Message);
        }
        return dataReader;
    }

    /// <summary>
    /// 执行一句增删改操作语句
    /// </summary>
    /// <param name="sql"></param>
    /// <returns>多少条数据被执行了</returns>
    private int ExecuteNonQuery(string sql)
    {
        
        int operatedRows = 0;   //执行了多少行操作
        try
        {
            command = connection.CreateCommand();
            command.CommandText = sql;
            Monitor.Enter(obj);
            operatedRows = command.ExecuteNonQuery();
            Monitor.Exit(obj);
            
        }
        catch (Exception e)
        {
            Debug.Log("Sql 语句错误！");
            Debug.Log(e.Message);
        }
        return operatedRows;
    }
}
