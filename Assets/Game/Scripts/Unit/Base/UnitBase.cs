using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] protected Status m_cStatus;


    [SerializeField] protected SpriteRenderer m_srModel;

    [SerializeField]
    private UnitGrip m_cUnitGrip;


    private Vector2 m_v2MoveDir;
    private Vector2 m_v2LookDir;


    [SerializeField] protected Dash m_cDash;
    [SerializeField] protected UnitAniMation m_cAnimation;
    [SerializeField] protected UnitSpriteTweenImfect m_cImfect;

    private BoxCollider2D m_collider;

    protected bool m_bControl;
    protected bool m_bMoveAble;
    protected bool m_bLookAble;

    protected WeaponBase m_cGripWeapon;


    private IEnumerator m_ieHealingEventCoroutine;

    [SerializeField] private int m_nDefaultlayer;
    private int m_nGodLayer;

    private Vector2 m_v2NextLookDir;
    private Vector2 m_v2NextMoveDir;

    public Vector2 v2NextLookDir
    {
        set
        {
            if (value == Vector2.zero)
                return;

            m_v2NextLookDir = value.normalized;
        }
        get
        {
            return m_v2NextLookDir;
        }
    }

    public Vector2 v2NextMoveDir
    {
        set
        {
            m_v2NextMoveDir = value.normalized;
        }
        get
        {
            return m_v2NextMoveDir;
        }
    }

    public virtual void lookDirUpdate()
    {
        if (!isControl || !isLookAble) return;

        v2LookDir = v2NextLookDir;

        lookSprite(v2LookDir);
        m_cAnimation.updateDir(v2LookDir);
        cGrip.gripUpdate(v2LookDir);

    }

    public virtual void moveDirUpdate()
    {
        if (!isControl || !isMoveAble) return;

        v2MoveDir = v2NextMoveDir;
        v2Velocity = v2MoveDir * m_cStatus.fSpeed;
        m_cAnimation.updateMovement(m_v2MoveDir);
    }



    public virtual void init()
    {
        m_collider = GetComponent<BoxCollider2D>();


        m_cStatus.init();
        m_cDash.init(this, m_cStatus.fDushPower, m_cStatus.fDushTime);
        m_cAnimation.init();
        m_cImfect.init(m_srModel);

        m_nGodLayer = 10;
    }



    public virtual Vector2 v2Velocity
    {
        set
        {

        }
        get
        {
            return Vector2.zero;
        }
    }


    public virtual void attack()
    {
        if (fStamina >= m_cGripWeapon.cWeaponData.fStamina)
        {
            m_cGripWeapon.attack();
        }

    }

    public virtual void hit(UnitBase unit, WeaponAttackData cAttackData)
    {


        foreach (WeaponDamageData data in cAttackData.getWeaponDamageData())
        {
            if (data.eDamageType == WeaponDamageData.DamageType.E_NOMAL)
                nHP -= data.nDamge;
            if (data.eDamageType == WeaponDamageData.DamageType.E_STIFFEN)
                cStatus.nCurrentStiffness += data.nDamge;
        }



    }

    protected virtual void stiffness()
    {

    }



    public virtual void dieEvent()
    {
        print(gameObject.name + " is die");
    }


    public virtual bool isControl
    {
        set
        {
            m_bControl = value;

        }

        get
        {
            return m_bControl;
        }
    }

    public bool isMoveAble
    {
        set
        {
            m_bMoveAble = value;
            if (!m_bMoveAble)
                v2Velocity = Vector2.zero;
        }
        get
        {
            return m_bMoveAble;
        }
    }

    public bool isLookAble
    {
        set
        {
            m_bLookAble = value;

        }
        get
        {
            return m_bLookAble;
        }
    }



    public bool isDie
    {
        get
        {
            if (nHP <= 0) return true;
            return false;
        }
    }

    public Vector2 v2UnitPos
    {
        get
        {
            return (Vector2)transform.position;
        }
    }

    public virtual Vector2 v2LookDir
    {
        private set
        {
            m_v2LookDir = value;
        }
        get
        {
            return m_v2LookDir;
        }

    }
    public Vector2 v2MoveDir
    {
        private set
        {
            m_v2MoveDir = value;
        }
        get
        {
            return m_v2MoveDir;
        }
    }
    private void lookSprite(Vector2 v2LookDir)
    {
        m_srModel.flipX = false;
        if (v2LookDir.x < 0)
            m_srModel.flipX = true;
    }

    protected virtual int nStiffness
    {
        set
        {
            m_cStatus.nCurrentStiffness = value;
        }
        get
        {
            return m_cStatus.nCurrentStiffness;
        }
    }


    protected void knockBack(Vector2 v2Dir, float fPower, float fTime, bool bEndEvent)
    {
        m_cDash.dushDetail(v2Dir, fPower, fTime, bEndEvent);
    }




    public void dushDetail(Vector2 v2Dir, float fPower, float fDushTime, bool bEndEvent)
    {
        m_cDash.dushDetail(v2Dir, fPower, fDushTime, bEndEvent);
    }

    protected void dash(Vector2 v2Dir, bool bEndEvent)
    {
        m_cDash.dush(v2Dir, bEndEvent);
    }


    public void dushStop()
    {
        m_cDash.dushStop();
    }



    public BoxCollider2D collider
    {
        get
        {
            return m_collider;
        }
    }

    public WeaponBase cGripWeapon
    {
        set
        {
            m_cGripWeapon = value;
        }
        get
        {
            return m_cGripWeapon;
        }
    }

    public UnitAniMation cAnimation
    {
        get
        {
            return m_cAnimation;
        }
    }



    public virtual float fStamina
    {
        set
        {
            cStatus.fStamina = value;
            cStatus.fStamina = Mathf.Clamp(fStamina, 0.0f, cStatus.fMaxStamina);

            if (cStatus.fStamina < cStatus.fMaxStamina)
            {
                staminaHeilingEventStart();
            }

            //Debug.Log("S: " + fStamina + "MAX S: " + cStatus.fMaxStamina);

        }
        get
        {
            return cStatus.fStamina;
        }
    }

    private void staminaHeilingEventStart()
    {
        if (m_ieHealingEventCoroutine != null)
            return;

        m_ieHealingEventCoroutine = healingEventCoroutine();
        StartCoroutine(m_ieHealingEventCoroutine);
    }


    private IEnumerator healingEventCoroutine()
    {
        while (fStamina < cStatus.fMaxStamina)
        {
            yield return new WaitForSeconds(cStatus.fStatminaHealingTickTime);
            fStamina += cStatus.fStatminaHealingTick;
        }

        m_ieHealingEventCoroutine = null;
    }

    protected Status cStatus
    {
        get
        {
            return m_cStatus;
        }
    }

    public void godMode()
    {
        gameObject.layer = m_nGodLayer;
        m_cImfect.godImfect();


        Invoke("godModeEnd", cStatus.fGodTime);
    }

    protected void godModeEnd()
    {
        m_cImfect.stop();
        gameObject.layer = m_nDefaultlayer;
    }

    public UnitGrip cGrip
    {
        get
        {
            return m_cUnitGrip;
        }
    }

    public void movementUpdate()
    {
        moveDirUpdate();

        lookDirUpdate();

    }

    public virtual int nHP
    {
        set
        {
            m_cStatus.nHp = value;
        }
        get
        {
            return m_cStatus.nHp;
        }
    }



}
