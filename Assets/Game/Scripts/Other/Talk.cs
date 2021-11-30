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


    public void talkEvnet()
    {
        m_bToggle = true;
        GameManager.instance.cStageManager.cPlayer.isControl = false;


        if (m_nIndex >= m_cTalkDatas.Length)
        {
            m_bToggle = false;
            m_nIndex = 0;
            GameManager.instance.cStageManager.cPlayer.isControl = true;
        }
           

        GameManager.instance.cUIManager.cUI_InGame.cUI_Talk.toggle(m_bToggle);

        if (!m_bToggle)
            return;

        TalkData cData = m_cTalkDatas[m_nIndex];
        GameManager.instance.cUIManager.cUI_InGame.cUI_Talk.draw(cData.strName ,cData.strTalk,cData.sprite);

        m_nIndex++;
    }
}
