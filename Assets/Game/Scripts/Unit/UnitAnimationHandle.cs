using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationHandle : MonoBehaviour
{
    [SerializeField]
    private UnitBase m_cUnitBase;

    public void unitControlFalse()
    {
        m_cUnitBase.isControl = false;
    }

    public void unitControlTrue()
    {
        m_cUnitBase.isControl = true;
    }

    public void godMode()
    {
        m_cUnitBase.godMode();
    }

}
