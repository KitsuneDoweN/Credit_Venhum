using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UI_GameLoading : UI_View
{
    [SerializeField]
    private TextMeshProUGUI m_LoadText;

    private IEnumerator m_ieLoadAnimationCoroutine;

    [SerializeField]
    private string m_strLoading;
    [SerializeField]
    private float m_fTick;
    
    public override void init()
    {
        base.init();

        toggle(false);
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }

    public void startLoadAnimaiton()
    {
        m_ieLoadAnimationCoroutine = loadAnimation();

        StartCoroutine(m_ieLoadAnimationCoroutine);
    }

    public void stopLoadAnimation()
    {
        StopCoroutine(m_ieLoadAnimationCoroutine);
        m_ieLoadAnimationCoroutine = null;
    }

    private IEnumerator loadAnimation()
    {

        int nDotCount;

        while (true)
        {
            m_LoadText.text = m_strLoading;
            nDotCount = 0;

            while (nDotCount <= 3)
            {
                yield return new WaitForSeconds(m_fTick);

                m_LoadText.text += ".";

                nDotCount++;
            }
        }
    }


}
