using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGrip : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_v2LocalPivot;

    private float m_fDistance;



    public void init(float fDistance)
    {
        gripSetting(fDistance);


    }



    public void gripUpdate(Vector2 v2Dir)
    {
        Vector3 v3GripVector = (Vector3)(v2Dir * m_fDistance);



        transform.localPosition = (Vector3)m_v2LocalPivot;

        transform.localPosition += v3GripVector;
    }



    public void gripSetting( float fDistance)
    {
        m_fDistance = fDistance;
    }

    





}
