using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class UnitHitSpriteTweenImfect : MonoBehaviour
{
    private Tween m_imfectTween;
    private SpriteRenderer m_model;

    [SerializeField] private Color m_hitColor;
    [SerializeField] private Color m_godColor;
    [SerializeField] private Color m_faintColor;

    [SerializeField] private float m_fHitTick;
    [SerializeField] private float m_fGodTick;
    [SerializeField] private float m_fFaintTick;

    public void init(SpriteRenderer model)
    {
        m_model = model;
        stop();
    }


    public void hitimfect()
    {
        stop();

        m_imfectTween = m_model.DOColor(m_hitColor, m_fHitTick).SetLoops(-1, LoopType.Yoyo);
        play();
    }

    public void godImfect()
    {
        stop();
        m_imfectTween = m_model.DOColor(m_godColor, m_fGodTick).SetLoops(-1, LoopType.Yoyo);
        play();
    }

    public void addFaintImfect(float fTime)
    {
        int nLoopCount = (int)(fTime / 0.2f);

        m_imfectTween = m_model.DOColor(m_faintColor, 0.2f).SetLoops(nLoopCount, LoopType.Yoyo).OnComplete(() =>
        {
            m_model.color = Color.white;
        });

    }



    public void play()
    {
        m_imfectTween.Play();
    }

    public void stop()
    {
        Utility.KillTween(m_imfectTween);
        m_model.color = Color.white;
    }



}
