using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionIcon : MonoBehaviour, IUpdate
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

    public void init()
    {
        UpdateManager.Instance.addProcesses(this);
    }

    public string id
    {
        get
        {
            return gameObject.name;
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
            GameManager.Instance.cStageManager.cCameraManager.mainCam.WorldToScreenPoint(v3DrawGamePos);
    }

    public void  updateProcesses()
    {
        if (m_bDraw && GameManager.Instance.eGameState == GameManager.E_GAMESTATE.E_INGAME)
            draw();
    }
}
