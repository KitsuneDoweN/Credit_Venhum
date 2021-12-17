using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBloodImfect : MonoBehaviour
{
    [SerializeField]
    private GameObject m_goBolodImfect;

    private IEnumerator m_ieBloodImfectCorotinue;

    public void init()
    {
        m_goBolodImfect.SetActive(false);
    }


    public void bloodImfect(Vector2 v2Dir)
    {
        if (m_ieBloodImfectCorotinue != null)
        {
            StopCoroutine(m_ieBloodImfectCorotinue);
        }


        m_goBolodImfect.SetActive(false);
        transform.rotation = Quaternion.Euler(.0f, .0f, Utility.getHorizontalAtBetweenAngle(v2Dir));


        m_ieBloodImfectCorotinue = blooImfectEventCoroutine();
        StartCoroutine(m_ieBloodImfectCorotinue);
    }

    private IEnumerator blooImfectEventCoroutine()
    {
        
        yield return new WaitForEndOfFrame();

        m_goBolodImfect.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        m_goBolodImfect.SetActive(false);
        m_ieBloodImfectCorotinue = null;
    }

}
