using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    public enum E_Weapon
    {
        E_NONE = -1 , E_SWORD, E_KNIF_THROW
    }



    [SerializeField]
    private WeaponBase[] m_cWeapons;

    public void init()
    {
       
    }

    public void switchWeapon(E_Weapon eSwitchWeapon,ref WeaponBase switchWeapon)
    {
        if(switchWeapon != null)
        {
            switchWeapon.transform.SetParent(transform);
            switchWeapon.transform.localPosition = Vector3.zero;
            switchWeapon.gameObject.SetActive(false);
        }

        switchWeapon = m_cWeapons[(int)eSwitchWeapon];
        switchWeapon.gameObject.SetActive(true);
    }


}
