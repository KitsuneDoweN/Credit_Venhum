using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dush : MonoBehaviour
{
    private UnitBase m_cUnit;

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

        ieDushCoroutine = dushEvnet(m_cUnit.rig2D, v2Dir, fPower, fDushTime, bEndEvent);
        StartCoroutine(m_ieDushCoroutine);
    }

    public void dush(Vector2 v2Dir, bool bEndEvent)
    {
        if (!isDushAble)
            return;

        ieDushCoroutine = dushEvnet(m_cUnit.rig2D, v2Dir, fPower, fTime, bEndEvent);
        StartCoroutine(m_ieDushCoroutine);
    }

    private IEnumerator dushEvnet(Rigidbody2D rigidbody2D, Vector2 v2Dir, float fPower, float fDushTime, bool bEndEvent)
    {
        float fTime = 0.0f;

        bool bControl = m_cUnit.isControl;

        m_cUnit.isControl = false;

        m_cUnit.v2Velocity = v2Dir * fPower;

        while (fTime < fDushTime)
        {
            fTime += Time.deltaTime;
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


}

