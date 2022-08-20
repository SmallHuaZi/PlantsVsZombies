using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : UIObject
{
    [SerializeField] private Button m_RetBtn;


    [SerializeField] private Slider m_Volume, m_Sound;


    public override void RefreshUI() { }


    protected override void Start()
    {
        base.Start();
        this.m_RetBtn.onClick.AddListener(() => EventManager.Broadcast<string>(EventType.UI_ClosePanel, this.name));

        this.m_Volume.onValueChanged.AddListener((float value) => EventManager.Broadcast<float>(EventType.Audio_SetAudioValue, value));

    }


}
