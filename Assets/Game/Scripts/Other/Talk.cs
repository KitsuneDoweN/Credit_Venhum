using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Talk : MonoBehaviour
{
    [SerializeField] TalkData[] m_cTalkDatas;
    [SerializeField]
    private UnityEvent m_TalkEndEvent;


    private int m_nIndex;
    private bool m_bToggle;

    public void init()
    {
        m_nIndex = 0;
        m_bToggle = false;
    }


    public bool talkEvnet()
    {
        m_bToggle = true;

        GameManager.Instance.cStageManager.cPlayer.stop();
        GameManager.Instance.cStageManager.cPlayer.isControl = false;



        if (m_nIndex >= m_cTalkDatas.Length)
        {
            m_bToggle = false;
            m_nIndex = 0;
            GameManager.Instance.cStageManager.cPlayer.isControl = true;
        }
           

        GameManager.Instance.cUIManager.cUI_Talk.toggle(m_bToggle);

        if (!m_bToggle)
        {
            m_TalkEndEvent.Invoke();
            return m_bToggle;
        }


        TalkData cData = m_cTalkDatas[m_nIndex];
        GameManager.Instance.cUIManager.cUI_Talk.draw(cData.strName ,cData.strTalk,cData.sprite);

        m_nIndex++;

        return m_bToggle;
    }
}
