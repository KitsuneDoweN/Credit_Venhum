using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNomalSworld : WeaponBase
{
    [SerializeField] private int m_nNotAttackLayer;
    [SerializeField] private int m_nAttackLayer;


    private IEnumerator m_ieAttackCoroutine;




    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);
        gameObject.layer = m_nNotAttackLayer;
        m_nCurrentCombo = 0;
    }

    public override void attackStart()
    {
        base.attackStart();
        gameObject.layer = m_nAttackLayer;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        int nTargetMask = 1 << collision.gameObject.layer;

        if((nTargetMask & maskTarget) != 0)
        {
            collision.GetComponent<UnitBase>().hit(m_unitBase, cWeaponData.sWeaponData.sDamages);
        }
    }



    public override void attackStop()
    {
        gameObject.layer = m_nNotAttackLayer;
    }





}
