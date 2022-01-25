using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProductionBase : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent m_producionEndEvent;
    public void productionAction()
    {
        StartCoroutine(productionCoroutine());
    }

    protected virtual IEnumerator productionCoroutine()
    {
        yield return null;
    }

    protected IEnumerator timeSlowAction(float fTimeScale, float fTime)
    {
        Time.timeScale = fTimeScale;

        yield return new WaitForSecondsRealtime(fTime);
        Time.timeScale = 1.0f;
    }


}
