using System;

public class SQLTypes : Attribute
{
    public string Name;
    public string Tag;
    public bool IsPrimaryKey;
    public bool IsSaveToDB;
    public SQLTypes(string name, string tag, bool IsKey, bool IsSave)
    {
        this.Name = name;
        this.Tag = tag;
        this.IsPrimaryKey = IsKey;
        this.IsSaveToDB = IsSave;
    }

}
