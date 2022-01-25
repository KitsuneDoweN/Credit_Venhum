using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTalkBase : MonoBehaviour
{

    [SerializeField]
    private Interaction m_cInteraction;
    private bool m_bToggle;

    [SerializeField]
    protected List<Talk> m_talkList;

    protected int m_nTalkIndex;

    [SerializeField]
    private Transform m_trTalkListParent;

    protected virtual void init()
    {
        m_talkList = new List<Talk>();
        for (int i = 0; i < m_trTalkListParent.GetChildCount(); i++)
        {
            m_talkList.Add(m_trTalkListParent.GetChild(i).GetComponent<Talk>());
        }


        foreach (Talk cTalk in m_talkList)
            cTalk.init();

        m_nTalkIndex = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            m_bToggle = true;
            GameManager.Instance.cInteraction = m_cInteraction;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_bToggle = false;
            GameManager.Instance.cInteraction = null;
        }
    }

    public void talkEvent()
    {
        m_talkList[m_nTalkIndex].talkEvnet();
    }

    protected void changeTalk(int nIndex)
    {
        m_nTalkIndex = nIndex;
        if(m_bToggle)
            GameManager.Instance.cInteraction = m_cInteraction;
    }

}
