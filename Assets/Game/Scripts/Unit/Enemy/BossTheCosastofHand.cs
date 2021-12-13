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

    private enum E_AttackPattern 
    {
        E_NONE = -1, E_RAKE, E_CHOPPING, E_SWEEP, E_TOTAL
    }

    private enum E_AttackTimeState
    {
        E_NONE = -1, E_DELAY, E_HINT, E_ATTACK, E_WAIT, E_TOTAL
    }

    private float[] m_fAttackPatterns = { .0f, 0.5f, 1.0f };


    [SerializeField] private float m_fWaitTime;
    [SerializeField] private float [] m_fAttackRakeTimes;
    [SerializeField] private float [] m_fAttackChoppingTimes;
    [SerializeField] private float [] m_fAttackSweepTimes;


    private float m_fCurrentWaitTime;

    private float[] m_fCurrentAttackRakeTimes;
    private float[] m_fCurrentAttackChoppingTimes;
    private float[] m_fCurrentAttackSweepTimes;


    [SerializeField] private Vector2[] m_v2SweepPoints;




    [SerializeField] private BossAttackRangeHit m_cRangeHit;


    [SerializeField] private Vector2[] m_v2ChoppingAttackPoints;
    [SerializeField] private Vector2[] m_v2ChoppingPoints;

    [SerializeField] private Animator m_cHandAnimator;

    private int m_nChoppingPointCurrentIndex;

    private delegate void BossEvent();
    private BossEvent[] m_delAI;


    private float m_fTick;

    private UnitBase m_cPlayer;

    private Tween m_attackTween;

    private Vector2 m_v2GripReturnPoint;

    //[SerializeField]
    //private UnitHitConnet m_cUnitHitConnnet;

    [SerializeField]
    private float m_fBerserkerSpeed;


    private bool m_bBerserkerMode;
    private bool isBerserkerMode
    {
        set
        {
            m_bBerserkerMode = value;
            if (m_bBerserkerMode)
                berserkerModeOn();

        }
        get
        {
            return m_bBerserkerMode;
        }
    }


    [SerializeField]
    private int nBerserkerHp;

    [SerializeField]
    private BoxCollider2D m_cHandCollider;

    private Vector2 m_v2HandColliderSizeDefault;


    [SerializeField]
    private Sprite m_bossIcon;


    [SerializeField]
    private GameObject m_imfect;
    [SerializeField]
    private Sprite m_BrokenSprite;

    [SerializeField]
    private UnitSound m_cSound;

    [SerializeField]
    private SpriteRenderer m_srGirpWeapon;


    private void Start()
    {
        init();
    }

    private void attackTimeSetting()
    {
        m_fCurrentAttackRakeTimes = new float[m_fAttackRakeTimes.Length];
        m_fCurrentAttackChoppingTimes = new float[m_fAttackChoppingTimes.Length];
        m_fCurrentAttackSweepTimes = new float[m_fAttackSweepTimes.Length];

        m_fCurrentWaitTime = m_fWaitTime;

        for (int i = 0; i < (int)E_AttackTimeState.E_TOTAL; i++)
        {
            m_fCurrentAttackRakeTimes[i] = m_fAttackRakeTimes[i];
            m_fCurrentAttackChoppingTimes[i] = m_fAttackChoppingTimes[i];
            m_fCurrentAttackSweepTimes[i] = m_fAttackSweepTimes[i];
        }
    }

    private void eventSetting()
    {
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

        eBossState = E_BossState.E_NONE;
    }


    public override void init()
    {
        base.init();

        isBerserkerMode = false;

       // m_cUnitHitConnnet.init(this);

        m_cGripWeapon = cGrip.GetComponentInChildren<WeaponBase>();
        m_cGripWeapon.init(this);

        cGrip.init(cGripWeapon.cWeaponData.fGripRange);

        m_cRangeHit.init();


        attackTimeSetting();

        eventSetting();

        m_v2HandColliderSizeDefault = m_cHandCollider.size;


        m_v2GripReturnPoint = (Vector2)cGrip.transform.position;

        isControl = false;



       
    }

    public void HandleWakeUp(UnitBase cPlayer)
    {
        m_cPlayer = cPlayer;
        isControl = true;
        isLookAble = true;
        isMoveAble = false;


        GameManager.instance.cUIManager.cUI_InGame.cUI_BossHp.toggle(true);
        GameManager.instance.cUIManager.cUI_InGame.cUI_BossHp.draw(m_bossIcon, nHP, m_cStatus.nMaxHp);


        eBossState = E_BossState.E_WAIT;
    }

    public override void hit(UnitBase unit, WeaponAttackData cAttackData)
    {
        base.hit(unit, cAttackData);

        GameManager.instance.cUIManager.cUI_InGame.cUI_BossHp.draw(m_bossIcon, nHP, m_cStatus.nMaxHp);

        if (!isBerserkerMode && nHP <= nBerserkerHp)
        {
            isBerserkerMode = true;
        }


        m_cImfect.hitimfect();

        Debug.Log("Boss hit");

        if(isDie)
        {
            eBossState = E_BossState.E_DIE;
            return;
        }

        m_cSound.hitPlayOnce();

    }


    private void waitEvent()
    {
        m_fTick += Time.deltaTime;
        if (m_fTick >= m_fCurrentWaitTime)
            eBossState = E_BossState.E_ATTACK_CHOICE;
            
    }

    private int m_nChoiceAttack = 0;

    private void attackChoiceEvent()
    {
       // m_cUnitHitConnnet.isHitAble = false;

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
        if (m_fTick >=  m_fCurrentAttackRakeTimes[(int)E_AttackTimeState.E_DELAY])
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_NONE);
            eBossState = E_BossState.E_ATTACK_RAKE;
            return;
        }

        m_fTargetPosX = m_cPlayer.v2UnitPos.x;
        m_fCurrentPosX = Mathf.SmoothDamp(m_fCurrentPosX, m_fTargetPosX, ref m_fVelocity, 0.1f);
        cGrip.transform.position = new Vector3(m_fCurrentPosX, cGrip.transform.position.y);

        if(m_fTick >= m_fCurrentAttackRakeTimes[(int)E_AttackTimeState.E_HINT])
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_RAKE);
        }
    }

    private Vector2 m_v2RakeAttackLocalPoint;

    private void attackRakeEvent()
    {
        isControl = false;

        m_v2RakeAttackLocalPoint = new Vector2(cGrip.transform.localPosition.x, -15.0f );
        m_cGripWeapon.attackEventStart();

        Utility.KillTween(m_attackTween);

        handAttackAnimation(E_AttackPattern.E_RAKE);

        m_cSound.attackPlayOnce(0);

        m_attackTween = cGrip.transform.DOLocalMove(m_v2RakeAttackLocalPoint, 0.5f).OnComplete(()=>
        {
            isControl = true;
            m_cGripWeapon.attackEventEnd();
            eBossState = E_BossState.E_ATTACK_RAKE_AFTER;
        });
    }

    private void attackRakeAfterEvent()
    {
        m_fTick += Time.deltaTime;
        if(m_fTick >=   m_fCurrentAttackRakeTimes[(int)E_AttackTimeState.E_WAIT])
        {
            eBossState = E_BossState.E_ATTACK_RETURN;
        }
    }



    private void attackChoppingDelayEvent()
    {
        m_fTick += Time.deltaTime;

        if(m_fTick >=  m_fCurrentAttackChoppingTimes[(int)E_AttackTimeState.E_DELAY])
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_NONE);
            eBossState = E_BossState.E_ATTACK_CHOPPING;
            m_nChoppingPointCurrentIndex = 0;
            return;
        }

        if (m_fTick >= m_fCurrentAttackChoppingTimes[(int)E_AttackTimeState.E_HINT])
        {
            m_cRangeHit.draw(BossAttackRangeHit.E_Type.E_CHOPPING);
        }
        

    }

    private float m_fChoppingTime = 0.5f;

    private void attackChoppingEvent()
    {
        m_fTick += Time.deltaTime;

        if(m_fTick >= m_fCurrentAttackChoppingTimes[(int)E_AttackTimeState.E_ATTACK])
        {
            isControl = false;
            Utility.KillTween(m_attackTween);

            m_cGripWeapon.attackEventStart();


            handAttackAnimation(E_AttackPattern.E_CHOPPING);
            m_cSound.attackPlayOnce(1);
            m_attackTween = cGrip.transform.DOLocalMove(m_v2ChoppingAttackPoints[m_nChoppingPointCurrentIndex], m_fChoppingTime).OnComplete(() =>
            {
                isControl = true;

                m_cGripWeapon.attackEventEnd();

                m_nChoppingPointCurrentIndex++;

                if (m_nChoppingPointCurrentIndex < m_v2ChoppingPoints.Length)
                {
                    eBossState = E_BossState.E_ATTACK_CHOPPING;
                    handIdleAnimation();
                }
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

        if(m_fTick >=  m_fCurrentAttackSweepTimes[(int)E_AttackTimeState.E_DELAY])
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


        m_cHandCollider.size = new Vector2(m_v2HandColliderSizeDefault.x, 20.0f);


        Vector2 v2LocalMovePos = m_v2SweepPoints[(int)E_Sweep.Left];
        if(m_eAttackSweep == E_Sweep.Left)
            v2LocalMovePos = m_v2SweepPoints[(int)E_Sweep.Right];

        handAttackAnimation(E_AttackPattern.E_SWEEP);
        m_cSound.attackPlayOnce(2);
        m_attackTween = cGrip.transform.DOLocalMove(v2LocalMovePos, 1.0f).OnComplete(() =>
        {
            isControl = true;
            m_cGripWeapon.attackEventEnd();
            eBossState = E_BossState.E_ATTACK_SWEEP_AFTER;
        });


    }

    private void attackSweepAfterEvent()
    {
        m_cHandCollider.size = m_v2HandColliderSizeDefault;
        eBossState = E_BossState.E_ATTACK_RETURN;
    }


    private void atttackReturnEvent()
    {
        isControl = false;
        Utility.KillTween(m_attackTween);


        handIdleAnimation();

        m_attackTween = cGrip.transform.DOMove(m_v2GripReturnPoint, 1.0f).OnComplete(() => 
        {
            isControl = true;
           // m_cUnitHitConnnet.isHitAble = true;
            eBossState = E_BossState.E_WAIT;
        });

    }
    private void dieEvent()
    {
        isControl = false;

        m_cSound.deathPlayOnce();
        cGrip.gameObject.SetActive(false);

        m_imfect.SetActive(true);



        GameManager.instance.cUIManager.cUI_InGame.cUI_BossHp.toggle(false);

        m_srModel.sprite = m_BrokenSprite;

        GameManager.instance.eGameState = GameManager.E_GAMESTATE.E_CLEAR;
    }





    private void Update()
    {
        if (!isControl)
            return;

        m_delAI[(int)eBossState]();
    }

    private void handIdleAnimation()
    {
        m_cHandAnimator.SetTrigger("idle");
    }

    private void handAttackAnimation(E_AttackPattern ePattern)
    {
        m_cHandAnimator.SetFloat("fAttackType", m_fAttackPatterns[(int)ePattern]);
        m_cHandAnimator.SetTrigger("attack");
    }

    private float calaculatePatternTime(float fDefalutTime)
    {
        if (fDefalutTime <= .0f)
            return .0f;

        float fResult = fDefalutTime / m_fBerserkerSpeed;
        return fResult;
    }

    private void berserkerModeOn()
    {

        m_srGirpWeapon.color = Color.red;

        for (int i = 0; i < (int)E_AttackTimeState.E_TOTAL; i++)
        {
            m_fCurrentAttackRakeTimes[i] = calaculatePatternTime(m_fAttackRakeTimes[i]);
            m_fCurrentAttackChoppingTimes[i] = calaculatePatternTime(m_fAttackChoppingTimes[i]);
            m_fCurrentAttackSweepTimes[i] = calaculatePatternTime(m_fAttackSweepTimes[i]);
        }
    }


}
