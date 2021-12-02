using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Talk : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_nameText;
    [SerializeField]
    private TextMeshProUGUI m_talkText;
    [SerializeField]
    private Image m_image;

    public void init()
    {
        toggle(false);
    }

    public void toggle(bool bToggle)
    {
        gameObject.SetActive(bToggle);
    }

    public void draw(string strName, string strTalk, Sprite sprite)
    {
        m_nameText.text = strName;
        m_talkText.text = strTalk;
        m_image.sprite = sprite;
    }

}
