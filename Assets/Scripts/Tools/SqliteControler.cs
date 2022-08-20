#define VERSION_1_0_0

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Threading;


public class SqliteControler : IDisposable
{
    private SqliteCommand m_CMD;

    private SqliteConnection m_SqlConnct;


    private string path
    {
        get
        {
            string path = default;
#if UNITY_EDITOR
            path = string.Concat("data source=" + Application.streamingAssetsPath + "/");
#elif UNITY_ANDROID
            path = string.Concat("URI=file:" + Application.persistentDataPath + "/");
#elif UNITY_IOS
            path =  string.Concat("data source=" + Application.persistentDataPath + "/");
#endif
            return path;
        }
    }

    public SqliteControler(string DBName)
    {
        string CompletePath = default;
#if UNITY_EDITOR
        CompletePath = Application.streamingAssetsPath + "/" + DBName;
#elif UNITY_ANDROID
        CompletePath = Application.persistentDataPath + "/" + DBName; 
#elif UNITY_IOS
        CompletePath = Application.persistentDataPath + "/" + DBName; 
#endif
        if (!File.Exists(CompletePath))
        {
            File.Create(CompletePath);
            Debug.Log("已为您重新创建文件");
        }
        CompletePath = path + DBName;
        try
        {
            this.m_SqlConnct = new SqliteConnection(CompletePath);
            this.m_SqlConnct.Open();
            this.m_CMD = this.m_SqlConnct.CreateCommand();
#if CODE_DONTCOMPILE
            if (this.m_CMD == null)
                Debug.Log("NULL");
#endif
        }
        catch (Exception e)
        {
            Debug.LogError("SqlError" + e.Message);
        }
    }


    /* 创建表 */
    public void CreateTable<T>()
    {
        Type type = typeof(T);
        string typeName = type.Name;
        StringBuilder Table = new StringBuilder($"create table {typeName} (");
        PropertyInfo[] listProperties = type.GetProperties();
        foreach (var Element in listProperties)
        {
            var Attrib = Element.GetCustomAttribute<SQLTypes>();
            if (Attrib.IsSaveToDB)
            {
                Table.Append($"{Attrib.Name} {Attrib.Tag}");
                if (Element.GetCustomAttribute<SQLTypes>().IsPrimaryKey)
                    Table.Append(" primary key");
                Table.Append(",");
            }
        }
        Table.Remove(Table.Length - 1, 1);
        Table.Append(")");

#if CODE_DONTCOMPILE
        Debug.Log(Table.ToString());
#endif

        this.m_CMD.CommandText = Table.ToString();
        this.m_CMD.ExecuteNonQuery();
    }


    /* 获取对象 */
    public T GetObject<T>(uint Gear) where T : class
    {
        Type type = typeof(T);
        var sql = $"SELECT * FROM {type.Name} WHERE _GEAR = {Gear}";
        this.m_CMD.CommandText = sql;
        using (var dr = this.m_CMD.ExecuteReader())
        {
            if (dr != null && dr.HasRows)
            {
                return Read<T>(dr);
            }
        }
        return null;
    }


    /* 将获取的字段实例化成对象 */
    private T Read<T>(SqliteDataReader dr) where T : class
    {
        Type type = typeof(T);
        T data = Activator.CreateInstance<T>();
        List<string> fileNames = new List<string>();

        var properties = type.GetProperties();

        /* 获取所有字段名 */
        for (int Pos = 0; Pos < dr.FieldCount; Pos++)
        {
            fileNames.Add(dr.GetName(Pos));
        }

        /* 遍历所有属性 */
        foreach (var Data in properties)
        {
            if (!Data.CanWrite) continue;

            //获取属性的上的特性
            var fieldName = Data.GetCustomAttribute<SQLTypes>().Name;

            /* 将特性上的字段与数据库字段作比较 */
            if (fileNames.Contains(fieldName))
            {
#if VERSION_1_0_0
                switch (Data.GetCustomAttribute<SQLTypes>().Tag)
                {
                    case "string":
                        Data.SetValue(data, dr[fieldName]);
                        break;
                    case "bool":
                        Data.SetValue(data, Convert.ToBoolean(dr[fieldName]));
#if CODE_DONTCOMPILE
                        Debug.Log(Convert.ToBoolean(dr[fieldName]));
#endif
                        break;
                    case "int":
                        Data.SetValue(data, Convert.ToInt32(dr[fieldName]));
                        break;
                }
#else
                if (Data.GetCustomAttribute<SQLTypes>().Tag == "string")
                {
                    Data.SetValue(data, dr[fieldName]);
                }
                else
                {
                    int Buff = (int)dr[fieldName];
                    Data.SetValue(data, Buff);
                }
#endif
            }
        }
        return data;
    }


