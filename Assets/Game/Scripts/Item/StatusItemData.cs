using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ItemDatas/StatusItemData", order = 1)]
public class StatusItemData : ScriptableObject
{
    [SerializeField]
    private float m_addStatmina;
    [SerializeField]
    private float m_addSpeed;
    [SerializeField]
    private int m_nHp;


    public float fAddStatmina
    {
        get
        {
            return m_addStatmina;
        }
    }

    public float fAddSpeed
    {
        get
        {
            return m_addSpeed;
        }
    }

    public int nAddHp
    {
        get
        {
            return m_nHp;
        }
    }
}
