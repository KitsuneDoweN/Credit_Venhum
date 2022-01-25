using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirseTalk : UnitTalkBase
{
    private int m_nFristMeet = 3;
    [SerializeField]
    private StageEvent m_cEvent;

    private void Start()
    {
        
        init();
    }

    protected override void init()
    {
        base.init();

        if (m_cEvent.isEventClear)
            m_nTalkIndex++;

    }

    public void nextTalk()
    {
        m_nTalkIndex++;
    }
}
