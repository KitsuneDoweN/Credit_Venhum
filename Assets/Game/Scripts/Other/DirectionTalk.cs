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

        GameManager.instance.cInteraction = m_cInteraction;
        GameManager.instance.cInteraction.interactionEvent.Invoke();
    }



    public void directionEvent()
    {


        if (GameManager.instance.nClearGrogress != 0 || !m_cTalk.talkEvnet())
        {
            GameManager.instance.cStageManager.cPlayer.isControl = false;
            GameManager.instance.cUIManager.cUI_FadeInOut.fadeInOut(false, m_directionEndEvent);
            GameManager.instance.cInteraction = null;
        }
    }

    public void directionEndEvent()
    {

        GameManager.instance.cStageManager.cPlayer.isControl = true;
    }



}
