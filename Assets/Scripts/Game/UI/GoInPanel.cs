using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GoInPanel : UIObject
{
    [SerializeField]
    public Button m_ReadFine, m_NewGame, m_ExitGame, m_Setting;


    public override void RefreshUI()
    { }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.m_ReadFine.onClick.AddListener(this.OpenRPEvent);
        this.m_NewGame.onClick.AddListener(this.NewGameEvent);
        this.m_ExitGame.onClick.AddListener(this.ExitGameEvent);
        this.m_Setting.onClick.AddListener(this.OpenSetting);
    }

    public void ExitGameEvent()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Debug.Log("GameExit");
    }


    public void NewGameEvent()
    {
        SqliteControler sqlDB = new SqliteControler("player.db");
        int playcout = sqlDB.GetTableLengh<player>();

#if CODE_DONTCOMPILE
        Debug.Log($"当前是第{playcout}个存档!");
#endif

        if (playcout < 4)
        {
            sqlDB.Insert<player>(new player(playcout, "温暖的桃子"));
        }
        else
        {
            sqlDB.Update<player>(new player(--playcout, "温暖的桃子"));
        }
        sqlDB.Close();


        /* 切换场景事件 */
        EventManager.Broadcast<string>(EventType.SceneChange, "Main");
    }


    public void OpenRPEvent()
    {
        EventManager.Broadcast<string>(EventType.UI_OpenPanel, "ReadFile");
    }


    public void OpenSetting()
    {
        EventManager.Broadcast<string>(EventType.UI_OpenPanel, "Setting");

    }

}
