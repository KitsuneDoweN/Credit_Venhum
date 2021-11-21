using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "WeaponDatas/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [Serializable]
    public struct S_WeaponDamage
    {
        public enum E_DamageType
        {
            E_NOMAL, E_FAINT
        }



        public int nDamage;
        public E_DamageType eDamageType;

    }

    [Serializable]
    public struct S_WeaponData
    {

        public float [] fCollTimes;
        public S_WeaponDamage[] sDamages;

        public enum E_WeaponType
        {
            E_NONE = -1, E_SWORD, E_BOW,E_BIGSPIKES,E_RUSH,



            E_TOTAL
        }
        public E_WeaponType eWeaponType;
        public float fRange;
    }



    [SerializeField] private S_WeaponData m_sWeaponData;

    [SerializeField] private int m_maxCombo;



    public S_WeaponData sWeaponData
    {
        get
        {
            return m_sWeaponData;
        }
    }

    public S_WeaponDamage[] sDamages
    {
        get
        {
            return m_sWeaponData.sDamages;
        }
    }

    public S_WeaponData.E_WeaponType eWeaponType
    {
        get
        {
            return m_sWeaponData.eWeaponType;
        }
    }

    public float fRange
    {
        get
        {
            return m_sWeaponData.fRange;
        }
    }

    public float[] fCoolTime
    {
        get
        {
            return m_sWeaponData.fCollTimes;
        }
    }



}
