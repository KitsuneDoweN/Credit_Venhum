using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : WeaponBase
{
    [SerializeField] private GameObject m_goThrow;
    [SerializeField] private Transform m_trFirePoint;


    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);
    }

    public override void attackEventStart()
    {
        base.attackEventStart();

        GameObject goThrow = Instantiate(m_goThrow);
        goThrow.transform.position = m_trFirePoint.position;
        goThrow.transform.rotation = m_trFirePoint.rotation;
        Vector2 v2FireDir = m_trFirePoint.position - transform.position;
        v2FireDir = v2FireDir.normalized;

        goThrow.GetComponent<Throw>().init(this, v2FireDir, 20.0f);
        goThrow.GetComponent<Throw>().shoot();
    }


    public override void attack()
    {
        base.attack();

        cUnit.isMoveAble = true;
        coolTimeEvent();
    }

    public override void attackEnd()
    {
        base.attackEnd();
    }

}
