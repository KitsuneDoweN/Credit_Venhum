using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaEnemyManager : MonoBehaviour
{
    private  List<UnitEnemyBase> m_cAreaEnemyList;
    private List<UnitEnemyBase> m_cAreaEnemyDieList;


    [SerializeField] Barricade m_cBarricade;

    [SerializeField] Transform m_trEnemys;

    [SerializeField]
    private UnityEvent m_cDieProcessEvent;


    private void Start()
    {
        init();
    }

    public void init()
    {
        setAreaEnemys();
    }

    private void setAreaEnemys()
    {
        m_cAreaEnemyList = new List<UnitEnemyBase>();
        m_cAreaEnemyDieList = new List<UnitEnemyBase>();

        m_cAreaEnemyList.Capacity = m_trEnemys.childCount;
        m_cAreaEnemyDieList.Capacity = m_cAreaEnemyList.Capacity;

        for (int i = 0; i < m_cAreaEnemyList.Capacity; i++)
        {
            UnitEnemyBase enemyUnit = m_trEnemys.GetChild(i).GetComponent<UnitEnemyBase>();
            enemyUnit.init();
            m_cAreaEnemyList.Add(enemyUnit);
        }
    }

    public void unitDieProcessed(UnitEnemyBase unit)
    {
        m_cDieProcessEvent.Invoke();

        m_cAreaEnemyDieList.Add(unit);

        if(m_cAreaEnemyDieList.Count == m_cAreaEnemyList.Count)
        {
            m_cBarricade.eventOn();
        }
    }

}
