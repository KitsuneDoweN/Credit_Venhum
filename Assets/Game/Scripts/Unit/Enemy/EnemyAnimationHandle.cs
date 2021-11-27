using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandle : MonoBehaviour
{
    [SerializeField]
    private UnitEnemyBase m_cUnit;


    public void hitEndEvent()
    {
        m_cUnit.hitEndEvent.Invoke();
    }

    public void attackEndEvent()
    {
        m_cUnit.attackEndEvent.Invoke();
    }

}
