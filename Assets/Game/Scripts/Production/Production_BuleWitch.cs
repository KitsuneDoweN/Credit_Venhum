using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production_BuleWitch : ProductionBase
{

    protected override IEnumerator productionCoroutine()
    {
        yield return timeSlowAction(0.1f, 0.1f);

        yield return new WaitForSecondsRealtime(2.0f);

        GameManager.Instance.cStageManager.cPlayer.stop();
        GameManager.Instance.cStageManager.cPlayer.isControl = false;

        m_producionEndEvent.Invoke();
    }
}
