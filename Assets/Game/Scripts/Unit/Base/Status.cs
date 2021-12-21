using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class Status
{
    private int m_nHp;
    [SerializeField] private int m_nMaxHp;
    public int nHp
    {
        set
        {
            m_nHp = value;
        }

        get
        {
            return m_nHp;
        }

    }

    public int nMaxHp
    {
        get
        {
            return m_nMaxHp;
        }
    }


    private float m_fSpeed;
    [SerializeField] private float m_fMaxSpeed;
    public float fSpeed
    {
        set
        {
            m_fSpeed = Mathf.Clamp(value, 0.0f, m_fMaxSpeed);
        }

        get
        {
            return m_fSpeed;
        }

    }

    public float fMaxSpeed
    {
        get
        {
            return m_fMaxSpeed;
        }
    }


    [SerializeField] private float m_fMaxStamina;
    private float m_fStamina;
    [SerializeField] protected float m_fStaminaHealingTick;
    [SerializeField] protected float m_fStaminaHealingTickTime;


    [SerializeField] private float m_fDushPower;
    [SerializeField] private float m_fDushTime;
    [SerializeField] private float m_fDushStamina;

    [SerializeField] private float m_fGodTime;

    [SerializeField] private int m_nMaxStiffness;

    private int m_nCurrentStiffness;


    public float fDushPower
    {
        get
        {
            return m_fDushPower;
        }
            
    }

    public float fDushTime
    {
        get
        {
            return m_fDushTime;
        }
    }

    public float fDashStamina
    {
        get
        {
            return m_fDushStamina;
        }
    }

    public float fMaxStamina
    {
        get
        {
            return m_fMaxStamina;
        }
    }

    public float fStamina
    {
        set
        {
            m_fStamina = value;
        }
        get
        {
            return m_fStamina;
        }
    }

    public float fStatminaHealingTick
    {
        get
        {
            return m_fStaminaHealingTick;
        }
    }

    public float fStatminaHealingTickTime
    {
        get
        {
            return m_fStaminaHealingTickTime;
        }
    }

    public int nMaxStiffness
    {
        get
        {
            return m_nMaxStiffness;
        }
    }

    public int nCurrentStiffness
    {
        set
        {
            m_nCurrentStiffness = value;
        }
        get
        {
            return m_nCurrentStiffness;
        }
    }



    public void init()
    {
        nHp = nMaxHp;
        fSpeed = fMaxSpeed;
        fStamina = fMaxStamina;
        nCurrentStiffness = 0;
    }

   
    public float fGodTime
    {
        get
        {
            return m_fGodTime;
        }
    }





}
