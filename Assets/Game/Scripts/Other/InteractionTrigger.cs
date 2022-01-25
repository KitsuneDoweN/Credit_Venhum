using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [SerializeField]
    private Interaction m_cInteraction;
    private bool m_bEnter = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_bEnter && collision.tag == "Player")
        {
            
            GameManager.Instance.cInteraction = m_cInteraction;
            m_bEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_bEnter = false;
            GameManager.Instance.cInteraction = null;
        }
    }

    public void toggleOff()
    {
        GameManager.Instance.cInteraction = null;
    }
}
