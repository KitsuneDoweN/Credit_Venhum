using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class UnitEnemyBase : UnitBase
{

    [SerializeField] private NavMeshAgent m_navAgent;

    [SerializeField]
    protected float m_fAttackRange;
    
    private UnitBase m_cTargetUnit;

    public UnityEvent hitEndEvent;

    public UnityEvent attackEndEvent;

    [SerializeField] private UnityEvent m_dieEvent;



    public override Vector2 v2Velocity
    {
        set
        {
            m_navAgent.velocity = value;
        }
        get
        {
            return m_navAgent.velocity;
        }

    }


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

    private void Start()
    {
        m_navAgent.speed = m_cStatus.fSpeed;
        m_navAgent.updateRotation = false;
        m_navAgent.updateUpAxis = false;
    }


    public override void init()
    {
        base.init();

        isControl = true;
        isMoveAble = true;
        isLookAble = true;



        m_cGripWeapon = cGrip.GetComponentInChildren<WeaponBase>();
        m_cGripWeapon.init(this);

        cGrip.init(cGripWeapon.cWeaponData.fGripRange);





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

    protected bool isStop
    {
        get
        {
            return m_navAgent.isStopped;
        }
    }

    protected virtual void navTrackingStop()
    {
        m_navAgent.isStopped = true;

        v2NextMoveDir = Vector2.zero;

        moveDirUpdate();

    }

    protected virtual void navTrackingReStart()
    {
        m_navAgent.isStopped = false;
    }



    protected virtual void setTargetDestination()
    {
        m_navAgent.SetDestination(cTargetUnit.transform.position);

        Vector2 v2Dir = cTargetUnit.v2UnitPos - v2UnitPos;

        v2NextMoveDir = v2Dir;
        v2NextLookDir = v2Dir;

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


        m_dieEvent.Invoke();
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

    protected void goPoint(Vector2 point)
    {
        m_navAgent.SetDestination((Vector3)point);

        Vector2 v2Dir = point - v2UnitPos;

        v2NextMoveDir = v2Dir;
        v2NextLookDir = v2Dir;

        movementUpdate();
    }

}
