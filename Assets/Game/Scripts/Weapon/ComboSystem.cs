using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    private int m_nCurrentCombo;
    private int m_nMaxCombo;

    private int m_nOldCombo;

    private bool m_bComboAble;

    public void init(WeaponData weaponData)
    {
        reset();
        m_nMaxCombo = weaponData.nMaxCombo;
    }

    public void reset()
    {
        comboAbleEnd();
        m_nCurrentCombo = 0;
    }

    public void comboAbleStart()
    {
        m_nOldCombo = m_nCurrentCombo;
        m_bComboAble = true;
    }

    public void comboAbleEnd()
    {
        m_bComboAble = false;
    }

    public bool comboChack()
    {
        bool bResult = false;

        if (m_nOldCombo != m_nCurrentCombo)
            bResult = true;

        return bResult;
    }

    public void combo()
    {
        if (isComboAble)
        {
            m_nCurrentCombo++;
            
            if (m_nCurrentCombo >= m_nMaxCombo)
                m_nCurrentCombo = 0;
            //Debug.Log(m_nCurrentCombo + "  " + m_nMaxCombo);

            comboAbleEnd();
        }
            

    }


    public int nCurrentCombo
    {
        get
        {
            return m_nCurrentCombo;
        }
    }

    public int nMaxCombo
    {
        get
        {
            return m_nMaxCombo;
        }
    }

    public bool isComboAble
    {
        get
        {
            return m_bComboAble;
        }
    }

   public int nOldCombo
    {
        get
        {
            return m_nOldCombo;
        }
    }


}
