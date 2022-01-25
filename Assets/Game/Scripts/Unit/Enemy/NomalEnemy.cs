using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NomalEnemy : UnitEnemyBase, IUpdate
{
    [SerializeField]
    private float m_fSerchRange;
    [SerializeField]
    private LayerMask m_targetLayer;
    [SerializeField]
    private float m_fAttakWaitTime;


    private bool m_bDelayImfect;

    private Vector2 m_v2RecallPoint;

    [SerializeField]
    private float m_fWallSensorDistance;
    [SerializeField]
    private LayerMask m_wallLayer;




    private enum E_EnemyState
    {
        E_NONE,
        E_TRACKING,

        E_ATTACK,
        E_ATTACKINGSTOP,
        E_ATTACKINGLOOK,
        
        E_DIE,

        E_TOTAL
    }

    [SerializeField]  private E_EnemyState m_eEnemyState;


    private delegate void EnemyEvent();
    private EnemyEvent[] m_delAI;

    private float m_fTrackingTime;
    private float m_fStiffnessTime;

    [SerializeField]
    private UnitSound m_cSound;



    [SerializeField]
    private bool m_bAttackingLook;



    public override void init()
    {
        base.init();


        m_cGripWeapon.init(this);

        cAnimation.setWeaponHandle(m_cGripWeapon);

        cGrip.gripSetting(cGripWeapon.cWeaponData.fGripRange);
        cGrip.gripUpdate(v2LookDir);



        m_bDelayImfect = false;



        m_v2RecallPoint = v2UnitPos;


        m_delAI = new EnemyEvent[(int)E_EnemyState.E_TOTAL];

        m_delAI[(int)E_EnemyState.E_NONE] = noneEvent;

        m_delAI[(int)E_EnemyState.E_TRACKING] = trackingEvent;

        m_delAI[(int)E_EnemyState.E_ATTACK] = attackEvent;
        m_delAI[(int)E_EnemyState.E_ATTACKINGSTOP] = attackingStop;
        m_delAI[(int)E_EnemyState.E_ATTACKINGLOOK] = attackingLook;



        m_delAI[(int)E_EnemyState.E_DIE] = dieEvent;


        eEnemyState = E_EnemyState.E_NONE;


        isControl = false;


        
        UpdateManager.Instance.addProcesses(this);
    }

    public override void dieEvent()
    {
        base.dieEvent();


        isControl = false;
        gameObject.layer = 11;

        cAnimation.die();
        cGripWeapon.attackDrawHit(Vector2.zero, false);

        dropEvent();

        Invoke("spawnDisable", 3.0f);
    }

    public override void hit(UnitBase unit, WeaponAttackData cAttackData)
    {

        base.hit(unit, cAttackData);


        
        if (isDie)
        {
            eEnemyState = E_EnemyState.E_DIE;
            return;
        }


        m_cSound.hitPlayOnce();


        cAnimation.hit();
        cGripWeapon.reset();

        Vector2 v2UnitToHitUnitDir = v2UnitPos - unit.v2UnitPos;
        v2UnitToHitUnitDir = v2UnitToHitUnitDir.normalized;

        knockBack(v2UnitToHitUnitDir, 10, 0.1f, false);




    }

    public void hitEndEvent()
    {
        isMoveAble = true;
        isLookAble = true;

        eEnemyState = E_EnemyState.E_TRACKING;
    }

    public void attackEndEvent()
    {
        eEnemyState = E_EnemyState.E_TRACKING;
    }



    private void noneEvent()
    {

    }

    private void attackingStop()
    {

    }

    private void attackingLook()
    {
        setTargetDestination();

        m_cGripWeapon.attackDrawHit(v2LookDir, true);
    }






    private void attackEvent()
    {
        if (cGripWeapon.cCoolTime.isCoolTime)
            return;

        attack();

        if (m_bAttackingLook)
        {
            eEnemyState = E_EnemyState.E_ATTACKINGLOOK;
        }
        else
        {
            eEnemyState = E_EnemyState.E_ATTACKINGSTOP;
        }

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



            if(m_eEnemyState == E_EnemyState.E_ATTACK)
            {
                navTrackingStop();
            }




            if(m_eEnemyState == E_EnemyState.E_TRACKING)
            {
                navTrackingReStart();

                m_fTrackingTime = .0f;
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


    public string id
    {
        get
        {
            return gameObject.name;
        }

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


    }



    private void trackingEvent()
    {
        if (cTargetUnit == null)
            return;

        setTargetDestination();
       // m_cSound.footStepPlayOnce();

        if (inRange(m_fAttackRange))
        {
             
            eEnemyState = E_EnemyState.E_ATTACK;
            return;
        }

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

        if (isControl)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + ((Vector3)v2LookDir * m_fWallSensorDistance));
        }

    }

    private bool wallChack()
    {
        bool isWallCheck = Physics2D.Raycast(v2UnitPos, v2LookDir, m_fWallSensorDistance, m_wallLayer);


        return isWallCheck;
    }

    public void updateProcesses()
    {
        if (!isControl)
            return;


        m_delAI[(int)eEnemyState]();
    }



    private void OnDestroy()
    {
        UpdateManager.Instance.removeProcesses(this);
    }

    protected override void spawnDisable()
    {
        base.spawnDisable();
    }

    public override void handleSpawn(Spawner cSpawner, UnitBase cTargetUnit)
    {
        base.handleSpawn(cSpawner, cTargetUnit);

        gameObject.layer = 6;
        m_cGripWeapon.reset();


        eEnemyState = E_EnemyState.E_TRACKING;
    }





}
