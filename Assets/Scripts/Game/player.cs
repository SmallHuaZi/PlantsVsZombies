using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player
{

    /* 游戏层级 */
    [SQLTypes("_GEAR", "int", true, true)]
    public int _Gear { get; set; }


    /* 玩家头像索引 */
    [SQLTypes("_ICON_INDEX", "int", false, true)]
    public int _ICONINDEX { get; set; }


    /* 玩家昵称 */
    [SQLTypes("_NAME", "string", false, true)]
    public string _NAME { get; set; }


    /* 玩家经济 */
    [SQLTypes("_ECONOMICS", "int", false, true)]
    public int _ECONOMICS { get; set; }


    /* 玩家所持饼干数  */
    [SQLTypes("_BISCUITS", "int", false, true)]
    public int _BISCUITS { get; set; }


    /* 玩家关卡完成数 */
    [SQLTypes("_COMPLETENUM", "int", false, true)]
    public int _COMPLETENUM { get; set; }


    [SQLTypes("HadProps", "Dictionary", false, false)]
    public Dictionary<PropType, List<Prop>> _HADPROPS { get; set; }


    public player() { }


    public player(int Gear, string Name)
    {
        this._Gear = Gear;
        this._NAME = Name;
        this._ICONINDEX = 0;
        this._BISCUITS = 100;
        this._ECONOMICS = 100;
        this._COMPLETENUM = 0;
    }
}
