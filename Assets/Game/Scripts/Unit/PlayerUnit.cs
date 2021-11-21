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

    //private PlayerW


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

        isControl = true;
        isMoveAble = true;


        switchWeapon(PlayerWeapons.E_Weapon.E_SWORD);
    }

    public override void move(Vector2 v2Dir)
    {

        if (v2Dir != Vector2.zero && v2LookDir != v2Dir)
            v2LookDir = v2Dir;

        v2MoveDir = v2Dir;


    }

    private void moveUpdate()
    {
        if (!isControl || !isMoveAble) return;

        v2Velocity = v2MoveDir * m_cStatus.fSpeed;

        Debug.Log(v2LookDir + "  " + v2MoveDir);

        m_cAnimation.updateMovement(v2LookDir, v2MoveDir);
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
        move(v2MoveDir);
    }

    public void switchWeapon(PlayerWeapons.E_Weapon eWeapon)
    {
        m_cWeapons.switchWeapon(eWeapon, ref m_cGripWeapon);

        m_cGripWeapon.transform.parent = m_cUnitGrip.transform;
        m_cGripWeapon.transform.localPosition = Vector3.zero;

        m_cUnitGrip.gripUpdate(v2LookDir);
    }


}
