using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : WeaponBase
{
    [SerializeField] private GameObject m_goThrow;
    [SerializeField] private Transform m_trFirePoint;
    [SerializeField] private float m_fThrowPower;

    public override void init(UnitBase unitBase)
    {
        base.init(unitBase);
        strAttackTrigger = "attackThrow";
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

}
