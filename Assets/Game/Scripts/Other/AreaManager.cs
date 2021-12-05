using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField]
    AreaEnemyManager[] m_cAreaEnemys;

    public void init()
    {
        foreach(AreaEnemyManager  enemyManager in m_cAreaEnemys)
        {
            enemyManager.init();
        }

        m_cAreaEnemys[0].enemyWakeUp();
        m_cAreaEnemys[1].enemyWakeUp();
    }




}
