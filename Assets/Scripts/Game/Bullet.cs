using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolObject
{
    /* 子弹预制体的速度 */
    [SerializeField] private float m_Speed;


    /* 子弹预制体的攻击力 */
    [SerializeField] private uint m_BaseDamage;


    private void OnTriggerEnter2D(Collider2D other)
    {
        this.Close();
        if (other.CompareTag("Animal"))
            other.GetComponent<ItemBase>().Bleed -= this.m_BaseDamage;
    }

    private void Update()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * this.m_Speed);
    }
}
