using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerUnit : UnitBase
{
    [SerializeField]
    private UnitGrip m_cUnitGrip;
    [SerializeField]
    private PlayerWeapons m_cWeapons;
    
    


    public override Vector2 v2LookDir
    {

        set
        {
            base.v2LookDir = value;
            m_cUnitGrip.gripUpdate(v2LookDir);
        }
        get
        {
            return base.v2LookDir;
        }
    }


    public override void init()
    {
        base.init();

        m_cUnitGrip.init(Vector2.up * 1.45f, 1.0f);
        m_cWeapons.init(this);

        isControl = true;
        isMoveAble = true;
        isLookAble = true;

        switchWeapon(PlayerWeapons.E_Weapon.E_SWORD);
    }

    public void moveDirUpdate(Vector2 dir)
    {
        v2OldMoveDir = dir;

        if (!isControl || !isMoveAble) return;

        v2MoveDir = v2OldMoveDir;
    }

    public void lookDirUpdate(Vector2 dir)
    {
       

        v2OldLookDir = dir;

        if (!isControl || !isLookAble ||dir == Vector2.zero || v2LookDir == dir) 
            return;

        v2LookDir = v2OldLookDir;
    }



    private void moveUpdate()
    {

        if (!isControl || !isMoveAble) return;

        v2Velocity = v2MoveDir * m_cStatus.fSpeed;



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

        if (m_cStatus.fStamina < m_cStatus.fDushStamina)
            return;

        m_cStatus.fStamina -= m_cStatus.fDushStamina;


        isMoveAble = false;
        dush(v2LookDir);
    }

    public void refeshMove()
    {
        isMoveAble = true;
        moveDirUpdate(v2MoveDir);
    }

    public void switchWeapon(PlayerWeapons.E_Weapon eWeapon)
    {
        m_cWeapons.switchWeapon(eWeapon, ref m_cGripWeapon);

        m_cGripWeapon.transform.parent = m_cUnitGrip.transform;
        m_cGripWeapon.transform.localPosition = Vector3.zero;

        m_cGripWeapon.reset();

        m_cAnimation.setWeaponHandle(m_cGripWeapon);

        m_cUnitGrip.gripUpdate(v2LookDir);
    }

    public PlayerWeapons.E_Weapon eGripWeapon
    {
        get
        {
            return m_cWeapons.eGripWeapon;
        }
    }

}
