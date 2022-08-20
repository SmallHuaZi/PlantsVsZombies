using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class GameManager : MonoBehaviour
{
    private player m_Current;

    private Dictionary<ConfigType, JsonData> m_DefaultSetting = new Dictionary<ConfigType, JsonData>();

    protected virtual void Awake()
    {
        /* 将配置文件加载到 */
        JsonData jsonData = JsonMapper.ToObject(File.ReadAllText(Application.streamingAssetsPath + "/config/InitailGame.json"));
        this.m_DefaultSetting.Add(ConfigType.Default_Player, jsonData["Player"]);
        this.m_DefaultSetting.Add(ConfigType.Default_Audio, jsonData["Audio"]);
        this.m_DefaultSetting.Add(ConfigType.Default_Scene, jsonData["Scene"]);


        /* 提供获取配置信息接口 */
        EventManager.AddListener<JsonData, ConfigType>(EventType.Get_DefaultConfig, (ConfigType type) => this.m_DefaultSetting[type]);

        /* 向外提供访问当前玩家的接口 */
        EventManager.AddListener<player>(EventType.Get_Current_Player, () => this.m_Current);

        /* 向外提供修改当前玩家信息的接口 */
        EventManager.AddListener<player>(EventType.Set_Current_Player, (player current) => this.m_Current = current);
    }
}
