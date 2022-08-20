using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(Rigidbody2D), typeof(PolygonCollider2D))]
public abstract class ItemBase : PoolObject
{

    /* 游戏对象数据 */
    [SerializeField] protected ItemData m_ItemData;


    /* 物体动画机 */
    [SerializeField] protected Animator m_AniCont;


    /* 触发Action事件 */
    [SerializeField] protected bool m_CanAction;


    /* 生产者类实现 */
    [SerializeField] protected Producer m_Prodecer;


    /* Action事件间隔 */
    [SerializeField] protected float m_ActionInterVal = default;


    /* runtime数据_血量 */
    [SerializeField] private uint m_Bleed = default;



    /* runtime数据_攻击力 */
    [SerializeField] private uint m_BaseDamage = default;



    /* 生产者类 */
    [Serializable]
    protected class Producer
    {
        public GameObject m_Product;
        public GameObject m_CreatePoint;
        public float m_AttackInterval;
    }


    /* 向外部提供修改血量的接口 */
    public virtual uint Bleed
    {
        get => this.m_Bleed;
        set
        {
            if (value.Equals(0))
                this.Close();
            this.m_Bleed = value;
        }
    }


    protected virtual void Start()
    {
        this.m_AniCont = this.transform.GetComponent<Animator>();

    }


    protected virtual void OnEnable()
    {
        /* 在每次渲染的时候重新设置数据 */

        this.m_Bleed = this.m_ItemData.Bleed;
        this.m_BaseDamage = this.m_ItemData.Attack;
        this.m_ActionInterVal = this.m_ItemData.ActionInterVal;

        /*******************************/
    }


    protected abstract void ActionEvent();

    protected abstract void DeadEvent();
}
