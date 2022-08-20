using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop
{
    [SQLTypes("_Gear", "int", false, true)]
    public int _Gear { get; set; }


    [SQLTypes("_Name", "string", false, true)]
    public string _PropName { get; set; }


    [SQLTypes("_Count", "int", false, true)]
    public int _PropCount { get; set; }


    [SQLTypes("_Type", "string", false, true)]
    public string _Type { get; set; }


    [SQLTypes("_Had", "bool", false, true)]
    public bool _Had { get; set; }


    public Prop() { }


    public Prop(int gear, string name, int cout,
                PropType type, bool had = false)
    {
        this._Gear = gear;
        this._PropName = name;
        this._PropCount = cout;
        this._Type = type.ToString();
        this._Had = had;
    }
}
