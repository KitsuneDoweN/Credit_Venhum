using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_InteractionText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_text;

    public void init()
    {
        toggle(false);
    }

    public void toggle(bool bToggle)
    {
        gameObject.SetActive(bToggle);

        if (GameManager.instance.eGameState != GameManager.E_GAMESTATE.E_INGAME)
            return;

        if (bToggle)
        {
            GameManager.instance.cStageManager.cPlayer.stop();
        }
        GameManager.instance.cStageManager.cPlayer.isControl = !bToggle;

    }

    public bool getToggle()
    {
        return gameObject.activeSelf;
    }

    public void draw(string str)
    {
        m_text.text = str;
    }

}
