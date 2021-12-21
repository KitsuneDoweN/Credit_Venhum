using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    private bool m_bInteraction = false;
    [SerializeField] private Interaction m_cInteraction;

    [SerializeField] private string m_strInteraction;

    [SerializeField]
    private Sprite m_offSprite;
    [SerializeField]
    private SpriteRenderer m_srModel;
    
    [SerializeField]
    private BoxCollider2D[] m_baricadeCollider;



    public void eventOn()
    {
        m_bInteraction = true;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_bInteraction) return;

        if (collision.tag == "Player")
            GameManager.Instance.cInteraction = m_cInteraction;

    }

    public void offEvent(bool bSkip)
    {
        Debug.Log("BarricadeOff");

        m_srModel.sprite = m_offSprite;

        foreach (Collider2D coll in m_baricadeCollider)
        {
            coll.enabled = false;
        }

        if (bSkip)
            return;
      
        GameManager.Instance.nClearGrogress++;





        GameManager.Instance.cInteraction = null;
        GameManager.Instance.cUIManager.cUI_InGame.cUI_InteractionText.toggle(true);
        GameManager.Instance.cUIManager.cUI_InGame.cUI_InteractionText.draw(m_strInteraction);
    }
}
