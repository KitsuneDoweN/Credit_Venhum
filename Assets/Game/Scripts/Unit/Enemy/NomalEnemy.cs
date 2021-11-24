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
    private UnitBloodImfect m_cBloodImfect;
    [SerializeField]
    private EnemySerchIcon m_cSerchIcon;



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



    private void Start()
    {
        init();
    }

    public override void init()
    {
        base.init();

        m_cGripWeapon.init(this);

        cAnimation.setWeaponHandle(m_cGripWeapon);

        cGrip.gripSetting(cGripWeapon.cWeaponData.fRange);
        cGrip.gripUpdate(v2LookDir);

        m_cBloodImfect.init();

        isControl = true;
        isMoveAble = true;
        isLookAble = true;

        m_cSerchIcon.init();


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

        cAnimation.hit();

        if (m_ieAttackWaitEventCoroutine != null)
        {
            StopCoroutine(m_ieAttackWaitEventCoroutine);
            m_ieAttackWaitEventCoroutine = null;
        }

        Vector2 v2UnitToHitUnitDir = v2UnitPos - unit.v2UnitPos ;
        v2UnitToHitUnitDir = v2UnitToHitUnitDir.normalized;

        m_cBloodImfect.bloodImfect(v2UnitToHitUnitDir);


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
                if (m_eEnemyState == E_EnemyState.E_WAIT)
                    setTarget(null);

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

        if(cTargetUnit == null || !inRange(m_fSerchRange))
        {
            eEnemyState = E_EnemyState.E_WAIT;
            m_cSerchIcon.drawIcon(EnemySerchIcon.E_type.E_OFF);
            return;
        }



        setTargetDestination();


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
    }


    


}
