using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoolTime : MonoBehaviour
{
    private bool m_bCoolTime;
    private IEnumerator m_ieCoolTimeEvent;
    private float m_fTick;

    [SerializeField]
    private UnityEvent m_event;

    public bool isCoolTime
    {
        set
        {
            m_bCoolTime = value;
        }
        get
        {
            return m_bCoolTime;
        }
    }

    public void startCoolTime(float fCoolTime)
    {
        stopCoolTime();

        m_ieCoolTimeEvent = coolTimeCoroutine(fCoolTime);
        StartCoroutine(m_ieCoolTimeEvent);
    }

    public void stopCoolTime()
    {
        if (m_ieCoolTimeEvent != null)
            StopCoroutine(m_ieCoolTimeEvent);

        m_ieCoolTimeEvent = null;
        isCoolTime = false;
    }


    private IEnumerator coolTimeCoroutine(float fCoolTime)
    {
        isCoolTime = true;
        fTick = .0f;


        m_event.Invoke();

        while (m_fTick < fCoolTime)
        {
            m_fTick += Time.deltaTime;
            yield return null;
        }

        isCoolTime = false;
        m_ieCoolTimeEvent = null;
    }

    public float fTick
    {
        set
        {
            m_fTick = value;
        }
        get
        {
            return m_fTick;
        }
    }

}
