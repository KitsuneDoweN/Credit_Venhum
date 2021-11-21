using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrip : MonoBehaviour
{
    private Vector2 m_v2LocalPivot;

    private float m_fDistance;


    public void init(Vector2 v2LocalPivot, float fDistance)
    {
        gripSetting(v2LocalPivot, fDistance);
    }



    public void gripUpdate(Vector2 v2Dir)
    {
        Vector3 v3GripVector = (Vector3)(v2Dir * m_fDistance);

        transform.localPosition = m_v2LocalPivot;

        transform.localPosition += v3GripVector;
    }

    public void gripSetting(Vector2 v2LocalPivot, float fDistance)
    {
        m_v2LocalPivot = v2LocalPivot;
        m_fDistance = fDistance;
    }

    




}
