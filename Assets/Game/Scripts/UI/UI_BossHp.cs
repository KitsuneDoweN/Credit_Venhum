using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UI_BossHp : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private Slider m_slider;

    public void init()
    {
        
    }



    public void draw(Sprite icon ,int nHP , int nMaxHp)
    {
        m_icon.sprite = icon;

        float fHpPercent = (float)nHP / (float)nMaxHp ;

        m_slider.value = fHpPercent;
    }

    public void toggle(bool bToggle)
    {
        gameObject.SetActive(bToggle);
    }
    
}
