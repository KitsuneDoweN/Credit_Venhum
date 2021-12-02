using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class UnitSpriteTweenImfect : MonoBehaviour
{
    private Tween m_imfectTween;
    private SpriteRenderer m_model;

    [SerializeField] private Color m_hitColor;
    [SerializeField] private Color m_godColor;
    [SerializeField] private Color m_dieColor;

    [SerializeField] private Color m_freshColor;


    [SerializeField] private float m_fHitTick;
    [SerializeField] private float m_fGodTick;
    [SerializeField] private float m_fdieTime;
    [SerializeField] private float m_fFreshTick;


    public void init(SpriteRenderer model)
    {
        m_model = model;
        stop();
    }


    public void hitimfect()
    {
        stop();

        m_imfectTween = m_model.DOColor(m_hitColor, m_fHitTick).SetLoops(4, LoopType.Yoyo);
        play();
    }

    public void godImfect()
    {
        stop();
        m_imfectTween = m_model.DOColor(m_godColor, m_fGodTick).SetLoops(-1, LoopType.Yoyo);
        play();
    }

    public void dieImfect()
    {

        stop();
        m_imfectTween = m_model.DOColor(m_dieColor, m_fdieTime);
        play();

    }

    public void freshImfect()
    {
        stop();
        m_imfectTween = m_model.DOColor(m_freshColor, m_fFreshTick).SetLoops(1, LoopType.Yoyo).OnComplete(() =>
        {
            m_model.color = Color.white;
        });
        play();
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
