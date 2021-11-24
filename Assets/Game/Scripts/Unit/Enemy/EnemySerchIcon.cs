using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySerchIcon : MonoBehaviour
{
    public enum E_type
    { 
        E_NONE = -1, E_ON, E_OFF
    }


    [SerializeField]
    private GameObject[] m_goIcons;

    private GameObject m_goDraw;

    public void drawIcon(E_type eType)
    {
        if (m_goDraw != null)
            m_goDraw.SetActive(false);

        m_goDraw = m_goIcons[(int)eType];

        m_goDraw.SetActive(true);
    }

    public void init()
    {
        foreach(GameObject go in m_goIcons)
        {
            go.SetActive(false);
        }

        drawIcon(E_type.E_OFF);
    }

}
