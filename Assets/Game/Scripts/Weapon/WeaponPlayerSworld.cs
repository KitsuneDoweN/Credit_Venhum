using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerSworld : WeaponBase
{
    [SerializeField] private int m_nNotAttackLayer;
    [SerializeField] private int m_nAttackLayer;
    [SerializeField] private float m_fMovePower;

    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);
        gameObject.layer = m_nNotAttackLayer;
        strAttackTrigger = "attackSword";
    }

    public override void attack()
    {

        if (isCoolTime)
            return;


        if (isAttackRun)
        {
            m_cComboSystem.combo();
            return;
        }
        


        cUnit.fStamina -= cWeaponData.fStamina;

        cUnit.isMoveAble = false;
        cUnit.isLookAble = false;


        attackAnimation();

        isAttackRun = true;
    }
    protected override void attackAnimation()
    {
        cUnit.cAnimation.attack(strAttackTrigger, m_cComboSystem.nCurrentCombo);
    }


    public override void attackEventStart()
    {
        base.attackEventStart();

        gameObject.layer = m_nAttackLayer;
    }

    public override void attackEventEnd()
    {
        base.attackEventEnd();
        gameObject.layer = m_nNotAttackLayer;
    }

    public override void attackEnd()
    {
        base.attackEnd();

        cUnit.lookDirUpdate();

        m_cComboSystem.comboAbleEnd();

        if (!m_cComboSystem.comboChack())
        {
            reset();
            cUnit.isMoveAble = true;
            return;
        }


        attack();
    }

    public override void attackImfect()
    {
        base.attackImfect();
        cUnit.dushDetail(cUnit.v2LookDir, m_fMovePower, 0.1f, false);
    }

    


    public void OnTriggerEnter2D(Collider2D collision)
    {
        int nTargetMask = 1 << collision.gameObject.layer;

        if((nTargetMask & maskTarget) != 0)
        {
            if(collision.tag == "HitConnet")
            {
                collision.GetComponent<UnitHitConnet>().hit(cUnit, cWeaponData.getWeaponAttackData(m_cComboSystem.nOldCombo));
            }
            else
            {
                collision.GetComponent<UnitBase>().hit(cUnit, cWeaponData.getWeaponAttackData(m_cComboSystem.nOldCombo));
            }
            
        }
    }


    public override void reset()
    {
        base.reset();

        gameObject.layer = m_nNotAttackLayer;
        cUnit.dushStop();

        m_cComboSystem.reset();

    }







}
