using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTalk : MonoBehaviour
{
    [SerializeField]
    private Talk m_cTalk;
    [SerializeField]
    private Interaction m_cInteraction;
    private bool m_bToggle;


    private void Start()
    {
        m_cTalk.init();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_bToggle = true;
            GameManager.instance.cInteraction = m_cInteraction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_bToggle = false;
            GameManager.instance.cInteraction = null;
        }
    }

    public void talkEvent()
    {
        m_cTalk.talkEvnet();
    }

    public void changeTalk(Talk cTalk)
    {
        m_cTalk = cTalk;
        if(m_bToggle)
            GameManager.instance.cInteraction = m_cInteraction; ;
    }

}
