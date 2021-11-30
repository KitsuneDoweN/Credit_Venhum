using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpManager : MonoBehaviour
{
    [SerializeField] Image[] m_hpImgs;

    public void drawHp(int nHP)
    {
        int nIndex = 0;

        while(nIndex < m_hpImgs.Length)
        {
            m_hpImgs[nIndex].enabled = false;
            if (nIndex < nHP)
                m_hpImgs[nIndex].enabled = true;

            nIndex++;
        }

    }

    public void toggle(bool bToggle)
    {
        gameObject.SetActive(bToggle);
    }


}
