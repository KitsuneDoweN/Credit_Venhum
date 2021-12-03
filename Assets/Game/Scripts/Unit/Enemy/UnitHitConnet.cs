using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHitConnet : MonoBehaviour
{

    private UnitBase m_cConnetUnit;

    private bool m_bHitAble;


    public bool isHitAble
    {
        set
        {
            m_bHitAble = value;
        }
        get
        {
            return m_bHitAble;
        }
    }

    public void init(UnitBase unit)
    {
        m_cConnetUnit = unit;
        isHitAble = true;
    }


    public void hit(UnitBase unit, WeaponAttackData cAttackData)
    {
        if (!isHitAble)
            return;


        m_cConnetUnit.hit(unit, cAttackData);
    }
}
