using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Save : UI_View, IUpdate
{

    private int m_nDotCount = 0;
    [SerializeField]
    private TextMeshProUGUI m_text;
    [SerializeField]
    private float m_fTick;

    private float m_fTime;

    private string m_strText = "save";

    private int m_nDotLoopCount = 0;

    private bool m_bOnUpdateProcesses = false;

    public string id
    {
        get
        {
            return gameObject.name;
        }
    }

    public override void init()
    {
        base.init();
        UpdateManager.Instance.addProcesses(this);
        m_bOnUpdateProcesses = true;
    }

    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
        if(m_bToogle == true)
        {
            m_fTime = .0f;
            m_nDotLoopCount = 0;
        }
    }

    public void updateProcesses()
    {
        if (!m_bToogle)
            return;

        m_text.text = m_strText;

        if (m_fTime >= m_fTick)
        {
            for (int i = 0; i < m_nDotLoopCount; i++)
            {
                m_text.text += ".";
            }
            m_nDotLoopCount++;
            if (m_nDotLoopCount > 3)
                m_nDotLoopCount = 1;
        }

        m_fTime += Time.deltaTime;

    }

    private void OnDisable()
    {
        if(m_bOnUpdateProcesses)
        UpdateManager.Instance.removeProcesses(this);
    }
}
