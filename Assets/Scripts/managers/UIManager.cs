#define VERSION_1_0_1

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{

    /* 根结点 */
    [SerializeField] private Transform m_Root;



    /* 当前UI面板 */
    [SerializeField] private UIObject m_Current;



    /* 存储场景已经打开的UI组件 */
    private Stack<UIObject> m_OpenedUIObject_Level1 = new Stack<UIObject>(),   /* 一级UI组件  */
                            m_OpenedUIObject_Level2 = new Stack<UIObject>(),   /* 二级UI组件  */
                            m_OpenedUIObject_Level3 = new Stack<UIObject>();   /* 三级UI组件  */



    /* 保存每次已经加载的UI组件 */
    private Dictionary<string, UIObject> m_Mapping = new Dictionary<string, UIObject>();



    private void Awake()
    {
        /*** 事件注册，向外提供方法  ***/
        EventManager.AddListener<string>(EventType.UI_OpenPanel, this.OpenUI);
        EventManager.AddListener<string>(EventType.UI_ClosePanel, this.CloseUI);
        EventManager.AddListener<UIObject>(EventType.UI_ObjRegister, this.RefisterUIObject);
        /*** 避免单例  ***/
    }


    private void Start()
    {
        this.OpenUI("Log_panel");
    }



    public virtual void OpenUI(string Name)
    {
#if VERSION_1_0_1
        /* 首先判断是否已经加载 */
        if (!this.m_Mapping.ContainsKey(Name))
        {
            /* 从AB包加载面板 */
            GameObject obj = ABManager._Instance.LoadAsset<GameObject>(ABType.ABPackage_UI, Name);
            obj = Tool.ObjectInitialize(obj, this.m_Root);
        }
        else
        {
            /* 已经加载，根据UI等级实施对应的机制 */
            UIObject obj = this.m_Mapping[Name];
            switch (obj.Level)
            {
                /* 全屏UI组件 */
                /* 高等级的UI切换会带动低等级的UI切换 */
                case UILevel.UI_Level_1:
                    {
                        /* 等级为1级的UI可以先使栈顶UI关闭再激活*/
                        /* 调用Close方法有原因是为了在进行完交互后再次激活栈顶元素刷新 */
                        if (this.m_OpenedUIObject_Level1.Count > 1)
                            this.m_OpenedUIObject_Level1.Peek().Close();
                        this.m_OpenedUIObject_Level1.Push(obj);
                        obj.Open();
                    }
                    goto case UILevel.UI_Level_2;

                /* 以一级UI为存在基准 */
                case UILevel.UI_Level_2:
                    {
                        if (this.m_OpenedUIObject_Level2.Count > 0)
                            this.m_OpenedUIObject_Level2.Pop().Close();
                        if (obj.Level.Equals(UILevel.UI_Level_2))
                        {
                            this.m_OpenedUIObject_Level2.Push(obj);
                            obj.Open();
                        }
                    }
                    goto case UILevel.UI_Level_3;

                /* 以二级UI为存在基础 */
                case UILevel.UI_Level_3:
                    {
                        if (this.m_OpenedUIObject_Level3.Count > 0)
                            this.m_OpenedUIObject_Level3.Pop().Close();
                        if (obj.Level.Equals(UILevel.UI_Level_3))
                        {
                            this.m_OpenedUIObject_Level3.Push(obj);
                            obj.Open();
                        }
                    }
                    break;
            }

        }
#endif

#if VERSION_1_0_0
        /* 场景中至少存在一块背景UI面板 */
        if (this.m_OpenedUIObject_Level1.Count > 1)
        {
            this.m_Current.Close();
            this.m_OpenedUIObject_Level1.Pop();
            /* 最顶上的UI组件为当前组件 */
            this.m_Current = this.m_OpenedUIObject_Level1.Peek();
        }

        /* 检测是否实例化 */
        if (this.m_Mapping.ContainsKey(Name))
        {
            this.m_Mapping[Name].Open();
            this.m_Current = this.m_Mapping[Name];
            /* 压入栈:标识是最前面的面板 */
            this.m_OpenedUIObject_Level1.Push(this.m_Current);
        }
        else
        {
            GameObject obj = ABManager._Instance.LoadAsset<GameObject>(ABType.ABPackage_UI, Name);
            obj = Tool.ObjectInitialize(obj, this.m_Root);
        }
#endif
    }



    /* 对外提供UI注册事件 */
    /* UIObject只会在激活时注册一次， */
    public void RefisterUIObject(UIObject obj)
    {
#if VERSION_1_0_1
        if (this.m_Mapping.ContainsValue(obj))
            Debug.Log("面板已经注册");
        else
        {
            this.m_Mapping.Add(obj.name, obj);
            this.OpenUI(obj.name);
        }

#endif

#if VERSION_1_0_0
        /* 检测面板是否存在 */
        if (!this.m_Mapping.ContainsValue(obj))
        {
            this.m_Mapping.Add(obj.name, obj);
            this.m_Current = obj;
            this.m_OpenedUIObject_Level1.Push(obj);
        }
        else
            Debug.LogError("this obj wased!");
#endif
    }



    /* 保留方法 */
    public virtual void CloseUI(string Name)
    {
#if VERSION_1_0_1
        do
        {
            if (!this.m_Mapping.ContainsKey(Name))
            {
                Debug.Log($"UI{Name}未曾打开");
                break;
            }

            UIObject obj = this.m_Mapping[Name];
            switch (obj.Level)
            {
                case UILevel.UI_Level_1:
                    {
                        if (this.m_OpenedUIObject_Level1.Count > 1)
                        {
                            this.m_OpenedUIObject_Level1.Pop().Close();
                        }
                        this.m_OpenedUIObject_Level1.Peek().Open();
                    }
                    goto case UILevel.UI_Level_2;

                case UILevel.UI_Level_2:
                    while (this.m_OpenedUIObject_Level2.Count > 0)
                    {
                        this.m_OpenedUIObject_Level2.Pop().Close();
                    }
                    goto case UILevel.UI_Level_3;

                case UILevel.UI_Level_3:
                    while (this.m_OpenedUIObject_Level2.Count > 0)
                    {
                        this.m_OpenedUIObject_Level2.Pop().Close();
                    }
                    break;
            }
        } while (false);

#endif

#if VERSION_1_0_0
        do
        {
            /* 判断其是否存在且是否为背景UI */
            if (!this.m_Mapping.ContainsKey(Name) && this.m_OpenedUIObject_Level1.Count.Equals(1))
            {
                Debug.Log("面板未曾打开");
                break;
            }

            this.m_Mapping[Name].Close();
            this.m_OpenedUIObject_Level1.Pop();
            this.m_Current = this.m_OpenedUIObject_Level1.Peek();
        } while (false);
#endif
    }
}
