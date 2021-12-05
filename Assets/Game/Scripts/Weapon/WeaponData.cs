using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "WeaponDatas/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public enum E_Type
    {
        E_NONE = -1 , E_ALMOST, E_TROW
    }


    [SerializeField]
    private float m_fStamina;

    [SerializeField]
    private WeaponAttackData[] m_cWeaponDamages;

    [SerializeField]
    private float m_fCoolTime;

    [SerializeField]
    private float m_fGripRange;

    [SerializeField]
    private E_Type m_eType;

    public E_Type eType
    {
        get
        {
            return m_eType;
        }
    }

    public float fGripRange
    {
        get
        {
            return m_fGripRange;
        }
    }

    public float fStamina
    {
        get
        {
            return m_fStamina;
        }
    }

    public int nMaxCombo
    {
        get
        {
            return m_cWeaponDamages.Length;
        }
    }

    public float fCoolTime
    {
        get
        {
            return m_fCoolTime;
        }
    }

    public WeaponAttackData getWeaponAttackData(int nIndex)
    {
        return m_cWeaponDamages[nIndex];
    }
}

