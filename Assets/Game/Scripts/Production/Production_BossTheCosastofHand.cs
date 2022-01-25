using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production_BossTheCosastofHand : ProductionBase
{
    

    protected override IEnumerator productionCoroutine()
    {
        GameManager.Instance.cStageManager.cPlayer.stop();
        GameManager.Instance.cStageManager.cPlayer.isControl = false;
        yield return timeSlowAction(0.1f, 0.5f);
        GameManager.Instance.cUIManager.cUI_FadeInOut.toggle(true);
        GameManager.Instance.cUIManager.cUI_FadeInOut.fadeInOut(true, m_producionEndEvent);
    }



}
