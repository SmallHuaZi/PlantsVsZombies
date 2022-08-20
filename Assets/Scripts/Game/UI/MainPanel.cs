using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class MainPanel : UIObject
{
    [SerializeField]
    private Button[] m_Btns;  /*{
        0:add_biscuits
        1:add_gold
        2:Icon
        3:Map
        4:Bag
        5:Day
        6:Store
        7:SignIn
        8:Support
        9:HandBook
    }*/

    [SerializeField]
    private Text m_PlayerName;

    [SerializeField]
    private Image m_PlayerIcon;

    protected override void Start()
    {
        base.Start();
        this.m_Btns[0].onClick.AddListener(() => { });
        this.m_Btns[1].onClick.AddListener(() => { });
        this.m_Btns[3].onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_OpenPanel, "Map"));
        this.m_Btns[4].onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_OpenPanel, "BackBag"));
        this.m_Btns[5].onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_OpenPanel, "EveryDay"));
        this.m_Btns[6].onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_OpenPanel, "Store"));
        this.m_Btns[7].onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_OpenPanel, "SignIn"));
        this.m_Btns[8].onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_OpenPanel, "Support"));
        this.m_Btns[9].onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_OpenPanel, "HandBook"));
    }

    public override void RefreshUI()
    {

    }
}
