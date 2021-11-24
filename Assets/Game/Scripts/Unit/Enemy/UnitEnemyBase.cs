using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitEnemyBase : UnitBase
{

    [SerializeField] private NavMeshAgent m_navAgent;

    [SerializeField]
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

        isControl = true;
        isMoveAble = true;
        isLookAble = true;



        m_cGripWeapon = cGrip.GetComponentInChildren<WeaponBase>();
        m_cGripWeapon.init(this);

        cGrip.init(cGripWeapon.cWeaponData.fRange);

        m_navAgent.speed = m_cStatus.fSpeed;
        m_navAgent.updateRotation = false;
        m_navAgent.updateUpAxis = false;

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
        v2Velocity = Vector2.zero;
    }

    protected void navTrackingReStart()
    {
        m_navAgent.isStopped = false;
    }



    protected virtual void setTargetDestination()
    {
        m_navAgent.SetDestination(cTargetUnit.transform.position);

        Vector2 v2Dir = cTargetUnit.v2UnitPos - v2UnitPos;

        v2OldMoveDir = v2Dir;
        v2OldLookDir = v2Dir;

        movementUpdate();


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
