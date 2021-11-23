using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class UnitHitSpriteTweenImfect : MonoBehaviour
{
    private Sequence m_imfectSequence;
    private SpriteRenderer m_model;
    [SerializeField] private Color m_hitColor;
    [SerializeField] private Color m_godColor;
    [SerializeField] private Color m_faintColor;

    public void init(SpriteRenderer model)
    {
        m_model = model;
        reset();
    }


    public void addHitimfect()
    {
        Tween hitTween = m_model.DOColor(m_hitColor, 0.1f).SetLoops(4, LoopType.Yoyo).OnComplete(() =>
        {
            m_model.color = Color.white;
        });

        m_imfectSequence.Append(hitTween);
    }

    public void addGodImfect()
    {
        Tween godTween = m_model.DOColor(m_godColor, 0.2f).SetLoops(5, LoopType.Yoyo).OnComplete(() =>
        {
            m_model.color = Color.white;
        });

        m_imfectSequence.Append(godTween);
    }

    public void addFaintImfect(float fTime)
    {
        int nLoopCount = (int)(fTime / 0.2f);

        Tween faintTween = m_model.DOColor(m_faintColor, 0.2f).SetLoops(nLoopCount, LoopType.Yoyo).OnComplete(() =>
        {
            m_model.color = Color.white;
        });

        m_imfectSequence.Append(faintTween);
    }



    public void play()
    {
        m_imfectSequence.Play();
    }

    public void reset()
    {
        Utility.resetColorSequence(ref m_imfectSequence, Color.white, m_model);
    }



}
