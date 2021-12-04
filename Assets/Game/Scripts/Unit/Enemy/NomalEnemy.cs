using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NomalEnemy : UnitEnemyBase
{
    [SerializeField]
    private float m_fSerchRange;
    [SerializeField]
    private LayerMask m_targetLayer;
    [SerializeField]
    private float m_fAttakWaitTime;

    [SerializeField]
    private EnemySerchIcon m_cSerchIcon;

    private bool m_bDelayImfect;

    private Vector2 m_v2RecallPoint;


    private enum E_EnemyState
    {
        E_NONE,
        E_WAIT,
        E_TRACKING,
        E_ATTACK,
        E_ATTACKWAIT,
        E_STIFFNESS,
        E_RECALL,
        
        E_DIE,

        E_TOTAL
    }

    [SerializeField]  private E_EnemyState m_eEnemyState;


    private delegate void EnemyEvent();
    private EnemyEvent[] m_delAI;





    private float m_fTrackingTime;
    private float m_fStiffnessTime;



    public override void init()
    {
        base.init();



        m_cGripWeapon.init(this);

        cAnimation.setWeaponHandle(m_cGripWeapon);

        cGrip.gripSetting(cGripWeapon.cWeaponData.fGripRange);
        cGrip.gripUpdate(v2LookDir);



        isControl = true;
        isMoveAble = true;
        isLookAble = true;

        m_bDelayImfect = false;

        m_cSerchIcon.init();

        m_v2RecallPoint = v2UnitPos;


        m_delAI = new EnemyEvent[(int)E_EnemyState.E_TOTAL];

        m_delAI[(int)E_EnemyState.E_NONE] = noneEvent;
        m_delAI[(int)E_EnemyState.E_WAIT] = waitEvent;
        m_delAI[(int)E_EnemyState.E_TRACKING] = trackingEvent;

        m_delAI[(int)E_EnemyState.E_ATTACK] = attackEvent;
        m_delAI[(int)E_EnemyState.E_ATTACKWAIT] = attackWaitEvent;
        m_delAI[(int)E_EnemyState.E_STIFFNESS] = stiffnessEvent;
        m_delAI[(int)E_EnemyState.E_RECALL] = recallEvent;
        m_delAI[(int)E_EnemyState.E_DIE] = dieEvent;


        eEnemyState = E_EnemyState.E_WAIT;


    }








    public override void die()
    {
        base.die();

    }

    public override void hit(UnitBase unit, WeaponAttackData cAttackData)
    {
        int nOldStiffness = cStatus.nCurrentStiffness;

        base.hit(unit, cAttackData);


        
        if (isDie)
        {
            eEnemyState = E_EnemyState.E_DIE;
            return;
        }




        Vector2 v2UnitToHitUnitDir = v2UnitPos - unit.v2UnitPos;
        v2UnitToHitUnitDir = v2UnitToHitUnitDir.normalized;




        if (eEnemyState != E_EnemyState.E_WAIT && eEnemyState != E_EnemyState.E_TRACKING
            && nOldStiffness == cStatus.nCurrentStiffness)
            return;

        cAnimation.hit();


        Debug.Log(v2UnitToHitUnitDir);
        dushDetail(v2UnitToHitUnitDir, 10, 0.1f, false);

    }

    public void hitEndEvent()
    {
        if(eEnemyState == E_EnemyState.E_ATTACK)
        {
            eEnemyState = E_EnemyState.E_STIFFNESS;
            return;
        }

        eEnemyState = E_EnemyState.E_WAIT;

    }

    public void attackEndEvent()
    {
        eEnemyState = E_EnemyState.E_TRACKING;
    }



    private void noneEvent()
    {

    }

    private void attackWaitEvent()
    {

    }



    private void attackEvent()
    {
        attack();

        eEnemyState = E_EnemyState.E_ATTACKWAIT;
    }


    protected void dieEvent()
    {
        isControl = false;
        gameObject.layer = 11;
        m_cSerchIcon.die();
        cAnimation.die();

        die();
    }

    public override void attack()
    {
        base.attack();

    }

    private E_EnemyState eEnemyState
    {
        set
        {
            m_eEnemyState = value;



            if (m_eEnemyState == E_EnemyState.E_WAIT)
            {
                setTarget(null);
                navTrackingStop();
            }

            if(m_eEnemyState == E_EnemyState.E_ATTACK)
            {
                navTrackingStop();
            }

            if(m_eEnemyState == E_EnemyState.E_STIFFNESS)
            {
                m_cAnimation.trigger("stiffness");
                navTrackingStop();
                m_fStiffnessTime = .0f;
            }


            if(m_eEnemyState == E_EnemyState.E_TRACKING)
            {
                navTrackingReStart();

                m_fTrackingTime = .0f;
            }

            if(m_eEnemyState == E_EnemyState.E_RECALL)
            {
                navTrackingReStart();
                goPoint(m_v2RecallPoint);
            }

            if(m_eEnemyState == E_EnemyState.E_DIE)
            {
                navTrackingStop();
            }
            
        }
        get
        {
            return m_eEnemyState;
        }

    }
    protected override int nStiffness
    {
        set
        {
            base.nStiffness = value;


            if(nStiffness >= cStatus.nMaxStiffness)
            {
                eEnemyState = E_EnemyState.E_STIFFNESS;
                nStiffness = 0;
            }

        }
        get
        {
            return base.nStiffness;
        }
    }



    private void Update()
    {
        if (!isControl)
            return;


        m_delAI[(int)eEnemyState]();


    }

    private void waitEvent()
    {
        if (cTargetUnit == null)
        {
            Collider2D targetCollider = Physics2D.OverlapCircle(v2UnitPos, m_fSerchRange, m_targetLayer);
            if (targetCollider == null)
                return;
            cTargetUnit = targetCollider.GetComponent<UnitBase>();

        }

        eEnemyState = E_EnemyState.E_TRACKING;
        m_cSerchIcon.drawIcon(EnemySerchIcon.E_type.E_ON);

    }



    private void trackingEvent()
    {

        if(cTargetUnit == null)
        {
            eEnemyState = E_EnemyState.E_RECALL;
            m_cSerchIcon.drawIcon(EnemySerchIcon.E_type.E_OFF);
            return;
        }
        else if(!inRange(m_fSerchRange))
        {
            navTrackingStop();
            m_fTrackingTime += Time.deltaTime;
            if(m_fTrackingTime >= 3.0f)
            {
                eEnemyState = E_EnemyState.E_RECALL;
                m_cSerchIcon.drawIcon(EnemySerchIcon.E_type.E_OFF);
            }
            return;
        }
        m_fTrackingTime = .0f;


        setTargetDestination();
       // m_cSound.footStepPlayOnce();

        if (inRange(m_fAttackRange))
        {
             
            eEnemyState = E_EnemyState.E_ATTACK;
            return;
        }

    }


    private void stiffnessEvent()
    {
        m_fStiffnessTime += Time.deltaTime;

        if (m_fStiffnessTime >= 1.0f)
        {
            eEnemyState = E_EnemyState.E_TRACKING;
            m_cAnimation.trigger("movement");
            return;
        }
            

    }

    private void recallEvent()
    {
        if (Vector2.Distance(v2UnitPos, m_v2RecallPoint) <= 0.1f)
            eEnemyState = E_EnemyState.E_WAIT;
    }


    private void OnDrawGizmos()
    {
        //Attack
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_fAttackRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_fSerchRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)v2LookDir);
    }


    


}
