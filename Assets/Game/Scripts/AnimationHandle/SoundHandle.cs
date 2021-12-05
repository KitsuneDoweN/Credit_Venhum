using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandle : MonoBehaviour
{
    [SerializeField]
    private UnitSound m_cUnitSound;

    public void soundHitPlayOnce()
    {
        m_cUnitSound.hitPlayOnce();
    }

    public void soundDeathPlayOnce()
    {
        m_cUnitSound.deathPlayOnce();
    }

    public void soundAttackPlayOnce()
    {
        m_cUnitSound.attackPlayOnce(0);
    }

    public void soundFootStepPlayOnce()
    {
        m_cUnitSound.footStepPlayOnce();
    }

    public void soundAttackPlayeOnceIndex(int nIndex)
    {
        m_cUnitSound.attackPlayOnce(nIndex);
    }



}
