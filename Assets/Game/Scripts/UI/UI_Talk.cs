using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Talk : UI_View
{
    [SerializeField]
    private TextMeshProUGUI m_nameText;
    [SerializeField]
    private TextMeshProUGUI m_talkText;
    [SerializeField]
    private Image m_image;

    public override void init()
    {
        base.init();
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }



    public void draw(string strName, string strTalk, Sprite sprite)
    {
        m_nameText.text = strName;
        m_talkText.text = strTalk;
        m_image.sprite = sprite;
    }

}
