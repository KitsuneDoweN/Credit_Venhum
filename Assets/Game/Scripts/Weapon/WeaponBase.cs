using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{

    [SerializeField] private LayerMask m_maskTarget;

    private IEnumerator m_ieCoolTimeEvent;

    private bool m_bCoolTime;


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

    public UnitBase cUnit
    {
        get
        {
            return m_unitBase;
        }
    }

    [SerializeField]
    protected ComboSystem m_cComboSystem;

    private bool m_bAttackRun;

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
        m_cComboSystem.init(m_cWeaponData);
        reset();
    }

    public virtual void attack()
    {
        if (isCoolTime)
            return;


        if (isAttackRun)
        {
            m_cComboSystem.combo();
            return;
        }
            
        cUnit.isMoveAble = false;

        cUnit.cAnimation.attack(m_cComboSystem.nCurrentCombo);
        isAttackRun = true;
    }

    public virtual void attackEventStart()
    {

    }


    public virtual void attackEventEnd()
    {

    }

    public virtual void attackEnd()
    {
        m_cComboSystem.comboAbleEnd();

        isAttackRun = false;


        Debug.Log(cUnit.v2OldMoveDir + "  " + cUnit.v2OldLookDir);

        cUnit.v2MoveDir = cUnit.v2OldMoveDir;

        if (cUnit.v2OldLookDir != Vector2.zero)
            cUnit.v2LookDir = cUnit.v2OldLookDir;

        Debug.Log(cUnit.v2OldMoveDir + "  " + cUnit.v2OldLookDir);



        if (!m_cComboSystem.comboChack())
        {
            cUnit.isMoveAble = true;
            coolTimeEvent();
            return;
        }





        attack();
    }

    public virtual void attackImfect()
    {

    }

    public void comboAbleStart()
    {
        m_cComboSystem.comboAbleStart();
    }

    public void reset()
    {
        isAttackRun = false;
        isCoolTime = false;

        m_cComboSystem.comboReset();
    }

    private void coolTimeEvent()
    {

        m_ieCoolTimeEvent = coolTimeEvnetCoroutine();
        StartCoroutine(m_ieCoolTimeEvent);
    }

    private IEnumerator coolTimeEvnetCoroutine()
    {
        isCoolTime = true; 

        yield return new WaitForSeconds(m_cWeaponData.fCoolTime);

        isCoolTime = false;
        m_ieCoolTimeEvent = null;
    }

    public void stopCoolTimeEvent()
    {
        if (m_ieCoolTimeEvent == null)
            return;

        StopCoroutine(m_ieCoolTimeEvent);
        isAttackRun = true;
    }

}
