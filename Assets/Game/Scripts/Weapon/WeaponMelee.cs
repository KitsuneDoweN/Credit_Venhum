using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMelee : WeaponBase
{

    [SerializeField] private int m_nNotAttackLayer;
    [SerializeField] private int m_nAttackLayer;
    [SerializeField] private float m_fMovePower;
    [SerializeField] private float m_fAttackActionTime;

    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);

        gameObject.layer = m_nNotAttackLayer;
    }

    public override void attack()
    {


        if (isAttackRun)
        {
            cComboSystem.combo();
            return ;
        }
        


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

    public override void attackEnd()
    {
        isAttackRun = false;
        cUnit.isLookAble = true;

        cUnit.lookDirUpdate();

        cComboSystem.comboAbleEnd();

        if (!cComboSystem.comboChack())
        {
            reset();
            return;
        }


        attack();
    }

    public override void attackAction()
    {
        base.attackAction();
        cUnit.dushDetail(cUnit.v2LookDir, m_fMovePower, m_fAttackActionTime, false);
    }




    public void OnTriggerEnter2D(Collider2D collision)
    {
        int nTargetMask = 1 << collision.gameObject.layer;

        if((nTargetMask & maskTarget) != 0)
        {
            if(collision.tag == "HitConnet")
            {
                collision.GetComponent<UnitHitConnet>().hit(cUnit, cWeaponData.getWeaponAttackData(cComboSystem.nOldCombo));
            }
            else
            {
                collision.GetComponent<UnitBase>().hit(cUnit, cWeaponData.getWeaponAttackData(cComboSystem.nOldCombo));
                Debug.Log("HIT  " + cComboSystem.nCurrentCombo);
            }
            
        }
    }


    public override void reset()
    {
        base.reset();

        gameObject.layer = m_nNotAttackLayer;
        cUnit.dushStop();

        cComboSystem.reset();

        cUnit.isMoveAble = true;
        cUnit.isLookAble = true;
    }







}
