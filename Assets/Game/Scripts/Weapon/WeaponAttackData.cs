using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponAttackData
{
    [SerializeField]
    private WeaponDamageData[] m_cDamageData;



    public WeaponDamageData[] getWeaponDamageData()
    {
        return m_cDamageData;
    }


}