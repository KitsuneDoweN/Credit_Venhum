using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UI_Ingame m_cUI_Ingame;
    [SerializeField] private UI_Title m_cUI_Title;
    [SerializeField] private UI_GameLoading m_cUI_GameLoading;
    [SerializeField] private UI_Gameover m_cUI_Gameover;
    [SerializeField] private UI_GameClear m_cUI_GameClear;
    [SerializeField] private UI_Dynamic m_cUI_Dynamic;
    [SerializeField] private UI_Option m_cUI_Option;
    [SerializeField] private UI_FadeInOut m_cUI_FadeInOut;
    [SerializeField] private UI_Talk m_cUI_Talk;
    [SerializeField] private UI_Save m_cUI_Save;
    public void init()
    {
        m_cUI_Ingame.init();
        m_cUI_Title.init();
        m_cUI_GameLoading.init();
        m_cUI_Gameover.init();
        m_cUI_GameClear.init();
        m_cUI_Dynamic.init();
        m_cUI_Option.init();
        m_cUI_FadeInOut.init();
        m_cUI_Talk.init();
        m_cUI_Save.init();

    }

    public UI_Talk cUI_Talk
    {
        get
        {
            return m_cUI_Talk;
        }
    }

    public UI_Ingame cUI_InGame
    {
        get
        {
            return m_cUI_Ingame;
        }
    }

    public UI_Option cUI_Option
    {
        get
        {
            return m_cUI_Option;
        }
    }

    public UI_Title cUI_Title
    {
        get
        {
            return m_cUI_Title;
        }
    }

    public UI_GameLoading cUI_GameLoading
    {
        get
        {
            return m_cUI_GameLoading;
        }
    }

    public UI_Gameover cUI_Gameover
    {
        get
        {
            return m_cUI_Gameover;
        }
    }

    public UI_GameClear cUI_GameClear
    {
        get
        {
            return m_cUI_GameClear;
        }
    }

    public UI_Dynamic cUI_Dynamic
    {
        get
        {
            return m_cUI_Dynamic;
        }
    }
    public UI_FadeInOut cUI_FadeInOut
    {
        get
        {
            return m_cUI_FadeInOut;
        }
    }

    public UI_Save cUI_Save
    {
        get
        {
            return m_cUI_Save;
        }
    }



    public void ingameStart()
    {
        cUI_InGame.ingameStart();

    }

    public void allClear()
    {
        cUI_InGame.toggle(false);
        cUI_Title.toggle(false);
        cUI_GameLoading.toggle(false);
        cUI_Gameover.toggle(false);
        cUI_GameClear.toggle(false);
        cUI_Dynamic.toggle(false);
        cUI_Option.toggle(false);
    }




}
