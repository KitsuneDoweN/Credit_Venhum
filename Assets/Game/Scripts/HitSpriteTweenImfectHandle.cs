using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSpriteTweenImfectHandle : MonoBehaviour
{
    [SerializeField] UnitHitSpriteTweenImfect m_cImfectTween;

    public void hitImfect()
    {
        m_cImfectTween.stop();
        m_cImfectTween.hitimfect();
        m_cImfectTween.play();
    }



    public void stopImfect()
    {
        m_cImfectTween.stop();
    }

}
