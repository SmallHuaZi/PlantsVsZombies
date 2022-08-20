using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIObject : MonoBehaviour, IClose, IOpen
{

    /* 设置UI等级,同一级别才会覆盖同一级别 */
    [SerializeField] private UILevel m_Level;

    public UILevel Level { get => this.m_Level; }


    /* 向UIManager注册自己 */
    protected virtual void Start() => EventManager.Broadcast<UIObject>(EventType.UI_ObjRegister, this);


    /* 在渲染的时候，自动刷新面板 */
    protected virtual void OnEnable() => this.RefreshUI();


    public abstract void RefreshUI();


    /* UI关闭事件 */
    public virtual void Close()
    {
        this.gameObject.SetActive(false);


#if CODE_DONTCOMPILE
        Debug.Log($"面板{this.name}已经关闭");
#endif

    }


    /* UI激活事件 */
    public virtual void Open()
    {
        this.gameObject.SetActive(true);

#if CODE_DONTCOMPILE
        Debug.Log($"面板{this.name}已经打开");
#endif

    }
}
