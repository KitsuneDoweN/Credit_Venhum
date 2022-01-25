using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_BossInfo : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private Image m_hpImage;
    [SerializeField]
    private TextMeshProUGUI m_text;

    public void init()
    {
        toggle(false);
    }



    public void draw(Sprite icon ,int nHP , int nMaxHp, string name)
    {
        m_icon.sprite = icon;

        float fHpPercent = (float)nHP / (float)nMaxHp ;

        m_hpImage.fillAmount = fHpPercent;

        m_text.text = name;
    }

    public void toggle(bool bToggle)
    {
        gameObject.SetActive(bToggle);
    }
    
}
