using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossBuleWitch : UnitBossBase, IUpdate
{
    private enum E_BossState
    {
        E_NONE,
        E_WAIT,
        E_ATTACK_CHOICE,

        E_ATTACK_TP,
        E_ATTACK_TP_AFTER,

        E_ATTACK_ENEMYSPAWN,
        E_HELLING,

        E_HITTIME,

        E_DIE,
        E_TOTAL
    }
    [SerializeField]
    private E_BossState m_eBossState;
    private E_BossState eBossState
    {
        set
        {
            m_fCurrentTime = .0f;
            m_eBossState = value;

            if (m_eBossState == E_BossState.E_WAIT)
            {
                tpPoint(getRandomPillarPoint());
                cAnimation.trigger("idle");
            }
            else if (m_eBossState == E_BossState.E_ATTACK_ENEMYSPAWN)
            {
                tpPoint(getRandomPillarPoint());
                cAnimation.trigger("charge");
                setSpawnEnemys();
            }
            else if (m_eBossState == E_BossState.E_HITTIME)
            {
                tpPoint(m_cHitTimeRandomPoint.getRandomPoint());
                cAnimation.trigger("idle");
            }



        }
        get
        {
            return m_eBossState;
        }
    }

    private delegate void BossEvent();
    private BossEvent[] m_delAI;

    private float m_fWaitTime = 1.0f;
    private float m_fHitEventTime = 2.0f;

    private float m_fCurrentTime;

    private int m_nTPCurrentCount;

    [SerializeField]
    private int m_nTPCount;

    private float m_fHellingTime = 10.0f;
    private float m_fHellingTick = 1.0f;
    private int m_nfHelling = 1;


    private Vector2 m_v2TpAddPoint = new Vector2(1.0f, 0.0f);


    [SerializeField]
    private NoWavePushTypeSpawnManager m_cSpawnManager;

    [SerializeField]
    private SpawnDataComponent m_cSpawnDataComponent;

    [SerializeField]
    private Transform m_pillarPointsParent;

    private Transform[] m_pillarPoints;

    [SerializeField]
    private RandomPoint m_cHitTimeRandomPoint;

    [SerializeField]
    private UnitSound m_cSound;

    public override void init()
    {
        base.init();

        m_cHitTimeRandomPoint.init();


        m_cGripWeapon = cGrip.GetComponentInChildren<WeaponBase>();
        m_cGripWeapon.init(this);

        cGrip.init(cGripWeapon.cWeaponData.fGripRange);


        eventSetting();

        pillarPointsSetting();

        UpdateManager.Instance.addProcesses(this);

    }





    private void pillarPointsSetting()
    {
        m_pillarPoints = new Transform[m_pillarPointsParent.childCount];
        for (int i = 0; i < m_pillarPoints.Length; i++)
        {
            m_pillarPoints[i] = m_pillarPointsParent.GetChild(i);
        }
    }

    private void eventSetting()
    {
        m_delAI = new BossEvent[(int)E_BossState.E_TOTAL];


        m_delAI[(int)E_BossState.E_NONE] = noneEvent;
        m_delAI[(int)E_BossState.E_WAIT] = waitEvent;
        m_delAI[(int)E_BossState.E_ATTACK_CHOICE] = attackChoiceEvent;
        m_delAI[(int)E_BossState.E_ATTACK_ENEMYSPAWN] = attackEnemySpawnEvent;
        m_delAI[(int)E_BossState.E_ATTACK_TP] = attackTpEvent;
        m_delAI[(int)E_BossState.E_ATTACK_TP_AFTER] = attackTpAfterEvent;

        m_delAI[(int)E_BossState.E_HELLING] = hellingEvent;
        m_delAI[(int)E_BossState.E_HITTIME] = hitTimeEvent;
        m_delAI[(int)E_BossState.E_DIE] = dieEvent;

        eBossState = E_BossState.E_NONE;
    }


    public string id
    {
        get
        {
            return gameObject.name;
        }
    }

    public void updateProcesses()
    {
        if (!isControl)
            return;

        m_fCurrentTime += Time.deltaTime;

        m_delAI[(int)eBossState]();
    }

    private void noneEvent()
    {

    }

    private void waitEvent()
    {

        if (m_fCurrentTime >= m_fWaitTime)
            eBossState = E_BossState.E_ATTACK_CHOICE;
    }

    private void attackChoiceEvent()
    {
        bool bAttackTP = true;

        if (getRandom(0, 10) < 2)
            bAttackTP = false;


        if (bAttackTP)
            eBossState = E_BossState.E_ATTACK_TP;
        else
            eBossState = E_BossState.E_ATTACK_ENEMYSPAWN;
    }

    private void attackTpEvent()
    {
        bool bRightAttack = true;
        m_nTPCurrentCount++;

        if (getRandom(0, 1) == 0)
            bRightAttack = false;

        Vector2 v2TpPoint = m_cPlayer.v2UnitPos;

        Vector2 v2TpAddPos = m_v2TpAddPoint;

        if (!bRightAttack)
            v2TpAddPos *= -1.0f;

        v2TpPoint += v2TpAddPos;

        tpPoint(v2TpPoint);

        attack();

        eBossState = E_BossState.E_ATTACK_TP_AFTER;
    }

    private void attackTpAfterEvent()
    {
        if (cGripWeapon.isAttackRun)
            return;


        if (m_nTPCurrentCount == m_nTPCount)
        {
            m_nTPCurrentCount = 0;
            eBossState = E_BossState.E_HITTIME;
        }
        else
        {
            
            eBossState = E_BossState.E_ATTACK_TP;
        }
    }


    private void attackEnemySpawnEvent()
    {

        if (m_cSpawnManager.isSpawnerAllClear)
            eBossState = E_BossState.E_HITTIME;


        if (m_fCurrentTime >= m_fHellingTime)
        {
            eBossState = E_BossState.E_HELLING;
        }
    }

    private void hellingEvent()
    {

        if (m_cSpawnManager.isSpawnerAllClear)
            eBossState = E_BossState.E_WAIT;

        if (m_fCurrentTime >= m_fHellingTick)
        {
            m_cImfect.hellingImfect();
            nHP += m_nfHelling;
            m_fCurrentTime = .0f;
        }

        if (nHP == cStatus.nMaxHp)
        {
            eBossState = E_BossState.E_WAIT;
        }

    }

    private void dieEvent()
    {
        isControl = false;
        m_cAnimation.die();
        m_cdeathProducion.productionAction();
    }

    private void hitTimeEvent()
    {
        if (m_fCurrentTime >= m_fHitEventTime)
        {
            eBossState = E_BossState.E_WAIT;
        }
    }

    private void tpPoint(Vector2 v2Point)
    {
        //1 == tp sound
        m_cSound.attackPlayOnce(1);

        v2UnitPos = v2Point;

        
        float fLookAtTargetDirX = m_cPlayer.v2UnitPos.x - v2UnitPos.x;

        if (fLookAtTargetDirX >= .0f)
            v2NextLookDir = Vector2.right;
        else
            v2NextLookDir = Vector2.left;



        lookDirUpdate();


    }



    public override void hit(UnitBase unit, WeaponAttackData cAttackData)
    {

        base.hit(unit, cAttackData);
        //피해 사운드 추가

        GameManager.Instance.cUIManager.cUI_InGame.cUI_BossHp.draw(m_bossIcon, nHP, m_cStatus.nMaxHp, m_cStatus.strName);

        if (isDie)
        {
            eBossState = E_BossState.E_DIE;
            return;
        }


        if (eBossState == E_BossState.E_HITTIME)
            cAnimation.hit();

    }

    private Vector2 getRandomPillarPoint()
    {

        int nIndex = getRandom(0, m_pillarPoints.Length - 1);


        return (Vector2)m_pillarPoints[nIndex].position;
    }

    private void setSpawnEnemys()
    {
        int nRandomCount = getRandom(1, 2);
        int nRandomIndex;

        int i = 0;

        List<SpawnData> dataList = new List<SpawnData>();

        SpawnEntity entity = m_cSpawnDataComponent.cSpawnData.cSapwnEntity;



        while (i < nRandomCount)
        {
            nRandomIndex = getRandom(0, 8);
            SpawnData cData = new SpawnData();
            cData.init(entity, nRandomIndex, 0);
            dataList.Add(cData);

            i++;
        }

        m_cSpawnManager.pushSpawnData(dataList);

    }

    private int getRandom(int nMin, int nMax)
    {
        return (int)Random.RandomRange(nMin, nMax);
    }

    public override void handleWakeUp(UnitBase cPlayer)
    {
        base.handleWakeUp(cPlayer);
        eBossState = E_BossState.E_WAIT;
    }


    private void OnDestroy()
    {
        UpdateManager.Instance.removeProcesses(this);
    }


    public override int nHP
    {
        get => base.nHP;
        set
        {
            base.nHP = value;
            GameManager.Instance.cUIManager.cUI_InGame.cUI_BossHp.draw(m_bossIcon, nHP, m_cStatus.nMaxHp, m_cStatus.strName);
        }
    }

}
