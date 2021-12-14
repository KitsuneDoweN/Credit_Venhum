using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemySworld : WeaponBase
{
    [SerializeField] private int m_nNotAttackLayer;
    [SerializeField] private int m_nAttackLayer;



    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);
        gameObject.layer = m_nNotAttackLayer;
        strAttackTrigger = "attack";
    }

    public override void attack()
    {

        cUnit.fStamina -= cWeaponData.fStamina;

        cUnit.isMoveAble = false;
        cUnit.isLookAble = false;

        attackAnimation();

        isAttackRun = true;
    }

    protected override void attackAnimation()
    {
        cUnit.cAnimation.attack(strAttackTrigger, cComboSystem.nCurrentCombo);
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



    public override void attackAction()
    {
        base.attackAction();
    }

    public override void attackEnd()
    {
        base.attackEnd();


    }

    public override void reset()
    {
        base.reset();
        gameObject.layer = m_nNotAttackLayer;
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        int nTargetMask = 1 << collision.gameObject.layer;

        if((nTargetMask & maskTarget) != 0)
        {
            collision.GetComponent<UnitBase>().hit(cUnit, cWeaponData.getWeaponAttackData(0));
        }
    }









}
