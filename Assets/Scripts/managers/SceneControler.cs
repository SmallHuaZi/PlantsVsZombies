using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using LitJson;

public class SceneControler : SceneBase
{

    /* 下一个场景：名称 */
    [SerializeField] private string m_strNextScene;
    public string strNextScene
    {
        get => this.m_strNextScene;
        set
        {
            /* 边界条件 */
            if (value == null || value.Equals(SceneManager.GetActiveScene().name))
                return;

            bool IsBuild = false;
            foreach (var ele in this.m_AllBuildScene)
            {
                if (ele.Contains(value))
                {
                    IsBuild = true;
                    break;
                }
            }
            if (IsBuild)
            {
                this.m_strNextScene = value;
                this.OnLoad();
            }
        }
    }


    /* 当前场景:名称 */
    [SerializeField] private string m_strCurrentScene;


    /* 场景切换事件 */
    private Dictionary<string, UnityEvent> m_UnderLoad = new Dictionary<string, UnityEvent>();
    private Dictionary<string, UnityEvent> m_OnLoad = new Dictionary<string, UnityEvent>();


    /* 在加载中 */
    private bool m_IsLoading;


    /*所有已经build的场景*/
    [SerializeField]
    private List<string> m_AllBuildScene = new List<string>();


    /*  */
    private void Awake()
    {
        /* 注册场景切换事件 */
        EventManager.AddListener<string>(EventType.SceneChange, (string sceneName) => this.strNextScene = sceneName);

        /* 注册切换场景的回调事件 */
        EventManager.AddListener<string, UnityAction>(EventType.SceneChangeEvent_OnLoad, this.AddListener_OnLoad);
        EventManager.AddListener<string, UnityAction>(EventType.SceneChangeEvent_UnderLoad, this.AddListener_UnderLoad);
    }


    private void Start()
    {

        /* 获取配置文件资源 */
        JsonData jsondata = EventManager.Broadcast<JsonData, ConfigType>(EventType.Get_DefaultConfig, ConfigType.Default_Scene);
        this.strNextScene = this.m_strCurrentScene = (string)jsondata["Default"];
        foreach (JsonData ele in jsondata["AllScene"])
            this.m_AllBuildScene.Add((string)ele);
    }


    /* 场景切换回调事件，场景切换前 */
    public void AddListener_OnLoad(string sceneName, UnityAction callBack)
     => this.AddListener(true, sceneName, callBack);


    public void AddListener_UnderLoad(string sceneName, UnityAction callBack)
     => this.AddListener(false, sceneName, callBack);


    private void AddListener(bool On, string sceneName, UnityAction callBack)
    {
        int Index = 0;
        for (; Index < this.m_AllBuildScene.Count; Index++)
            if (this.m_AllBuildScene[Index].Contains(sceneName))
                break;
        if (Index < this.m_AllBuildScene.Count)
        {
            if (On)
            {
                if (!this.m_OnLoad.ContainsKey(sceneName))
                    this.m_OnLoad.Add(sceneName, new UnityEvent());
                this.m_OnLoad[sceneName].AddListener(callBack);
            }
            else
            {
                if (!this.m_UnderLoad.ContainsKey(sceneName))
                    this.m_UnderLoad.Add(sceneName, new UnityEvent());
                this.m_UnderLoad[sceneName].AddListener(callBack);
            }

        }
        else if (Index.Equals(this.m_AllBuildScene.Count))
            Debug.LogError("不存在此场景!");
    }


    /**/
    public override async void OnLoad()
    {
        do
        {
            if (this.m_IsLoading)
            {
                Debug.Log("正在加载场景，不允许再加载");
                break;
            }
            /* lock */
            this.m_IsLoading = true;
            /* lock */

            AsyncOperation scene = SceneManager.LoadSceneAsync(this.m_strNextScene, LoadSceneMode.Single);
            scene.allowSceneActivation = false;

            /* 打开加载面板 */
            EventManager.Broadcast<string>(EventType.UI_OpenPanel, "Loading");

            /* 注册加载完成回调事件 */
            if (!EventManager.EventIsSign(EventType.UI_Loaded))
                EventManager.AddListener(EventType.UI_Loaded, Loaded);

            while (!scene.isDone)
            {
                // EventBase.Broadcast<float>(EventType.UI_Loading, scene.progress);
                if (scene.progress >= 0.8f)
                    break;
                await Task.Yield();
            }

            if (this.m_OnLoad.ContainsKey(this.m_strCurrentScene))
                this.m_OnLoad[this.m_strCurrentScene].Invoke();

            /* 这里的本地方法是因为解耦合，需要通过加载面板加载完毕过后再触发回调，打开面板 */
            async void Loaded()
            {
                /* 一定要使用bool变量进行事件控制，防止非加载场景事件触发进度条，导致空引用 */
                if (this.m_IsLoading)
                {
                    scene.allowSceneActivation = true;

                    /* 异步等待加载完成 */
                    while (!scene.isDone)
                        await Task.Yield();

                    this.m_strCurrentScene = this.strNextScene;


                    if (this.m_UnderLoad.ContainsKey(this.m_strCurrentScene))
                        this.m_UnderLoad[this.m_strCurrentScene].Invoke();
                    /* unlock */
                    this.m_IsLoading = false;
                    /* unlock */
                    this.strNextScene = default;
                }
            }
        } while (false);
    }


    /**/
    public override void OnUnLoad()
    {

    }


    /**/
    public override void OnUpdate()
    {

    }

}