    /* 插入数据 */
    public void Insert<T>(T tableType) where T : class
    {
        if (tableType == default(T))
            Debug.Log("传入空参数");
        Type type = typeof(T);
        StringBuilder sqlCommand = new StringBuilder($"Insert into {type.Name} values (");

        PropertyInfo[] AllPropertys = type.GetProperties();

        foreach (var Property in AllPropertys)
        {
#if VERSION_1_0_0
            switch (Property.GetCustomAttribute<SQLTypes>().Tag)
            {
                case "string":
                    sqlCommand.Append($"'{Property.GetValue(tableType)}'" + ",");
                    break;
                case "int":
                    sqlCommand.Append(Property.GetValue(tableType) + ",");
                    break;
            }
#else
            if (Property.GetCustomAttribute<SQLTypes>().Tag.Equals("string"))
                sqlCommand.Append($"'{Property.GetValue(tableType)}'" + ",");
            else
             sqlCommand.Append(Property.GetValue(tableType) + ",");
#endif
        }

        sqlCommand.Replace(',', ')', sqlCommand.Length - 1, 1);

#if CODE_DONTCOMPILE
        //打印sql语句
        Debug.Log(sqlCommand.ToString());
#endif

        /* 记录sql语句 */
        Tool.Log(sqlCommand.ToString());

        this.m_CMD.CommandText = sqlCommand.ToString();
        this.m_CMD.ExecuteNonQuery();
    }


    /* 批量处理数据 */
    public void Insert<T>(List<T> list) where T : class
    {
        if (list == default(List<T>))
            Debug.Log("传入空参数");
        Type type = typeof(T);
        StringBuilder sqlCommand = new StringBuilder($"Insert into {type.Name} values (");
        PropertyInfo[] AllPropertys = type.GetProperties();

        foreach (var ele in list)
        {
            foreach (var Property in AllPropertys)
            {
                if (Property.GetCustomAttribute<SQLTypes>().Tag.Equals("string"))
                    sqlCommand.Append($"'{Property.GetValue(ele)}'" + ",");
                else
                    sqlCommand.Append(Property.GetValue(ele) + ",");
            }
            sqlCommand.Remove(sqlCommand.Length - 1, 1);
            sqlCommand.Append("),(");
        }
        sqlCommand.Remove(sqlCommand.Length - 2, 2);

#if CODE_DONTCOMPILE
        Debug.Log(sqlCommand.ToString());
#endif
        /* 记录sql语句 */
        Tool.Log(sqlCommand.ToString());

        this.m_CMD.CommandText = sqlCommand.ToString();
        this.m_CMD.ExecuteNonQuery();
    }



    /* 更新数据 */
    public void Update<T>(T value) where T : class
    {
        if (value == default(T))
            Debug.LogError("传入参数为空");

        string FieldName = default;
        Type type = value.GetType();
        StringBuilder sql = new StringBuilder();
        sql.Append($"UPDATE {type.Name} SET ");
        PropertyInfo[] list = type.GetProperties();
        foreach (var Element in list)
        {
            FieldName = Element.GetCustomAttribute<SQLTypes>().Name;

#if VERSION_1_0_0
            switch (Element.GetCustomAttribute<SQLTypes>().Tag)
            {
                case "string":
                    sql.Append($"{FieldName} = '{Element.GetValue(value)}'" + ",");
                    break;
                case "int":
                    sql.Append($"{FieldName} = {Element.GetValue(value)}" + ",");
                    break;


            }
#else
            if (Element.GetCustomAttribute<SQLTypes>().Tag.Equals("string"))
                sql.Append($"{Element.Name} = '{Element.GetValue(value)}'" + ",");

            else
                sql.Append(Element.GetValue(value) + ",");
#endif
        }

        PropertyInfo gear = type.GetProperty("_Gear");

        sql.Replace(",", $" where _GEAR = {gear.GetValue(value)}", sql.Length - 1, 1);

        Tool.Log(sql.ToString());

        this.m_CMD.CommandText = sql.ToString();
        this.m_CMD.ExecuteNonQuery();
    }


    /* 批量更改数据 */
    public void Update<T>(List<T> list) where T : class
    {

    }



    /* 查看表是否有数据 */
    public bool EstimateIsLiving<T>(T current)
    {
        Type type = typeof(T);
        PropertyInfo gear = type.GetProperty("_Gear");
        this.m_CMD.CommandText = $"select * from {type.Name} where _GEAR = {gear.GetValue(current)} limit 1";
        SqliteDataReader reader = this.m_CMD.ExecuteReader();
        if (reader.FieldCount.Equals(0))
            return false;
        return true;
    }


    /* 获取表长 */
    public int GetTableLengh<T>()
    {
        var type = typeof(T);
        var sql = $"SELECT count(*) FROM {type.Name}";
        this.m_CMD.CommandText = sql;
        var dr = this.m_CMD.ExecuteScalar();
        return Convert.ToInt32(dr);
    }

    /* 关闭数据库连接 */
    public void Close()
    {
        this.m_SqlConnct.Close();
    }

    public void Dispose()
    {
        if (this.m_CMD != null)
            this.m_CMD.Dispose();
        if (this.m_SqlConnct != null)
            this.m_SqlConnct.Close();
    }
}
// create table player (_NAME string,_ECONOMICS int,_BISCUITS int,_COMPLETENUM int,_ICON_INDEX int)