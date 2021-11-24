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

    private float m_fAttakWaitTime;

    private enum E_EnemyState
    {
        E_NONE,
        E_WAIT,
        E_TRACKING,
        E_ATTACK,

        E_TOTAL
    }

    [SerializeField]  private E_EnemyState m_eEnemyState;






    private delegate void EnemyEvent();
    private EnemyEvent[] m_delAI;

    private IEnumerator m_ieAttackWaitEventCoroutine;

    private float m_fWaitTime;





    public override void init()
    {
        base.init();

        m_cGripWeapon.init(this);

        m_bControl = true;

        setTarget(GameManager.instance.cStageManager.cPlayer);

         m_delAI = new EnemyEvent[(int)E_EnemyState.E_TOTAL];

        m_delAI[(int)E_EnemyState.E_NONE] = noneEvent;
        m_delAI[(int)E_EnemyState.E_ATTACK] = attackEvent;
        m_delAI[(int)E_EnemyState.E_WAIT] = waitEvent;
        m_delAI[(int)E_EnemyState.E_TRACKING] = trackingEvent;

        eEnemyState = E_EnemyState.E_WAIT;


    }



    public override void die()
    {
        base.die();
    }

    public override void hit(UnitBase unit, WeaponAttackData cAttackDatas)
    {
        base.hit(unit, cAttackDatas);
        if (isDie)
            return;

        if (m_ieAttackWaitEventCoroutine != null)
        {
            StopCoroutine(m_ieAttackWaitEventCoroutine);
            m_ieAttackWaitEventCoroutine = null;
        }
            


       eEnemyState = E_EnemyState.E_WAIT;

        
    }

    private void noneEvent()
    {

    }


    private void attackEvent()
    {
        if (m_ieAttackWaitEventCoroutine != null)
            return;

        m_fWaitTime += Time.deltaTime;

        if (m_fWaitTime < 1.0f)
            return;

        attack();


    }

    public override void attack()
    {
        base.attack();

        m_ieAttackWaitEventCoroutine = attackWaitEventCoroutine();
        StartCoroutine(m_ieAttackWaitEventCoroutine);
    }

    IEnumerator attackWaitEventCoroutine()
    {
        

        float fTime = .0f;

        while(fTime < m_fAttakWaitTime && isControl)
        {
            fTime += Time.deltaTime;
            yield return null;
        }

        m_ieAttackWaitEventCoroutine = null;

        if (!isControl)
            yield break ;

        
        eEnemyState = E_EnemyState.E_TRACKING;
    }


 



    private E_EnemyState eEnemyState
    {
        set
        {
            m_eEnemyState = value;

            if (m_eEnemyState == E_EnemyState.E_WAIT || m_eEnemyState == E_EnemyState.E_ATTACK)
            {
                m_fWaitTime = .0f;
                navTrackingStop();
            }

            if(m_eEnemyState == E_EnemyState.E_TRACKING)
            {
                navTrackingReStart();
            }
            
            
        }
        get
        {
            return m_eEnemyState;
        }

    }


    private void Update()
    {
        

        if (!isControl || !isMoveAble )
            return;


        if(cTargetUnit != null)
        {
            v2LookDir = (cTargetUnit.v2UnitPos - v2UnitPos);
            v2MoveDir = v2LookDir;
        }

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


    }



    private void trackingEvent()
    {

        if(cTargetUnit == null)
        {
            eEnemyState = E_EnemyState.E_WAIT;
            return;
        }



        setTargetDestination();



        if (inRange(m_fAttackRange))
        {
             
            eEnemyState = E_EnemyState.E_ATTACK;
        }

    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, m_fAttackRange);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)v2LookDir);
    }



}
