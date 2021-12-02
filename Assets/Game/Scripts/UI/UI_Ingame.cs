using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ingame : UI_View
{
    [SerializeField]
    private UI_PlayerInfo m_cUI_PlayerInfo;
    //[SerializeField]
    //private UI_BossHp m_cUI_BossHp;
    [SerializeField]
    private UI_Talk m_cUI_Talk;

    [SerializeField]
    private UI_InteractionText m_cUI_InteractionText;

    public override void init()
    {
        base.init();
        // m_cUI_BossHp.init();


        cUI_InteractionText.init();
        cUI_Talk.init();
    }

    public void ingameStart()
    {
        toggle(true);

        cUI_PlayerInfo.toggle(true);
        cUI_Talk.toggle(false);
        cUI_InteractionText.toggle(false);
        //m_cUI_BossHp.toggle(false);
    }



    public override void toggle(bool bToggle)
    {
        base.toggle(bToggle);
    }




    //public void boosHpToggle(bool bToggle)
    //{
    //    m_cUI_BossHp.toggle(bToggle);
    //}

    public UI_PlayerInfo cUI_PlayerInfo
    {
        get
        {
            return m_cUI_PlayerInfo;
        }
    }

    //public UI_BossHp cUI_BossHp
    //{
    //    get
    //    {
    //        return m_cUI_BossHp;
    //    }
    //}

    public UI_Talk cUI_Talk
    {
        get
        {
            return m_cUI_Talk;
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
