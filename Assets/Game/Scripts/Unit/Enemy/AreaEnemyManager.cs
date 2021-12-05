using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AreaEnemyManager : MonoBehaviour
{
    private  List<UnitEnemyBase> m_cAreaEnemyList;
    private int m_nAreaEnemyDieCount;


    [SerializeField] Barricade m_cBarricade;

    [SerializeField] Transform m_trEnemys;

    [SerializeField]
    private UnityEvent m_cDieProcessEvent;



    public void init()
    {
        setAreaEnemys();
    }

    private void setAreaEnemys()
    {
        m_cAreaEnemyList = new List<UnitEnemyBase>();
        m_nAreaEnemyDieCount = 0;

        m_cAreaEnemyList.Capacity = m_trEnemys.childCount;


        for (int i = 0; i < m_cAreaEnemyList.Capacity; i++)
        {
            UnitEnemyBase enemyUnit = m_trEnemys.GetChild(i).GetComponent<UnitEnemyBase>();
            enemyUnit.init();
            m_cAreaEnemyList.Add(enemyUnit);
        }
    }



    public void enemyWakeUp()
    {
        foreach(UnitEnemyBase unit in m_cAreaEnemyList)
        {
            unit.isControl = true;
        }
    }


    public void unitDieProcessed()
    {
        m_cDieProcessEvent.Invoke();

        m_nAreaEnemyDieCount++;

        if (m_nAreaEnemyDieCount == m_cAreaEnemyList.Count)
        {
            m_cBarricade.eventOn();
        }
    }

}
