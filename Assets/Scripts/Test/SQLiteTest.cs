using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;

public class Leo_SQLiteTest : MonoBehaviour {

    SqliteConnection connection;
    SqliteCommand command;
    SqliteDataReader reader;

    private string dbName = "testDB";

	void Start () {
        OpenConnect(dbName);
        //CreateTable("ich", new string[] { "ID", "name", "price" }, new string[] { "int", "text", "float" });
        //CloseSqlConnection();
        //SqliteHelper.Instance.Connect(dbName);
        //int row=InsertInto("ich", new string[] { "6897werfs54e6" });
        reader=ReadTable("ich");
        if (reader != null)
        {
            Debug.Log(reader.HasRows);
        }
        else
        {
            Debug.Log("reader is null");
        }
        //Debug.Log(row);
       // bool isHaveData = reader.Read();
       // Debug.Log(isHaveData);
        //Debug.Log(reader.HasRows);
        //while (reader.Read())
        //{
        //    Debug.Log(reader.GetInt32(reader.GetOrdinal("ID")));
        //    //Debug.Log(reader.GetString(reader.GetOrdinal("name")) + reader.GetFloat(reader.GetOrdinal("price")));
        //}


    }
	
	void Update () {
	
	}
    void OnDestroy()
    {
        CloseSqlConnection();
    }
    //打开数据库
    public void OpenConnect(string DBName)
    {
        try
        {
            //数据库存放在 Asset/StreamingAssets
            //该文件夹需要提前创建好，否则会报Unable to open the database file错误
            string path = Application.dataPath + "/Plugins/Android/assets/" + DBName + ".db";
#if UNITY_ANDROID
            //string Path  = jar:file://" + Application.dataPath + "!/assets/" + "你的文件";
            //string Path=("URI=file:" + appDBPath);
#endif
            //新建数据库连接
            connection = new SqliteConnection("URI=file:" + path);
            //打开数据库
            connection.Open();
            Debug.Log("打开数据库");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public void CloseSqlConnection()
    {

        if (command != null)
        {

            command.Dispose();

        }

        command = null;

        if (reader != null)
        {

            reader.Dispose();

        }

        reader = null;

        if (connection != null)
        {

            connection.Close();

        }

        connection = null;

        Debug.Log("Disconnected from db.");

    }

    public SqliteDataReader ExecuteQuery(string sqlQuery)
    {
        try
        {
            command = connection.CreateCommand();

            command.CommandText = sqlQuery;

            reader = command.ExecuteReader();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return reader;

    }

    public int ExecuteOperation(string sql)
    {
        int operatedRows = 0;   //执行了多少行操作
        try
        {          
            command = connection.CreateCommand();
            command.CommandText = sql;
            operatedRows = command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        return operatedRows;
    }

    public SqliteDataReader CreateTable(string name, string[] col, string[] colType)
    {

        if (col.Length != colType.Length)
        {

            throw new SqliteException("columns.Length != colType.Length");

        }

        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];

        for (int i = 1; i < col.Length; ++i)
        {

            query += ", " + col[i] + " " + colType[i];

        }

        query += ")";

        return ExecuteQuery(query);

    }

    public SqliteDataReader ReadTable(string tableName)
    {

        //string query = "SELECT * FROM " + tableName;
        //string sql = "SELECT ID FROM ich WHERE name='ich1111'";
        string sql = "sdfa64s68d4";
        return ExecuteQuery(sql);

    }

    public int InsertInto(string tableName, string[] values)
    {

        string sql = "INSERT INTO " + tableName + " VALUES (" + values[0];

        for (int i = 1; i < values.Length; ++i)
        {

            sql += ", " + values[i];

        }

        sql += ")";

        return ExecuteOperation(sql);

    }

}
