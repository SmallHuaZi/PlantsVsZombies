using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private uint m_Bleed;
    [SerializeField] private uint m_Attack;


    /* Action事件间隔 */
    [SerializeField] protected float m_ActionInterVal;

    public uint Bleed
    {
        get => this.m_Bleed;
    }

    public uint Attack
    {
        get => this.m_Attack;
    }

    public float ActionInterVal
    {
        get => this.m_ActionInterVal;
    }

}
