using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
/* 抽象基类，所有游戏对象继承于此类 */
public abstract class PoolObject : MonoBehaviour, IOpen, IClose
{
    [SerializeField] protected ObjectPool m_Pool;
    public virtual void Open() => this.gameObject.SetActive(true);
    public virtual void Close() => this.gameObject.SetActive(false);
    protected virtual void OnDisable() => this.m_Pool.RegisterObject(this);

}
