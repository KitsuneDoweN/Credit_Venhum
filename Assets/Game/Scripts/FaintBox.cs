using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaintBox : MonoBehaviour
{
    [SerializeField] private float m_fLifeTime;


    private void Start()
    {
        Destroy(gameObject, m_fLifeTime);
    }

    
}
