using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;



/* 需要如何触发Action事件才是应该关心的事件 */

public class F_peach : ItemBase
{

    protected override void Start()
    {
        base.Start();
    }


    /* 桃子的攻击事件 */
    protected override async void ActionEvent()
    {
        PoolObject bullet = this.m_Pool.GetPoolObject("peach_bullet");
        if (bullet == null)
            bullet = Instantiate<GameObject>(this.m_Prodecer.m_Product).
                    GetComponent<Bullet>();
        bullet.transform.position = this.m_Prodecer.
                                    m_CreatePoint.
                                    transform.
                                    position;


        this.m_AniCont.SetBool("attack", false);
        await Task.Delay(Convert.ToInt32(this.m_ActionInterVal * 1000));
        this.m_AniCont.SetBool("attack", this.m_CanAction);
    }


    protected override void DeadEvent()
    {

    }
}
