using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ingame : UI_View
{
    [SerializeField]
    private UI_HpManager m_cUI_HPManager;
    [SerializeField]
    private UI_BossHp m_cUI_BossHp;


    public override void init()
    {
        base.init();

        m_cUI_HPManager.init();
        m_cUI_BossHp.init();

        toggle(false);
    }

    public void ingameStart()
    {
        toggle(true);
        m_cUI_HPManager.toggle(true);
        m_cUI_BossHp.toggle(false);
    }



    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }

    public void playerHpToggle(bool bToggle)
    {
        m_cUI_HPManager.toggle(bToggle);
    }

    public void boosHpToggle(bool bToggle)
    {
        m_cUI_BossHp.toggle(bToggle);
    }

    public UI_HpManager cUI_HpManager
    {
        get
        {
            return m_cUI_HPManager;
        }
    }

    public UI_BossHp cUI_BossHp
    {
        get
        {
            return m_cUI_BossHp;
        }
    }
}
