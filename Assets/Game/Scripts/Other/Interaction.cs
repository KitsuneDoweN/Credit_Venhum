using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    [SerializeField] private UnityEvent m_interactionEvent;
    [SerializeField] private Transform m_trIcon;


    public UnityEvent interactionEvent
    {
        get
        {
            return m_interactionEvent;
        }
    }

    public Transform trIcon
    {
        get
        {
            return m_trIcon;
        }
    }




}
