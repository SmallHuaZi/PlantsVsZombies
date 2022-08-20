using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LitJson;
using System;

public class AudioManager : MonoBehaviour, IClose, IOpen
{
    [SerializeField] private AudioSource m_Audio;

    [SerializeField] private List<AudioClip> m_Clips;

    private void Start()
    {
        /* 向外提供访问音频音量的接口 */
        EventManager.AddListener<float>(EventType.Audio_SetAudioValue, this.SetValue);

        /* 提供音频组件开关接口 */
        EventManager.AddListener(EventType.Audio_CloseAudio, this.Close);
        EventManager.AddListener(EventType.Audio_OpenAudio, this.Open);

        /*注册事件：游戏对局状态 */
        EventManager.AddListener(EventType.Game_Start, () => this.SetClip(""));
        EventManager.AddListener(EventType.Game_Pause, () => this.SetClip(""));
        EventManager.AddListener(EventType.Game_Fail, () => this.SetClip(""));
        EventManager.AddListener(EventType.Game_Successful, () => this.SetClip(""));

        /* 获取所有的Clip文件 */
        this.m_Clips = ABManager._Instance.LoadAllAssets<AudioClip>(ABType.ABPackage_Audio).ToList();

        /* 获取音频配置文件 */
        JsonData audioSet = EventManager.Broadcast<JsonData, ConfigType>(EventType.Get_DefaultConfig, ConfigType.Default_Audio);


        /* 解析音频配置文件 */
        this.m_Audio.clip = this.m_Clips.Find(new System.Predicate<AudioClip>((AudioClip clip) => clip.name.Equals((string)audioSet["Clip"])));
        this.m_Audio.volume = (float)(double)audioSet["Sound"];
        this.m_Audio.Play();
    }

    public void SetClip(string name)
    {
        foreach (var clip in this.m_Clips)
        {
            if (clip.name.Equals(name))
            {
                this.m_Audio.clip = clip;
                return;
            }
        }

#if CODE_DONTCOMPILE
        Debug.Log("没有这个音频文件");
#endif
    }


    public void SetValue(float value)
    {
        this.m_Audio.volume = value;
    }

    public void Close()
    {
        this.m_Audio.Stop();
    }

    public void Open()
    {
        this.m_Audio.Play();
    }

}
