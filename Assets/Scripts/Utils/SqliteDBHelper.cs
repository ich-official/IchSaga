using System;
using System.Data;
using System.Data.Common;
using Mono.Data.Sqlite;
namespace SQLiteQueryBrowser
{
    /// <summary> 
    /// ˵��������һ�����System.Data.SQLite�����ݿⳣ�������װ��ͨ���ࡣ 
    /// </summary> 
    public class SQLiteDBHelper
    {
        private string connectionString = string.Empty;
        /// <summary> 
        /// ���캯�� 
        /// </summary> 
        /// <param name="dbPath">SQLite���ݿ��ļ�·��</param> 
        public SQLiteDBHelper(string dbPath)
        {
            this.connectionString = "Data Source=" + dbPath;
        }
        /// <summary> 
        /// ����SQLite���ݿ��ļ� 
        /// </summary> 
        /// <param name="dbPath">Ҫ������SQLite���ݿ��ļ�·��</param> 
        public static void CreateDB(string dbPath)
        {
            using (SqliteConnection connection = new SqliteConnection("Data Source=" + dbPath))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(connection))
                {
                    command.CommandText = "CREATE TABLE Demo(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE)";
                    command.ExecuteNonQuery();
                    command.CommandText = "DROP TABLE Demo";
                    command.ExecuteNonQuery();
                }
            }
        }
        /// <summary> 
        /// ��SQLite���ݿ�ִ����ɾ�Ĳ�����������Ӱ��������� 
        /// </summary> 
        /// <param name="sql">Ҫִ�е���ɾ�ĵ�SQL���</param> 
        /// <param name="parameters">ִ����ɾ���������Ҫ�Ĳ���������������������SQL����е�˳��Ϊ׼</param> 
        /// <returns></returns> 
        public int ExecuteNonQuery(string sql, SqliteParameter[] parameters)
        {
            int affectedRows = 0;
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    using (SqliteCommand command = new SqliteCommand(connection))
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }
                        affectedRows = command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            return affectedRows;
        }
        /// <summary> 
        /// ִ��һ����ѯ��䣬����һ��������SqliteDataReaderʵ�� 
        /// </summary> 
        /// <param name="sql">Ҫִ�еĲ�ѯ���</param> 
        /// <param name="parameters">ִ��SQL��ѯ�������Ҫ�Ĳ���������������������SQL����е�˳��Ϊ׼</param> 
        /// <returns></returns> 
        public SqliteDataReader ExecuteReader(string sql, SqliteParameter[] parameters)
        {
            SqliteConnection connection = new SqliteConnection(connectionString);
            SqliteCommand command = new SqliteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        /// <summary> 
        /// ִ��һ����ѯ��䣬����һ��������ѯ�����DataTable 
        /// </summary> 
        /// <param name="sql">Ҫִ�еĲ�ѯ���</param> 
        /// <param name="parameters">ִ��SQL��ѯ�������Ҫ�Ĳ���������������������SQL����е�˳��Ϊ׼</param> 
        /// <returns></returns> 
        public DataTable ExecuteDataTable(string sql, SqliteParameter[] parameters)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqliteDataAdapter adapter = new SqliteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        /// <summary> 
        /// ִ��һ����ѯ��䣬���ز�ѯ����ĵ�һ�е�һ�� 
        /// </summary> 
        /// <param name="sql">Ҫִ�еĲ�ѯ���</param> 
        /// <param name="parameters">ִ��SQL��ѯ�������Ҫ�Ĳ���������������������SQL����е�˳��Ϊ׼</param> 
        /// <returns></returns> 
        public Object ExecuteScalar(string sql, SqliteParameter[] parameters)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    SqliteDataAdapter adapter = new SqliteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }
        /// <summary> 
        /// ��ѯ���ݿ��е���������������Ϣ 
        /// </summary> 
        /// <returns></returns> 
        public DataTable GetSchema()
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                DataTable data = connection.GetSchema("TABLES");
                connection.Close();
                //foreach (DataColumn column in data.Columns) 
                //{ 
                //  Console.WriteLine(column.ColumnName); 
                //} 
                return data;
            }
        }
    }
}