using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBow : WeaponBase
{
    [SerializeField] private GameObject m_goThrow;
    [SerializeField] private Transform m_trFirePoint;


    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);
    }

    public override void attackStart()
    {
        base.attackStart();

        GameObject goThrow = Instantiate(m_goThrow);
        goThrow.transform.position = m_trFirePoint.position;
        goThrow.transform.rotation = m_trFirePoint.rotation;
        Vector2 v2FireDir = m_trFirePoint.position - transform.position;
        v2FireDir = v2FireDir.normalized;

        goThrow.GetComponent<Throw>().init(this, v2FireDir, 20.0f);
        goThrow.GetComponent<Throw>().shoot();
    }




}
