using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeaponDamageData 
{
    public enum DamageType
    {
        E_NONE, E_NOMAL, E_STIFFEN
    }
    
    
    [SerializeField]
    private DamageType m_eDamageType;
    [SerializeField]
    private int m_nDamge;

    public DamageType eDamageType
    {
        get
        {
            return m_eDamageType;
        }
    }

    public int nDamge
    {
        get
        {
            return m_nDamge;
        }
    }
}
