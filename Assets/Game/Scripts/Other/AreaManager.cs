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


        for (int i = 0; i < m_cAreaEnemys.Length; i++)
        {
            if (GameManager.Instance.nClearGrogress > m_cAreaEnemys[i].nClear)
            {
                m_cAreaEnemys[i].skip();
            }
        }

      



    }

    public void handleWakeUp(int nIndex)
    {
        if (m_cAreaEnemys[nIndex].isSkip)
            return;

        m_cAreaEnemys[nIndex].enemyWakeUp();
    }




}
