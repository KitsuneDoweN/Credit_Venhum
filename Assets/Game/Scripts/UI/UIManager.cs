using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UI_Ingame m_cUI_Ingame;
    [SerializeField] private UI_Title m_cUI_Title;
    [SerializeField] private UI_GameLoading m_cUI_GameLoading;
    [SerializeField] private UI_Gameover m_cUI_Gameover;
    [SerializeField] private UI_GameClear m_cUI_GameClear;
    [SerializeField] private UI_Dynamic m_cUI_Dynamic;



    public void init()
    {
        m_cUI_Ingame.init();
        //m_cUI_Title.init();
        //m_cUI_GameLoading.init();
        //m_cUI_Gameover.init();
        //m_cUI_GameClear.init();
        m_cUI_Dynamic.init();
    }

    public UI_Ingame cUI_InGame
    {
        get
        {
            return m_cUI_Ingame;
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

    public void gameStart()
    {
        m_cUI_Ingame.ingameStart();
    }

    public void allClear()
    {
        cUI_InGame.toggle(false);
        cUI_Title.toggle(false);
        cUI_GameLoading.toggle(false);
        cUI_Gameover.toggle(false);
        cUI_GameClear.toggle(false);
        cUI_Dynamic.toggle(false);
    }

}
