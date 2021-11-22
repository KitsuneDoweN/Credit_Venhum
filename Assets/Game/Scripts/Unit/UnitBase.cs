using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] protected Status m_cStatus;

    [SerializeField] protected Rigidbody2D m_rigidbody2D;
    [SerializeField] protected SpriteRenderer m_srModel;


    private Vector2 m_v2MoveDir;
    private Vector2 m_v2LookDir;

    private Vector2 m_v2OldLookDir;
    private Vector2 m_v2OldMoveDir;

    [SerializeField] protected Dush m_cDush;
    [SerializeField] protected UnitAniMation m_cAnimation;
    [SerializeField] protected UnitHitSpriteTweenImfect m_cImfect;

    private BoxCollider2D m_collider;

    protected bool m_bControl;
    protected bool m_bMoveAble;
    protected bool m_bLookAble;

    protected WeaponBase m_cGripWeapon;




     public virtual void init()
    {
        m_collider = GetComponent<BoxCollider2D>();


        m_cStatus.init();
        m_cDush.init(this, m_cStatus.fDushPower, m_cStatus.fDushTime);
        m_cAnimation.init();
        m_cImfect.init(m_srModel);


    }



    public Vector2 v2Velocity
    {
        set
        {
            m_rigidbody2D.velocity = value;
        }
        get
        {
            return m_rigidbody2D.velocity;
        }
    }


    public virtual void attack() 
    {
        m_cGripWeapon.attack();
    }

    public virtual void hit(UnitBase unit, WeaponAttackData cWeaponDamage) 
    {

    }


    public virtual void die() 
    {
        print(gameObject.name + " is die");
        Destroy(gameObject);
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

    public Rigidbody2D rig2D
    {
        get
        {
            return m_rigidbody2D;
        }
    }

    public bool isDie
    {
        get
        {
            if (m_cStatus.nHp <= 0) return true;
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
        set
        {
            m_v2LookDir = value.normalized;
            lookSprite(m_v2LookDir);
            m_cAnimation.updateDir(v2LookDir);
        }
        get
        {
            return m_v2LookDir;
        }

    }
    public  Vector2 v2MoveDir
    {
        set
        {
            m_v2MoveDir = value.normalized;
            m_cAnimation.updateMovement(m_v2MoveDir);
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

   


    protected void knockBack(Vector2 v2Dir, float fPower,float fTime, bool bEndEvent)
    {
        m_cDush.dushDetail(v2Dir, fPower, fTime, bEndEvent);
    }



    
    public void dushDetail(Vector2 v2Dir, float fPower, float fDushTime, bool bEndEvent)
    {
        m_cDush.dushDetail(v2Dir, fPower, fDushTime, bEndEvent);
    }

    protected void dush(Vector2 v2Dir, bool bEndEvent)
    {
        m_cDush.dush(v2Dir, bEndEvent);
    }
    

    public void dushStop()
    {
        m_cDush.dushStop();
    }

    public void moveDirUpdate(Vector2 dir)
    {
        v2OldMoveDir = dir;

        if (!isControl || !isMoveAble) return;

        v2MoveDir = v2OldMoveDir;
    }

    public void lookDirUpdate(Vector2 dir)
    {

        if (dir != Vector2.zero)
            v2OldLookDir = dir;

        if (!isControl || !isLookAble || dir == Vector2.zero || v2LookDir == dir)
            return;

        v2LookDir = v2OldLookDir;


        Debug.Log("LOOK " + v2LookDir + " Old" + v2OldLookDir);
    }


    public BoxCollider2D collider
    {
        get
        {
            return m_collider;
        }
    }

    protected WeaponBase cGripWeapon
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

    public Vector2 v2OldLookDir
    {
        set
        {
            m_v2OldLookDir = value;
        }
        get
        {
            return m_v2OldLookDir;
        }
    }

    public Vector2 v2OldMoveDir
    {
        set
        {
            m_v2OldMoveDir = value;
        }
        get
        {
            return m_v2OldMoveDir;
        }
    }

    

}
