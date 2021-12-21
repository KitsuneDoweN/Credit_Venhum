using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
public class PlayerUnit : UnitBase , IUpdate
{

    [SerializeField]
    private PlayerWeapons m_cWeapons;
    
    [SerializeField]
    private Rigidbody2D m_rigidbody2D;

    [SerializeField]
    private UnitSound m_cSound;

    public Rigidbody2D rig2D
    {
        get
        {
            return m_rigidbody2D;
        }
    }

    public override int nHP
    {
        set
        {
            base.nHP = value;

            float fFillAmount = (float)nHP / (float)m_cStatus.nMaxHp ;
            GameManager.Instance.cUIManager.cUI_InGame.cUI_PlayerInfo.draw(UI_PlayerInfo.E_INFO.E_HP, fFillAmount);
        }
        get
        {
            return base.nHP;
        }
    }

    public override float fStamina
    {
        set
        {
            base.fStamina = value;
            float fFillAmount = fStamina / m_cStatus.fMaxStamina;
            Debug.Log(fStamina + "  " + m_cStatus.fMaxStamina);
            GameManager.Instance.cUIManager.cUI_InGame.cUI_PlayerInfo.draw(UI_PlayerInfo.E_INFO.E_STATMINA, fFillAmount);
        }
        get
        {
            return base.fStamina; 
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
        cGrip.init(cGripWeapon.cWeaponData.fGripRange);

        nHP = nHP;


        isControl = true;
        isMoveAble = true;
        isLookAble = true;

        UpdateManager.Instance.addProcesses(this);
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
        if (isDie) 
            return;

        base.hit(unit, cAttackData);

        Debug.Log("hit");


        if (!isDie)
        {
            cGripWeapon.reset();
            stop();

            if (unit.cGripWeapon.cWeaponData.eType == WeaponData.E_Type.E_ALMOST)
            {
                m_cSound.hitPlayOnceIndex(0.0f);
            }

            if (unit.cGripWeapon.cWeaponData.eType == WeaponData.E_Type.E_TROW)
            {
                m_cSound.hitPlayOnceIndex(1.0f);
            }


            cAnimation.hit();

        }
        else
        {
            dieEvent();
        }
        



    }


    public override void attack()
    {
        base.attack();
    }

    public override void dieEvent()
    {
        base.dieEvent();
        stop();
        isControl = false;
        cAnimation.die();
        Debug.Log("Die");
        UpdateManager.Instance.removeProcesses(this);
    }



    public void dashAction()
    {
        if (!isControl)
            return;

        if (fStamina < cStatus.fDashStamina)
            return;

        fStamina -= cStatus.fDashStamina;

        isLookAble = true;

        lookDirUpdate();


        godMode();

        dash(v2LookDir, true);
    }

    public void dashActionEndEvent()
    {
        godModeEnd();
    }





    public bool switchWeapon(PlayerWeapons.E_Weapon eWeapon)
    {
        if (!m_cWeapons.switchWeapon(eWeapon, ref m_cGripWeapon))
            return false ;

        cGripWeapon.transform.parent = cGrip.transform;
        cGripWeapon.transform.localPosition = Vector3.zero;

        cGripWeapon.reset();

        cAnimation.setWeaponHandle(m_cGripWeapon);

        cGrip.gripSetting(cGripWeapon.cWeaponData.fGripRange);
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

    public string id
    {
        get
        {
            return gameObject.name;
        }
    }

    public void stop()
    {
        v2NextMoveDir = Vector2.zero;
        movementUpdate();
    }

    public void updateProcesses()
    {
        if (!isControl)
            return;

        movementUpdate();
    }
}
