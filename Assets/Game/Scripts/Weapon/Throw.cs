using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_rig2D;
    [SerializeField] private float m_fLifeTime;
     private float m_fPower;


    private WeaponBase m_cWeapon;
    private Vector2 m_v2PowerDir;

    private WeaponAttackData m_cAttackData;

    public void init(WeaponBase cWeapon, Vector2 v2PowerDir, float fPower)
    {
        m_v2PowerDir = v2PowerDir;
        m_cWeapon = cWeapon;
        m_fPower = fPower;
        m_cAttackData = m_cWeapon.cWeaponData.getWeaponAttackData(m_cWeapon.cComboSystem.nCurrentCombo);
    }

    public void shoot()
    {
        m_rig2D.velocity = m_v2PowerDir * m_fPower;
        StartCoroutine(ThrowEvent());
    }



    IEnumerator ThrowEvent()
    {
        float fTime = .0f;

        while(fTime < m_fLifeTime)
        {
            yield return null;
            fTime += Time.deltaTime;
        }
        Destroy(gameObject);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        int nTargetMask = 1 << collision.gameObject.layer;

        if ((nTargetMask & m_cWeapon.maskTarget) != 0)
        {
            collision.GetComponent<UnitBase>().hit(m_cWeapon.cUnit, m_cAttackData);
            m_fLifeTime = 0.0f;
        }

        if(collision.tag == "Wall")
        {
            m_fLifeTime = 0.0f;
        }


    }
}
