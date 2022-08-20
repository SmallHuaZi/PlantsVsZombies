using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;
using System.IO;

public class CallLua : MonoBehaviour, ICallScripts
{
    private Lua m_CallLua = new Lua();

    [SerializeField] private string _M_LuaFilePath;

    [SerializeField] private string[] _M_AllLuaFile;

    private LuaFunction _M_Func;

    private void Start()
    {
        this._M_AllLuaFile = new string[10];
        this._M_LuaFilePath = Application.dataPath + "/Scripts/Lua";

        DirectoryInfo LuaDirectory = new DirectoryInfo(this._M_LuaFilePath);
        FileInfo[] AllLuaFile = LuaDirectory.GetFiles("*.lua");
        for (int index = 0; index < AllLuaFile.Length; index++)
            this._M_AllLuaFile[index] = AllLuaFile[index].Name;

        this.CallScripts("");
    }


    public void CallScripts(string _CallFileName)
    {
        Lua lua = new Lua();
        lua.DoFile(this._M_LuaFilePath + this._M_AllLuaFile[0]);

        this._M_Func = lua.GetFunction("output");
        this._M_Func.Call();
    }
}


