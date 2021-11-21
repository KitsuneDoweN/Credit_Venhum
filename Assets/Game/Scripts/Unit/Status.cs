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
            m_nHp = Mathf.Clamp(value, 0, m_nMaxHp);
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


    [SerializeField] private float m_fDushPower;
    [SerializeField] private float m_fDushTime;
    [SerializeField] private float m_fDushStamina;

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

    public float fDushStamina
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
            return m_fDushStamina;
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


    public void init()
    {
        m_nHp = m_nMaxHp;
        m_fSpeed = m_fMaxSpeed;
        m_fStamina = m_fMaxStamina;
    }

   






}
