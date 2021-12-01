using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfo : MonoBehaviour
{
    public enum E_INFO
    {
        E_NONE = - 1, E_HP, E_STATMINA, E_SECONDARYWEAPON
    }
    
    [SerializeField]
    private ImageFilled[] m_cImageFileds;

    public void init()
    {

    }

    public void draw(E_INFO eInfo, float fFillAmount)
    {
        if (eInfo == E_INFO.E_NONE)
            return;

        m_cImageFileds[(int)eInfo].draw(fFillAmount);
    }



    public void toggle(bool bToggle)
    {
        gameObject.SetActive(bToggle);
    }


}
