
public enum EventType
{
    /* 回调UI加载器事件 */
    UI_OpenPanel,
    UI_ClosePanel,
    UI_Loading,
    UI_Loaded,
    UI_ObjRegister,


    /* AB回调事件 */
    AB_LoadSingleton,
    AB_LoadAll,


    /* 获取场景中的Audio */
    Audio_SetAudioValue,
    Audio_CloseAudio,
    Audio_OpenAudio,


    /* Scene回调事件 */
    SceneChange,
    SceneChangeEvent_OnLoad,
    SceneChangeEvent_UnderLoad,


    /*获取当前唯一玩家*/
    Get_Current_Player,
    Set_Current_Player,

    /* 游戏对局事件 */
    Game_Start,
    Game_Pause,
    Game_Fail,
    Game_Successful,


    /* 获取配置信息 */
    Get_DefaultConfig
}
