using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitEnemyBase : UnitBase
{

    [SerializeField] private NavMeshAgent m_navAgent;

    protected float m_fAttackRange;
    
    private UnitBase m_cTargetUnit;

    protected UnitBase cTargetUnit
    {
        set
        {
            m_cTargetUnit = value;
        }
        get
        {
            return m_cTargetUnit;
        }
    }

    public override void init()
    {
        base.init();


        setTarget(GameManager.instance.cStageManager.cPlayer);

        m_navAgent.updateRotation = false;
        m_navAgent.updateUpAxis = false;

        m_fAttackRange = cGripWeapon.cWeaponData.fRange;
        cAnimation.init();
    }


    public override void hit(UnitBase unit, WeaponAttackData cAttackDatas)
    {
        base.hit(unit, cAttackDatas);


    }

    public virtual void handleSpawn()
    {
        init();
    }

    protected void navTrackingStop()
    {
        m_navAgent.isStopped = true;
        m_navAgent.velocity = Vector3.zero;
        v2MoveDir = Vector2.zero;
    }

    protected void navTrackingReStart()
    {
        m_navAgent.isStopped = false;
    }



    protected void setTargetDestination()
    {
        m_navAgent.SetDestination(cTargetUnit.transform.position);
    }

    public void setTarget(UnitBase cTargetUnit)
    {
        m_cTargetUnit = cTargetUnit;
    }


    public override void die()
    {
        navTrackingStop();
        base.die();
    }

    protected bool inRange(float fRange)
    {
        bool bResult = false;

        float fTargetRange = Vector2.Distance(cTargetUnit.v2UnitPos, v2UnitPos);
        fTargetRange = Mathf.Abs(fTargetRange);

        if (fTargetRange <= fRange)
            bResult = true;

        return bResult;
    }


}
