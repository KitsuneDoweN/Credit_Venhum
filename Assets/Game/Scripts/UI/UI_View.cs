using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_View : MonoBehaviour
{
    
    private RectTransform m_rootRect;
    private Vector3 m_v3OriginRectPos;

    public virtual void init()
    {
        m_rootRect = GetComponent<RectTransform>();
        m_v3OriginRectPos = m_rootRect.localPosition;
    }

    public virtual void toggle(bool bToggle)
    {
        m_rootRect.localPosition = m_v3OriginRectPos;

        if (bToggle)
        {
            m_rootRect.localPosition = Vector3.zero;
        }

        m_rootRect.gameObject.SetActive(bToggle);
    }



}
