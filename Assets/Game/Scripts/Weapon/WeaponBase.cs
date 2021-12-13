using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{


    [SerializeField]
    private WeaponData m_cWeaponData;
    [SerializeField]
    private ComboSystem m_cComboSystem;
    [SerializeField]
    private CoolTime m_cCoolTime;


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

    public ComboSystem cComboSystem
    {
        get
        {
            return m_cComboSystem;
        }
    }

    public CoolTime cCoolTime
    {
        get
        {
            return m_cCoolTime;
        }
    }


    [SerializeField] private LayerMask m_maskTarget;




    private string m_strAttackTrigger;



    public LayerMask maskTarget
    {
        get
        {
            return m_maskTarget;
        }
    }





    private UnitBase m_unitBase;



    public UnitBase cUnit
    {
        get
        {
            return m_unitBase;
        }
    }


    private bool m_bAttackRun;

    protected string strAttackTrigger
    {
        set
        {
            m_strAttackTrigger = value;
        }
        get
        {
            return m_strAttackTrigger;
        }
    }

    public bool isAttackRun
    {
        set
        {
            m_bAttackRun = value;
            m_unitBase.isLookAble = !m_bAttackRun;
        }
        get
        {
            return m_bAttackRun;
        }
    }


    public virtual void init(UnitBase unitBase)
    {
        m_unitBase = unitBase;
        cComboSystem.init(cWeaponData);
        reset();
    }

    public virtual void attack()
    {

    }

    public virtual void attackEventStart()
    {

    }

    public virtual void attackEventEnd()
    {

    }

    public virtual void attackEnd()
    {
        isAttackRun = false;
        cUnit.isMoveAble = true;
        cUnit.isLookAble = true;
    }

    public virtual void attackAction()
    {

    }

    public virtual void reset()
    {
        isAttackRun = false;
    }

    protected virtual void attackAnimation()
    {

    }

    public virtual void comboAbleStart()
    {
        cComboSystem.comboAbleStart();
    }

}
