using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundHandle : MonoBehaviour
{
    [SerializeField]
    private enemySound m_cEnemySound;

    public void hitPlayOnce()
    {
        m_cEnemySound.hitPlayOnce();
    }

    public void deathPlayOnce()
    {
        m_cEnemySound.deathPlayOnce();
    }

    public void attackPlayOnce()
    {
        m_cEnemySound.attackPlayOnce();
    }

    public void footStepPlayOnce()
    {
        m_cEnemySound.footStepPlayOnce();
    }
}
