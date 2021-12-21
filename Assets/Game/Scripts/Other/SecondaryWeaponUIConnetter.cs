using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponUIConnetter : MonoBehaviour
{
    [SerializeField]
    private WeaponBase m_cWeapon;
    private IEnumerator m_ieCoolTimeEvent;


    public void coolTimeEvent()
    {
        if (m_ieCoolTimeEvent != null)
        {
            StopCoroutine(m_ieCoolTimeEvent);
            m_ieCoolTimeEvent = null;
        }

        m_ieCoolTimeEvent = collTimeEvetn();
        StartCoroutine(m_ieCoolTimeEvent);

    }


    private IEnumerator collTimeEvetn()
    {
        float fFillAmount = .0f;
        while (m_cWeapon.cCoolTime.isCoolTime)
        {
            fFillAmount = m_cWeapon.cCoolTime.fTick / m_cWeapon.cWeaponData.fCoolTime;
            GameManager.Instance.cUIManager.cUI_InGame.cUI_PlayerInfo.draw(
                UI_PlayerInfo.E_INFO.E_SECONDARYWEAPON, fFillAmount);
            yield return null;
        }
        GameManager.Instance.cUIManager.cUI_InGame.cUI_PlayerInfo.draw(
    UI_PlayerInfo.E_INFO.E_SECONDARYWEAPON, 1.0f);
        m_ieCoolTimeEvent = null;
    }
}
