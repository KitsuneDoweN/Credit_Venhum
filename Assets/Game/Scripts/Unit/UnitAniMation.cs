using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAniMation : MonoBehaviour
{
    [SerializeField] private Animator m_animator;
    [SerializeField] private float m_fMovement;
    [SerializeField] private float m_fDir;
    [SerializeField] private WeaponAnimationHandle m_cWeaponAnimationHandle;




    public void init()
    {
       
    }

    public float fMovement
    {
        set
        {
            m_fMovement = value;
            m_animator.SetFloat("fMovement", m_fMovement);
            
        }
        
    }

    public float fDir
    {
        set
        {
            m_fDir = value;
            m_animator.SetFloat("fDir", m_fDir);
        }
    }



    public void hit()
    {
        trigger("hit");
    }


    public void setAnimationSpeed(float fSpeed)
    {
        m_animator.speed = fSpeed;
    }

    public void updateMovement(Vector2 v2MoveDir)
    {
        fMovement = 0.0f;
        if (v2MoveDir.sqrMagnitude > 0.1f)
            fMovement = 1.0f;

    }

    public void updateDir(Vector2 v2LookDir)
    {

        fDir = 0.0f;

        if (v2LookDir.y < 0.0f)
            fDir = 1.0f;

        if (v2LookDir.x > 0.0f || v2LookDir.x < 0.0f)
            fDir = 0.5f;
    }

    public void attack(string attackTrigger,int nIndex)
    {
        m_animator.SetInteger("nCombo", nIndex);
        trigger(attackTrigger);
    }

    public void setWeaponHandle(WeaponBase weapon)
    {
        m_cWeaponAnimationHandle.cWeaponBase = weapon;
    }

    public void trigger(string Trigger)
    {
        m_animator.SetTrigger(Trigger);
    }

}
