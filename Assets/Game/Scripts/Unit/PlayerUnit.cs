using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerUnit : UnitBase
{

    [SerializeField]
    private PlayerWeapons m_cWeapons;






    public override void init()
    {
        base.init();


        m_cWeapons.init(this);

        switchWeapon(PlayerWeapons.E_Weapon.E_SWORD);
        cGrip.init(cGripWeapon.cWeaponData.fRange);


        isControl = true;
        isMoveAble = true;
        isLookAble = true;


        hit(this, cGripWeapon.cWeaponData.getWeaponAttackData(0));
    }


    public override bool isControl
    {
        set 
        { 
            base.isControl = value;

            v2MoveDir = v2OldMoveDir;
            v2LookDir = v2OldLookDir;

        }
        get
        {
            return base.isControl;
        }
    }


    private void moveUpdate()
    {

        if (!isControl || !isMoveAble) return;

        v2Velocity = v2MoveDir * m_cStatus.fSpeed;



    }


    public override void hit(UnitBase unit, WeaponAttackData cAttackData)
    {
        base.hit(unit, cAttackData);

        m_cAnimation.trigger("Hit");

        m_cImfect.hitimfect();
        m_cImfect.godImfect();


    }


    public override void attack()
    {
        base.attack();
    }

    public override void die()
    {
        base.die();
    }

    private void Update()
    {
        moveUpdate();
    }

    public void dushAction()
    {
        if (!isControl)
            return;

        if (fStamina < cStatus.fDushStamina)
            return;

        fStamina -= cStatus.fDushStamina;


        isMoveAble = false;
        dush(v2LookDir, true);
    }

    public void refeshMove()
    {
        isMoveAble = true;
        moveDirUpdate(v2MoveDir);
    }

    public void switchWeapon(PlayerWeapons.E_Weapon eWeapon)
    {
        if (!m_cWeapons.switchWeapon(eWeapon,  ref m_cGripWeapon))
            return;

        cGripWeapon.transform.parent = cGrip.transform;
        cGripWeapon.transform.localPosition = Vector3.zero;

        cGripWeapon.reset();

        cAnimation.setWeaponHandle(m_cGripWeapon);

        cGrip.gripSetting(cGripWeapon.cWeaponData.fRange);
        cGrip.gripUpdate(v2LookDir);
    }

    public PlayerWeapons.E_Weapon eGripWeapon
    {
        get
        {
            return m_cWeapons.eGripWeapon;
        }
    }

    

}
