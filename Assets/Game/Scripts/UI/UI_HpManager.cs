using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HpManager : MonoBehaviour
{
    [SerializeField] private GameObject m_uiHpPrefab;

    private List<UI_Hp> m_hpList = new List<UI_Hp>();
    private int m_nHpDrawCount;

    public  void init()
    {
        
        m_nHpDrawCount = (int)UI_Hp.E_HP.E_TOTAL;
      
    }

    public void setPlayerUI(int nHP)
    {
        while(m_hpList.Count > 0)
        {
            Destroy(m_hpList[0].gameObject);
            m_hpList.RemoveAt(0);
        }
        
        
        m_hpList.Capacity = nHP / m_nHpDrawCount;

        int nCount = 0;


        while (nCount < m_hpList.Capacity)
        {
            m_hpList.Add(Instantiate(m_uiHpPrefab, transform).GetComponent<UI_Hp>());
            m_hpList[m_hpList.Count - 1].init();
            nCount++;
        }
    }





    public void draw(int nHp)
    {
        if (nHp <= 0) return;

        int nHpCount = nHp / m_nHpDrawCount;
        int nHpLastDrawHpImage = nHp % m_nHpDrawCount;

        for (int i = 0; i < m_hpList.Count; i++)
        {
            if(nHpCount > i)
            {
                m_hpList[i].gameObject.SetActive(true);
               m_hpList[i].drawHp(UI_Hp.E_HP.E_FULL);
            }
            else
            {
                m_hpList[i].gameObject.SetActive(false);
            }
        }

        if(nHpLastDrawHpImage == 1)
        {
            m_hpList[nHpCount].gameObject.SetActive(true);
            m_hpList[nHpCount].drawHp(UI_Hp.E_HP.E_HALF);
        }
    }
    public void toggle(bool bToggle)
    {
        gameObject.SetActive(bToggle);
    }

}
