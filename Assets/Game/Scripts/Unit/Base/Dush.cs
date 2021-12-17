using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dush : MonoBehaviour
{
    private UnitBase m_cUnit;

    private LayerMask m_wallLayerMask;

    private IEnumerator m_ieDushCoroutine;
    private IEnumerator ieDushCoroutine
    {
        set
        {
            if (m_ieDushCoroutine != null)
                StopCoroutine(m_ieDushCoroutine);

            m_ieDushCoroutine = value;
        }
        get
        {
            return m_ieDushCoroutine;
        }
    }

    [SerializeField]
    private UnityEvent m_dushEndEvent;

    [SerializeField]
    private float m_fCollisionDistance;


    private float m_fTime;
    public float fTime
    {
        set
        {
            m_fTime = value;
        }
        get
        {
            return m_fTime;
        }
    }



    private float m_fPower;


    public float fPower
    {
        set
        {
            m_fPower = value;
        }
        get
        {
            return m_fPower;
        }
    }


    public void init(UnitBase unit , float fPower, float fTime)
    {
        m_cUnit = unit;
        m_ieDushCoroutine = null;

        m_wallLayerMask = LayerMask.GetMask("Wall");

        setDushInfo(fPower, fTime);
    }


    public void setDushInfo(float fPower, float fTime)
    {

        m_fPower = fPower;
        m_fTime = fTime;

    }


    public void dushDetail(Vector2 v2Dir, float fPower, float fDushTime, bool bEndEvent)
    {
        if (!isDushAble)
            return;

        ieDushCoroutine = dushEvnet(v2Dir, fPower, fDushTime, bEndEvent);
        StartCoroutine(m_ieDushCoroutine);
    }

    public void dush(Vector2 v2Dir, bool bEndEvent)
    {
        if (!isDushAble)
            return;

        ieDushCoroutine = dushEvnet(v2Dir, fPower, fTime, bEndEvent);
        StartCoroutine(m_ieDushCoroutine);
    }

    private IEnumerator dushEvnet(Vector2 v2Dir, float fPower, float fDushTime, bool bEndEvent)
    {
        float fTime = 0.0f;

        bool bControl = m_cUnit.isControl;

        m_cUnit.isControl = false;



        m_cUnit.v2Velocity = v2Dir * fPower;

        while (fTime < fDushTime)
        {
            fTime += Time.deltaTime;

            wallCollision(v2Dir, m_fCollisionDistance);

            yield return null;
        }

        m_cUnit.v2Velocity = Vector2.zero;

        m_cUnit.isControl = bControl;




        if (bEndEvent)
            m_dushEndEvent.Invoke();


        m_ieDushCoroutine = null;
    }

    public bool isDushAble
    {
        get
        {
            if (m_ieDushCoroutine == null)
                return true;

            return false;
        }
    }

    public void dushStop()
    {
        if (m_ieDushCoroutine == null) return;

        StopCoroutine(m_ieDushCoroutine);
        m_ieDushCoroutine = null;
        m_cUnit.v2Velocity = Vector2.zero;
        m_cUnit.isControl = true;
    }



    private void wallCollision(Vector2 v2Dir, float fCollisionDistance)
    {
        RaycastHit2D hit2D = Physics2D.Raycast(m_cUnit.v2UnitPos, v2Dir, fCollisionDistance, m_wallLayerMask);

        if (!hit2D)
            return;

        m_cUnit.v2Velocity = Vector2.zero;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (m_cUnit == null)
            return;

        Gizmos.DrawLine(m_cUnit.v2UnitPos, m_cUnit.v2UnitPos + (m_cUnit.v2LookDir * m_fCollisionDistance));
    }

}

