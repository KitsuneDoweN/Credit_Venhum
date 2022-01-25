using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEvent : MonoBehaviour
{
    public enum EventType
    {
        E_NONE = -1, E_AREA_CLEAR
    }

    [SerializeField]
    private int m_nEventIndex;

    public bool isEventClear
    {
        get
        {
            return GameManager.Instance.cSaveManager.isEventClear(nEventIndex);
        }
    }

    public EventType eventType
    {
        get
        {
            return (EventType)GameManager.Instance.cSaveManager.getEventType(m_nEventIndex);
        }
    }




    public void eventClear()
    {
        GameManager.Instance.cSaveManager.stageEventDataSave(nEventIndex, 1);
        GameManager.Instance.cSaveManager.save();
    }

    public int nEventIndex
    {
        get
        {
            return m_nEventIndex;
        }
    }
}
