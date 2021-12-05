using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class UI_FadeInOut : UI_View
{
    [SerializeField]
    private float m_fFadeInTime;
    [SerializeField]
    private float m_fFadeOutTime;

    [SerializeField]
    private Color m_fadeInColor;
    [SerializeField]
    private Color m_fadeOutColor;


    [SerializeField]
    private Image m_img;

    private Tween m_fadeInOutTween;

    [SerializeField] 
    private UnityEvent m_fadeInOutCommpletEvent;


    public override void init()
    {
        base.init();
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }

    public void draw(bool bFadein)
    {
        m_img.color = m_fadeInColor;

        if (!bFadein)
        {
            m_img.color = m_fadeOutColor;
        }

    }


    public void fadeInOut(bool bFadein, UnityEvent fadeCompleteEvent)
    {
        if (bFadein)
        {
            m_img.color = m_fadeOutColor;
            Utility.KillTween(m_fadeInOutTween);
            m_fadeInOutTween = m_img.DOColor(m_fadeInColor, m_fFadeInTime).OnComplete(() => 
            {
                fadeCompleteEvent.Invoke();

            });

        }
        else
        {
            m_img.color = m_fadeInColor;
            Utility.KillTween(m_fadeInOutTween);
            m_fadeInOutTween = m_img.DOColor(m_fadeOutColor, m_fFadeOutTime).OnComplete(() =>
            {
                fadeCompleteEvent.Invoke();
            });
        }

    }
    public UnityEvent fadeInOutCommpletEvent
    {
        get
        {
            return m_fadeInOutCommpletEvent;
        }
    }


}
