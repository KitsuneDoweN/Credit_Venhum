using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPlayerSworld : WeaponBase
{
    [SerializeField] private int m_nNotAttackLayer;
    [SerializeField] private int m_nAttackLayer;

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
         

        cUnit.cAnimation.attack(strAttackTrigger, m_cComboSystem.nCurrentCombo);
        isAttackRun = true;
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
            cUnit.isMoveAble = true;
            return;
        }

        attack();
    }

    public override void attackImfect()
    {
        base.attackImfect();
    }




    public void OnTriggerEnter2D(Collider2D collision)
    {
        int nTargetMask = 1 << collision.gameObject.layer;

        if((nTargetMask & maskTarget) != 0)
        {
            collision.GetComponent<UnitBase>().hit(cUnit, cWeaponData.getWeaponAttackData(m_cComboSystem.nOldCombo));
        }
    }









}
