using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_View : MonoBehaviour
{
    
    private RectTransform m_rootRect;
    private Canvas m_canvas;
    private Vector3 m_v3OriginRectPos;
    protected bool m_bToogle;

    public virtual void init()
    {
        m_canvas = GetComponent<Canvas>();
           m_rootRect = GetComponent<RectTransform>();
        m_v3OriginRectPos = m_rootRect.localPosition;
        toggle(false);
    }

    public virtual void toggle(bool bToggle)
    {
        m_bToogle = bToggle;
        m_rootRect.localPosition = m_v3OriginRectPos;

        if (bToggle)
        {
            m_rootRect.localPosition = Vector3.zero;
        }

        m_canvas.enabled = bToggle;
    }



}
