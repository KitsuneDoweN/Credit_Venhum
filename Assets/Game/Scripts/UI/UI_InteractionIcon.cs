using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionIcon : MonoBehaviour
{
    [SerializeField] private Image m_icon;
    
    private Vector3 m_v3GamePos;

    private bool m_bDraw;

    private Vector3 v3DrawGamePos
    {
        set
        {
            m_v3GamePos = value;
        }
        get
        {
            return m_v3GamePos;
        }
    }


    public void drawOn(Vector3 v3GamePos)
    {
        v3DrawGamePos = v3GamePos;
        m_bDraw = true;
    }



    public void drawOff()
    {
        m_bDraw = false;
    }


    private void draw()
    {
        m_icon.rectTransform.anchoredPosition =
            GameManager.instance.cStageManager.cCameraManager.mainCam.WorldToScreenPoint(v3DrawGamePos);
    }

    private void Update()
    {
        if (m_bDraw && GameManager.instance.eGameState == GameManager.E_GAMESTATE.E_INGAME)
            draw();
    }
}
