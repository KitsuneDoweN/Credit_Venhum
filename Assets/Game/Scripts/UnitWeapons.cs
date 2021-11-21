using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeapons : MonoBehaviour
{

    [SerializeField] private WeaponBase[] weapons;


    private WeaponBase m_cGripWeapon;
    private int m_nGripWeaponIndex;



    public void init(UnitBase unit)
    {
        
        foreach (WeaponBase weapon in weapons)
        {
            weapon.init(unit);
            weapon.gameObject.SetActive(false);
        }
    }


    public void switchWeapon(int nSwitchWeaponIndex, WeaponBase weapon)
    {
        nGripWeaponIndex = nSwitchWeaponIndex;


        m_cGripWeapon = weapons[nGripWeaponIndex];
 
    }

    
    public int nGripWeaponIndex
    {
        set
        {
            m_nGripWeaponIndex = value;
        }
        get
        {
            return m_nGripWeaponIndex;
        }
    }




}
