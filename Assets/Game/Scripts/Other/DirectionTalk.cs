using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DirectionTalk : MonoBehaviour
{
    [SerializeField]
    private Talk m_cTalk;
    [SerializeField]
    private Interaction m_cInteraction;
    [SerializeField]
    private UnityEvent m_directionEndEvent;



    public void directionStart()
    {

        GameManager.Instance.cInteraction = m_cInteraction;
        GameManager.Instance.cInteraction.interactionEvent.Invoke();
    }



    public void directionEvent()
    {


        if (GameManager.Instance.nClearGrogress != 0 || !m_cTalk.talkEvnet())
        {
            GameManager.Instance.cStageManager.cPlayer.isControl = false;
            GameManager.Instance.cUIManager.cUI_FadeInOut.fadeInOut(false, m_directionEndEvent);
            GameManager.Instance.cInteraction = null;
        }
    }

    public void directionEndEvent()
    {

        GameManager.Instance.cStageManager.cPlayer.isControl = true;
    }



}
