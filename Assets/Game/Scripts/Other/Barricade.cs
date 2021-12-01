using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private bool m_bInteraction = false;
    [SerializeField] private Interaction m_cInteraction;

    [SerializeField] private string m_strInteraction;

    public void Start()
    {
        eventOn();
    }

    public void eventOn()
    {
        m_bInteraction = true;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_bInteraction) return;

        if (collision.tag == "Player")
            GameManager.instance.cInteraction = m_cInteraction;

    }

    public void offEvent()
    {
        Debug.Log("BarricadeOff");
        gameObject.SetActive(false);
        GameManager.instance.cInteraction = null;
        GameManager.instance.cUIManager.cUI_InGame.cUI_InteractionText.toggle(true);
        GameManager.instance.cUIManager.cUI_InGame.cUI_InteractionText.draw(m_strInteraction);
    }
}
