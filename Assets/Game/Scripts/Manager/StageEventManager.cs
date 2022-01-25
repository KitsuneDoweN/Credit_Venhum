using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField]
    private List<StageEvent> m_stageEventList = new List<StageEvent>();

    public void init(StageManager stageManager)
    {
        foreach(StageEvent cStageEvent in m_stageEventList)
        {
            if (cStageEvent.isEventClear)
            {
                if(cStageEvent.eventType == StageEvent.EventType.E_AREA_CLEAR)
                {
                    stageManager.skipWave();
                }
            }
        }
    }

}
