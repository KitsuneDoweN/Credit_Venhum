using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBase : MonoBehaviour
{

    [SerializeField] private LayerMask m_maskTarget;

    private IEnumerator m_ieCoolTimeEvent;

    private string m_strAttackTrigger;

    private bool m_bCoolTime;

    private float m_fCoolTimeTick;

    [SerializeField]
    private UnityEvent m_coolTimeEvent;

    [SerializeField]
    private SpriteRenderer m_srModel;

    public SpriteRenderer srModel
    {
        get
        {
            return m_srModel;
        }
    }


    public float fCoolTimeTick
    {

        get
        {
            return m_fCoolTimeTick;
        }
    }


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


    private UnitBase m_unitBase;



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

            //Debug.Log("isAttackRun: " + m_bAttackRun);
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

    public virtual void attackImfect()
    {

    }

    public void comboAbleStart()
    {
        m_cComboSystem.comboAbleStart();
    }

    public virtual void reset()
    {
        isAttackRun = false;

    }

    protected void coolTimeEvent()
    {

        m_ieCoolTimeEvent = coolTimeEvnetCoroutine();
        StartCoroutine(m_ieCoolTimeEvent);

    }

    private IEnumerator coolTimeEvnetCoroutine()
    {
        isCoolTime = true;
        m_fCoolTimeTick = 0.0f;

        m_coolTimeEvent.Invoke();

        while (fCoolTimeTick < m_cWeaponData.fCoolTime)
        {
            m_fCoolTimeTick += Time.deltaTime;
            yield return null;
        }

        isCoolTime = false;
        m_ieCoolTimeEvent = null;
    }

    protected virtual void attackAnimation()
    {

    }




    public void stopCoolTimeEvent()
    {
        if (m_ieCoolTimeEvent == null)
            return;

        StopCoroutine(m_ieCoolTimeEvent);
        isAttackRun = true;
    }

    public int nCurrentCombo
    {
        get
        {
            return m_cComboSystem.nCurrentCombo;
        }
    }


}
