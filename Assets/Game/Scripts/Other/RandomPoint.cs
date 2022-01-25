using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPoint : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_v2Size;

    private Vector2 m_v2LeftDown;
    private Vector2 m_v2RightUp;

    private Vector2 m_v2HalfSize;

    private Vector2 m_v2Centor
    {
        get
        {
            return (Vector2)gameObject.transform.position;
        }
    }


    public void init()
    {


        m_v2HalfSize = m_v2Size * 0.5f;

        m_v2LeftDown = m_v2Centor - m_v2HalfSize;
        m_v2RightUp = m_v2Centor + m_v2HalfSize;

    }

    public Vector2 getRandomPoint()
    {
        Vector2 v2RandomPoint = new Vector2(Random.RandomRange(m_v2LeftDown.x, m_v2RightUp.x), Random.RandomRange(m_v2LeftDown.y, m_v2RightUp.y));
        return v2RandomPoint;
    }

    //(-5.0, 0.8)

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(m_v2Centor, m_v2Size);
    }
}
