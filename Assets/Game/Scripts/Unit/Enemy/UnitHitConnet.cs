using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHitConnet : MonoBehaviour
{
    [SerializeField]
    private UnitBase m_cConnetUnit;

    public void hit(UnitBase unit, WeaponAttackData cAttackData)
    {
        
        m_cConnetUnit.hit(unit, cAttackData);
    }
}
