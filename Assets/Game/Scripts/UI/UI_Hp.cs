using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UI_Hp : MonoBehaviour
{

    [SerializeField] private Image [] m_hpImages;

    public enum E_HP 
    {
        E_NONE = -1,  E_FULL, E_HALF, E_TOTAL
    }



    public void init()
    {
        drawHp(E_HP.E_FULL);
    }



    public void drawHp(E_HP eHp)
    {
        foreach (Image image in m_hpImages)
            image.gameObject.SetActive(false);


        m_hpImages[(int)eHp].gameObject.SetActive(true);

    }


}
