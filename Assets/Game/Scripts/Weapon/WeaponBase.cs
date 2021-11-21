using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [SerializeField] private LayerMask m_maskTarget;

    private IEnumerator m_ieCoolTimeEvent;

    private bool m_bCoolTime;

    protected int m_nCurrentCombo;


    public bool isCoolTime
    {
        set
        {
            m_bCoolTime = value;
        }
        get
        {
            return m_bCoolTime;
        }
    }

    public LayerMask maskTarget
    {
        get
        {
            return m_maskTarget;
        }
    }


   

    [SerializeField]
    private WeaponData m_cWeaponData;

    public WeaponData cWeaponData
    {
        set
        {
            m_cWeaponData = value;
        }
        get
        {
            return m_cWeaponData;
        }
    }


    protected UnitBase m_unitBase;

    public UnitBase cGirpUnit
    {
        get
        {
            return m_unitBase;
        }
    }



    public virtual void init(UnitBase unitBase)
    {
        m_unitBase = unitBase;
    }


    public virtual void attackStart()
    {
        coolTimeer();
    }


    public virtual void attackStop()
    {

    }

    public virtual void updateWeapon(Vector2 v2Dir) { }

    public void coolTimeStart()
    {
        coolTimeer();
    }

    private void coolTimeer()
    {
        coolTimeReset();

       // m_ieCoolTimeEvent = coolTimeEvnet(m_cWeaponData.fCoolTime[nIndex]);
        StartCoroutine(m_ieCoolTimeEvent);
    }

    private IEnumerator coolTimeEvnet(float fCoolTime)
    {
        float fTime = .0f;

        isCoolTime = true;

        while (fTime <= fCoolTime)
        {
            fTime += Time.deltaTime;
            yield return null;
        }

        isCoolTime = false;
    }

    public void coolTimeReset()
    {
        if (m_ieCoolTimeEvent != null)
            StopCoroutine(m_ieCoolTimeEvent);

        isCoolTime = false;
    }

}
