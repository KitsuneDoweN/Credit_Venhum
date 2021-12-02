using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTweenImfectHandle : MonoBehaviour
{
    [SerializeField] UnitSpriteTweenImfect m_cImfectTween;

    public void hitImfect()
    {
        m_cImfectTween.hitimfect();
    }



    public void stopImfect()
    {
        m_cImfectTween.stop();
    }

    public void dieImfect()
    {
        m_cImfectTween.dieImfect();
    }

    public void freshImfect()
    {
        m_cImfectTween.freshImfect();
    }

}
