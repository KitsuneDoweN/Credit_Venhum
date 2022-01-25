using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkMasterManager : SingleToon<TalkMasterManager>
{
    private Dictionary<int, List<TalkData>> m_talkDictionary;
    [SerializeField]
    private CSV m_cTalkData;

    private string m_strTalkID = "TalkID";
    private string m_strSpriteID = "SpriteID";
    private string m_strStrName = "StrName";
    private string m_strStrTalk = "StrTalk";


    private void Awake()
    {
        init();
        talkDataSetting();
    }

    protected override bool init()
    {
        bool bOverlap = !base.init();
        if (bOverlap)
            return false;

        m_cTalkData.init();
        m_talkDictionary = new Dictionary<int, List<TalkData>>();
        return true;
    }

    private void talkDataSetting()
    {
        int nTalkID = -1;
        int nCurrentTalkID;
        string strSpriteID;
        string strName;
        string strTalk;
        int nDataCount = 0;

        while(nDataCount < m_cTalkData.nListCount)
        {
            nCurrentTalkID = Utility.convertObjectToInt(m_cTalkData.getData(nDataCount, m_strTalkID));
            strSpriteID = m_cTalkData.getData(nDataCount, m_strSpriteID).ToString();
            strName = m_cTalkData.getData(nDataCount, m_strStrName).ToString();
            strTalk = m_cTalkData.getData(nDataCount, m_strStrTalk).ToString();

            if(nCurrentTalkID != nTalkID)
            {
                m_talkDictionary.Add(nCurrentTalkID, new List<TalkData>());
                nTalkID = nCurrentTalkID;
            }

            TalkData newData = new TalkData();
            Sprite newSprite = Resources.Load<Sprite>("TalkUI/" + strSpriteID);

            newData.init(newSprite, strName, strTalk);


            m_talkDictionary[nCurrentTalkID].Add(newData);

            nDataCount++;
        }


       


    }

    public List<TalkData> getTalkDataList(int nID)
    {
        return m_talkDictionary[nID];
    }

}
