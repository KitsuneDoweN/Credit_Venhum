using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{
    [SerializeField] TalkData[] m_cTalkDatas;



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

        GameManager.instance.cStageManager.cPlayer.stop();
        GameManager.instance.cStageManager.cPlayer.isControl = false;



        if (m_nIndex >= m_cTalkDatas.Length)
        {
            m_bToggle = false;
            m_nIndex = 0;
            GameManager.instance.cStageManager.cPlayer.isControl = true;
        }
           

        GameManager.instance.cUIManager.cUI_Talk.toggle(m_bToggle);

        if (!m_bToggle)
            return m_bToggle;

        TalkData cData = m_cTalkDatas[m_nIndex];
        GameManager.instance.cUIManager.cUI_Talk.draw(cData.strName ,cData.strTalk,cData.sprite);

        m_nIndex++;

        return m_bToggle;
    }
}
