using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossTheCosastofHand : UnitBase
{
    private enum E_BossState
    {
        E_NONE = -1,
        E_WAIT,
        E_ATTACK_CHOICE,
        
        E_ATTACK_RAKE_DELAY,
        E_ATTACK_RAKE,
        E_ATTACK_RAKE_AFTER,


        E_ATTACK_CHOPPING_DELAY,
        E_ATTACK_CHOPPING,
        E_ATTACK_CHOPPING_AFTER,



        E_ATTACK_SWEEP_DELAY,
        E_ATTACK_SWEEP,
        E_ATTACK_SWEEP_AFTER,


        E_ATTACK_RETURN,
        E_DIE,
        E_TOTAL,
    }

    private E_BossState m_eBossState;

    private E_BossState eBossState
    {
        set
        {
            m_fTick = .0f;
            m_eBossState = value;

            if(eBossState == E_BossState.E_ATTACK_RAKE_DELAY)
            {
                m_fCurrentPosX = m_v2GripReturnPoint.x;
                m_fTargetPosX = m_fCurrentPosX;
                m_fVelocity = .0f;
            }

            if(eBossState == E_BossState.E_ATTACK_CHOPPING)
            {
                cGrip.transform.localPosition = m_v2ChoppingPoints[m_nChoppingPointCurrentIndex];
            }

            if(eBossState == E_BossState.E_ATTACK_SWEEP_DELAY)
            {
                m_eAttackSweep = (E_Sweep)Random.Range((int)E_Sweep.Right, (int)E_Sweep.Left);

                cGrip.transform.localPosition = m_v2SweepPoints[(int)m_eAttackSweep];

                m_cRangeHit.sweepSetting(m_eAttackSweep);
            }

        }
        get
        {
            return m_eBossState;
        }
    }

    public enum E_Sweep
    {
        Right, Left
    }
    private E_Sweep m_eAttackSweep;


    [SerializeField] private float m_fWaitTime;

    [SerializeField] private float m_fAttackRakeDelayTime;
    [SerializeField] private float m_fAttackRakeHintTime;
    [SerializeField] private float m_fAttackRakeWaitTime;



    [SerializeField] private float m_fAttackChoppingDelayTime;
    [SerializeField] private float m_fAttackChoppingHintTime;
    [SerializeField] private float m_fAttackChoppingTime;


    [SerializeField] private float m_fAttackSweepDelayTime;
    [SerializeField] private float m_fAttackSweepTime;

    [SerializeField] private Vector2[] m_v2SweepPoints;




    [SerializeField] private BossAttackRangeHit m_cRangeHit;

    [SerializeField] private Vector2[] m_v2ChoppingAttackPoints;
    [SerializeField] private Vector2[] m_v2ChoppingPoints;
    private int m_nChoppingPointCurrentIndex;

    private delegate void BossEvent();
    private BossEvent[] m_delAI;


    private float m_fTick;

    private UnitBase m_cPlayer;

    private Tween m_attackTween;

    private Vector2 m_v2GripReturnPoint;

    private void Start()
    {
        init();
    }

    public override void init()
    {
        base.init();

        m_cGripWeapon = cGrip.GetComponentInChildren<WeaponBase>();
        m_cGripWeapon.init(this);

        cGrip.init(cGripWeapon.cWeaponData.fGripRange);

        m_cRangeHit.init();

        m_delAI = new BossEvent[(int)E_BossState.E_TOTAL];

        m_delAI[(int)E_BossState.E_WAIT] = waitEvent;
        
        m_delAI[(int)E_BossState.E_ATTACK_RAKE_DELAY] = attackRakeDelayEvent;
        m_delAI[(int)E_BossState.E_ATTACK_RAKE] = attackRakeEvent;
        m_delAI[(int)E_BossState.E_ATTACK_RAKE_AFTER] = attackRakeAfterEvent;
       

        m_delAI[(int)E_BossState.E_ATTACK_CHOPPING_DELAY] = attackChoppingDelayEvent;
        m_delAI[(int)E_BossState.E_ATTACK_CHOPPING] = attackChoppingEvent;
        m_delAI[(int)E_BossState.E_ATTACK_CHOPPING_AFTER] = attackChoppingAtferEvent;

        m_delAI[(int)E_BossState.E_ATTACK_SWEEP_DELAY] = attackSweepDelayEvent;
        m_delAI[(int)E_BossState.E_ATTACK_SWEEP] = attackSweepEvent;
        m_delAI[(int)E_BossState.E_ATTACK_SWEEP_AFTER] = attackSweepAfterEvent;


        m_delAI[(int)E_BossState.E_ATTACK_CHOICE] = attackChoiceEvent;



        m_delAI[(int)E_BossState.E_ATTACK_RETURN] = atttackReturnEvent;

        m_delAI[(int)E_BossState.E_DIE] = dieEvent;



        m_v2GripReturnPoint = (Vector2)cGrip.transform.position;

        isControl = false;



       
    }

    public void HandleWakeUp(UnitBase cPlayer)
    {
        m_cPlayer = cPlayer;
        isControl = true;
        isLookAble = true;
        isMoveAble = false;

        eBossState = E_BossState.E_WAIT;
    }

    public override void hit(UnitBase unit, WeaponAttackData cAttackData)
    {
        base.hit(unit, cAttackData);

        m_cImfect.hitimfect();

        Debug.Log("Boss hit");
    }


    private void waitEvent()
    {
        m_fTick += Time.deltaTime;
        if (m_fTick >= m_fWaitTime)
            eBossState = E_BossState.E_ATTACK_CHOICE;
            
    }

    private int m_nChoiceAttack = 0;

    private void attackChoiceEvent()
    {

        if (m_nChoiceAttack == 0)
            eBossState = E_BossState.E_ATTACK_RAKE_DELAY;
        else if (m_nChoiceAttack == 1)
            eBossState = E_BossState.E_ATTACK_CHOPPING_DELAY;
        else if(m_nChoiceAttack == 2)
        {
            eBossState = E_BossState.E_ATTACK_SWEEP_DELAY;
            m_nChoiceAttack = -1;
        }

        m_nChoiceAttack++;

    }

    private float m_fTargetPosX;
    private float m_fCurrentPosX;
    private float m_fVelocity;

    private void attackRakeDelayEvent()
    {
        m_fTick += Time.deltaTime;
        if (m_fTick >= m_fAttackRakeDelayTime)
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_NONE);
            eBossState = E_BossState.E_ATTACK_RAKE;
            return;
        }

        m_fTargetPosX = m_cPlayer.v2UnitPos.x;
        m_fCurrentPosX = Mathf.SmoothDamp(m_fCurrentPosX, m_fTargetPosX, ref m_fVelocity, 0.1f);
        cGrip.transform.position = new Vector3(m_fCurrentPosX, cGrip.transform.position.y);

        if(m_fTick >= m_fAttackRakeHintTime)
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_RAKE);
        }
    }

    private Vector2 m_v2RakeAttackPoint;

    private void attackRakeEvent()
    {
        isControl = false;

        m_v2RakeAttackPoint = new Vector2(cGrip.transform.position.x, m_cPlayer.v2UnitPos.y);
        m_cGripWeapon.attackEventStart();

        Utility.KillTween(m_attackTween);
        m_attackTween = cGrip.transform.DOMove(m_v2RakeAttackPoint, 0.5f).OnComplete(()=>
        {
            isControl = true;
            m_cGripWeapon.attackEventEnd();
            eBossState = E_BossState.E_ATTACK_RAKE_AFTER;
        });
    }

    private void attackRakeAfterEvent()
    {
        m_fTick += Time.deltaTime;
        if(m_fTick >= m_fAttackRakeWaitTime)
        {
            eBossState = E_BossState.E_ATTACK_RETURN;
        }
    }



    private void attackChoppingDelayEvent()
    {
        m_fTick += Time.deltaTime;

        if(m_fTick >= m_fAttackChoppingDelayTime)
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_NONE);
            eBossState = E_BossState.E_ATTACK_CHOPPING;
            m_nChoppingPointCurrentIndex = 0;
            return;
        }

        if (m_fTick >= m_fAttackChoppingHintTime)
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_CHOPPING);
        }
        

    }

    private float m_fChoppingTime = 0.5f;

    private void attackChoppingEvent()
    {
        m_fTick += Time.deltaTime;

        if(m_fTick >= m_fAttackChoppingTime)
        {
            isControl = false;
            Utility.KillTween(m_attackTween);

            m_cGripWeapon.attackEventStart();

            m_attackTween = cGrip.transform.DOLocalMove(m_v2ChoppingAttackPoints[m_nChoppingPointCurrentIndex], m_fChoppingTime).OnComplete(() =>
            {
                isControl = true;

                m_cGripWeapon.attackEventEnd();

                m_nChoppingPointCurrentIndex++;

                if (m_nChoppingPointCurrentIndex < m_v2ChoppingPoints.Length)
                    eBossState = E_BossState.E_ATTACK_CHOPPING;
                else
                {
                    m_nChoppingPointCurrentIndex = 0;
                    eBossState = E_BossState.E_ATTACK_CHOPPING_AFTER;
                }
                    

            });
        }




        
    }

    private void attackChoppingAtferEvent()
    {
        eBossState = E_BossState.E_ATTACK_RETURN;
    }




    private void attackSweepDelayEvent()
    {
        m_fTick += Time.deltaTime;

        if(m_fTick >= m_fAttackSweepDelayTime)
        {

            eBossState = E_BossState.E_ATTACK_SWEEP;
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_NONE);
            return;
        }

        m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_SWEEP);

    }

    private void attackSweepEvent()
    {
        isControl = false;
        m_cGripWeapon.attackEventStart();

        Utility.KillTween(m_attackTween);

        Vector2 v2LocalMovePos = m_v2SweepPoints[(int)E_Sweep.Left];
        if(m_eAttackSweep == E_Sweep.Left)
            v2LocalMovePos = m_v2SweepPoints[(int)E_Sweep.Right];


        m_attackTween = cGrip.transform.DOLocalMove(v2LocalMovePos, 1.0f).OnComplete(() =>
        {
            isControl = true;
            m_cGripWeapon.attackEventEnd();
            eBossState = E_BossState.E_ATTACK_SWEEP_AFTER;
        });


    }

    private void attackSweepAfterEvent()
    {
        eBossState = E_BossState.E_ATTACK_RETURN;
    }


    private void atttackReturnEvent()
    {
        isControl = false;
        Utility.KillTween(m_attackTween);
        m_attackTween = cGrip.transform.DOMove(m_v2GripReturnPoint, 1.0f).OnComplete(() => 
        {
            isControl = true;
            eBossState = E_BossState.E_WAIT;
        });

    }
    private void dieEvent()
    {

    }

    private void Update()
    {
        if (!isControl)
            return;

        m_delAI[(int)eBossState]();
    }

    
}
