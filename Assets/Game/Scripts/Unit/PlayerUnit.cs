using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerUnit : UnitBase
{

    [SerializeField]
    private PlayerWeapons m_cWeapons;
    
    [SerializeField]
    private Rigidbody2D m_rigidbody2D;

    public Rigidbody2D rig2D
    {
        get
        {
            return m_rigidbody2D;
        }
    }

    public override Vector2 v2Velocity
    {
        set
        {
            rig2D.velocity = value;
        }
        get
        {
            return rig2D.velocity;
        }
    }


    public override void init()
    {
        base.init();


        m_cWeapons.init(this);

        switchWeapon(PlayerWeapons.E_Weapon.E_SWORD);
        cGrip.init(cGripWeapon.cWeaponData.fRange);


        isControl = true;
        isMoveAble = true;
        isLookAble = true;


        //hit(this, cGripWeapon.cWeaponData.getWeaponAttackData(0));
    }


    public override bool isControl
    {
        set 
        { 
            base.isControl = value;

        }
        get
        {
            return base.isControl;
        }
    }





    public override void moveDirUpdate()
    {
        base.moveDirUpdate();


    }

    public override void lookDirUpdate()
    {
        base.lookDirUpdate();
    }


    public override void hit(UnitBase unit, WeaponAttackData cAttackData)
    {
        base.hit(unit, cAttackData);

        Debug.Log("hit");

        isControl = false;


        cAnimation.hit();

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
        movementUpdate();
    }

    public void dushAction()
    {
        if (!isControl)
            return;

        if (fStamina < cStatus.fDushStamina)
            return;

        fStamina -= cStatus.fDushStamina;

        isLookAble = true;

        lookDirUpdate();
        
        dush(v2LookDir, true);
    }



    public bool switchWeapon(PlayerWeapons.E_Weapon eWeapon)
    {
        if (!m_cWeapons.switchWeapon(eWeapon, ref m_cGripWeapon))
            return false ;

        cGripWeapon.transform.parent = cGrip.transform;
        cGripWeapon.transform.localPosition = Vector3.zero;

        cGripWeapon.reset();

        cAnimation.setWeaponHandle(m_cGripWeapon);

        cGrip.gripSetting(cGripWeapon.cWeaponData.fRange);
        cGrip.gripUpdate(v2LookDir);

        return true;
    }

    public PlayerWeapons.E_Weapon eGripWeapon
    {
        get
        {
            return m_cWeapons.eGripWeapon;
        }
    }

    

}
