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
        m_cUnitBase.isMoveAble = false;
        m_cUnitBase.isLookAble = false;
    }

    public void unitControlTrue()
    {
        m_cUnitBase.isControl = true;
        m_cUnitBase.isMoveAble = true;
        m_cUnitBase.isLookAble = true;
    }




    public void godMode()
    {
        m_cUnitBase.godMode();
    }



}
