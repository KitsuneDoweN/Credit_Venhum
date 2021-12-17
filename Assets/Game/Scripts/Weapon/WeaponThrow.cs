using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : WeaponBase
{
    [SerializeField] private GameObject m_goThrow;
    [SerializeField] private Transform m_trFirePoint;
    [SerializeField] private float m_fThrowPower;
    [SerializeField] private LineRenderer m_aimmingLine;


    [SerializeField]
    private bool m_bDrawHitLineEvent;

    private bool m_bDrawHitLine;

    private bool isDrawHitLine
    {
        set
        {
            m_bDrawHitLine = value;
        }
        get
        {
            if (!m_bDrawHitLineEvent)
                return false;
            return m_bDrawHitLine;
        }
    }


    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);
        isDrawHitLine = false;
    }

    public override void attackEventStart()
    {
        base.attackEventStart();

        Vector2 v2FireDir = cUnit.v2LookDir;

        float fRotaionAngle = Utility.getHorizontalAtBetweenAngle(v2FireDir);

        GameObject goThrow = Instantiate(m_goThrow);
        
        goThrow.transform.position = m_trFirePoint.position;
        goThrow.transform.rotation = Quaternion.Euler(.0f,.0f,fRotaionAngle);
        

        goThrow.GetComponent<Throw>().init(this, v2FireDir, m_fThrowPower);
        goThrow.GetComponent<Throw>().shoot();

        isDrawHitLine = false;
    }


    public override void attack()
    {
        if (cCoolTime.isCoolTime)
            return;
        if (isAttackRun)
            return;

        cUnit.fStamina -= cWeaponData.fStamina;

        cUnit.isMoveAble = false;

        attackAnimation();

        isAttackRun = true;

        cCoolTime.startCoolTime(cWeaponData.fCoolTime);


        isDrawHitLine = true;
    }

    protected override void attackAnimation()
    {
        cUnit.cAnimation.attack(strAttackTrigger, cComboSystem.nCurrentCombo);
    }

    public override void attackEnd()
    {
        base.attackEnd();

        cUnit.isMoveAble = true;

    }

    public override void reset()
    {
        base.reset();
    }


    public override void attackEventEnd()
    {
        base.attackEventEnd();
    }

    public  override void attackDrawHit(Vector2 v2Dir, bool isDraw)
    {
        if (isDrawHitLine && isDraw)
        {
            m_aimmingLine.SetPosition(0, (Vector2)transform.position);
            m_aimmingLine.SetPosition(1, (Vector2)transform.position + (v2Dir * 10.0f));
        }
        else
        {
            m_aimmingLine.SetPosition(0, Vector2.zero);
            m_aimmingLine.SetPosition(1, Vector2.zero);
        }
    }


}
