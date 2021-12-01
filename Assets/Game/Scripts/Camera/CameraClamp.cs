using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{
    [SerializeField] private Vector2 m_v2Center;
    [SerializeField] private Vector2 m_v2Size;

    private Vector2 m_v2LeftDownPos;
    private Vector2 m_v2RightUpPos;




    public void init()
    {
        Vector2 v2HalfSize = m_v2Size / 2;
        m_v2LeftDownPos = m_v2Center - v2HalfSize;
        m_v2RightUpPos = m_v2Center + v2HalfSize;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(m_v2Center, m_v2Size);
    }

    public Vector2 clamp(Vector2 v2CurrentCamera, Vector2 v2CamBoundZero, Vector2 v2CamBoundOne)
    {
        Vector2 v2ReturnClamp = v2CurrentCamera;

        Vector2 v2CamBoundZeroToCamCentor = v2CurrentCamera - v2CamBoundZero;
        Vector2 v2CamBoundOneToCamCentor = v2CurrentCamera - v2CamBoundOne;


        if (v2CamBoundZero.x >= m_v2LeftDownPos.x && v2CamBoundOne.x <= m_v2RightUpPos.x)
        {
            if (v2CamBoundZero.y >= m_v2LeftDownPos.y && v2CamBoundOne.y <= m_v2RightUpPos.y)
                return v2ReturnClamp;
        }



        if (v2CamBoundZero.x < m_v2LeftDownPos.x && v2CamBoundOne.x > m_v2RightUpPos.x)
        {
            v2ReturnClamp.x = m_v2Center.x;
        }
        else
        {
            if(v2CamBoundZero.x < m_v2LeftDownPos.x)
            {
                v2ReturnClamp.x = m_v2LeftDownPos.x + v2CamBoundZeroToCamCentor.x;
            }
            else if(v2CamBoundOne.x > m_v2RightUpPos.x)
            {
                v2ReturnClamp.x = m_v2RightUpPos.x + v2CamBoundOneToCamCentor.x;
            }
        }

        if (v2CamBoundZero.y < m_v2LeftDownPos.y && v2CamBoundOne.y > m_v2RightUpPos.y)
        {
            v2ReturnClamp.y = m_v2Center.y;
        }
        else
        {
            if(v2CamBoundZero.y < m_v2LeftDownPos.y)
            {
                v2ReturnClamp.y = m_v2LeftDownPos.y + v2CamBoundZeroToCamCentor.y;
            }
            else if (v2CamBoundOne.y > m_v2RightUpPos.y)
            {
                v2ReturnClamp.y = m_v2RightUpPos.y + v2CamBoundOneToCamCentor.y;
            }
        }


        return v2ReturnClamp;
    }

}
