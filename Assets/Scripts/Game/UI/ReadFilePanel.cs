using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadFilePanel : UIObject
{
    [SerializeField]
    private Button m_Gear_0,
                    m_Gear_1,
                    m_Gear_2,
                    m_Gear_3,
                    m_RetBtn;

    public override void RefreshUI() { }



    protected override void Start()
    {
        base.Start();
        this.m_Gear_0.onClick.AddListener(() => { this.ReadFile(0); });
        this.m_Gear_1.onClick.AddListener(() => { this.ReadFile(1); });
        this.m_Gear_2.onClick.AddListener(() => { this.ReadFile(2); });
        this.m_Gear_3.onClick.AddListener(() => { this.ReadFile(3); });
        this.m_RetBtn.onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_ClosePanel, this.name));
    }



    private void ReadFile(uint gear)
    {
        using (SqliteControler sqlite = new SqliteControler("player.db"))
        {
            player oldPlayer = sqlite.GetObject<player>(gear);
            if (oldPlayer != null)
            {
#if CODE_DONTCOMPILE
                Debug.Log(oldPlayer._NAME + "进入了游戏");
#endif
                EventManager.Broadcast<player>(EventType.Set_Current_Player, oldPlayer);
            }
            else
                Debug.Log("不存在此玩家信息");
        }

        /* 切换场景事件 */
        EventManager.Broadcast<string>(EventType.SceneChange, "Main");
    }
}
