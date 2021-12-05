using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ingame : UI_View
{
    [SerializeField]
    private UI_PlayerInfo m_cUI_PlayerInfo;
    [SerializeField]
    private UI_BossHp m_cUI_BossHp;


    [SerializeField]
    private UI_InteractionText m_cUI_InteractionText;

    public override void init()
    {
        base.init();
        cUI_BossHp.init();
        cUI_InteractionText.init();

    }

    public void ingameStart()
    {
        toggle(true);

        cUI_PlayerInfo.toggle(true);

        cUI_InteractionText.toggle(false);
        cUI_BossHp.toggle(false);
    }



    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }



    public UI_PlayerInfo cUI_PlayerInfo
    {
        get
        {
            return m_cUI_PlayerInfo;
        }
    }

    public UI_BossHp cUI_BossHp
    {
        get
        {
            return m_cUI_BossHp;
        }
    }



    public UI_InteractionText cUI_InteractionText
    {
        get
        {
            return m_cUI_InteractionText;
        }
    }
}
