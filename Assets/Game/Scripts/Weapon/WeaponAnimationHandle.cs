using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationHandle : MonoBehaviour
{
    [SerializeField]
    private WeaponBase m_cWeaponBase;

    public WeaponBase cWeaponBase
    {
        set
        {
            m_cWeaponBase = value;
        }
        get
        {
            return m_cWeaponBase;
        }
    }

    public  void attackEventStart()
    {
        m_cWeaponBase.attackEventStart();
    }

    public void attackEventEnd()
    {
        m_cWeaponBase.attackEventEnd();
    }

    public  void attackEnd()
    {
        m_cWeaponBase.attackEnd();
    }

    public  void attackImfect()
    {
        m_cWeaponBase.attackAction();
    }

    public void comboAbleStart()
    {
        m_cWeaponBase.comboAbleStart();
    }

    public void reset()
    {
        m_cWeaponBase.reset();
    }

    

}
