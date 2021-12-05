using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private InputManager m_cInputManager;

    private StageManager m_cStageManger;

    private string m_strNextStageName;

    private Interaction m_cInteraction;

    [SerializeField] private bool m_bTestGame;

    public enum E_GAMESTATE
    {
        E_NONE = -1, E_TITLE, E_LODE, E_INGAME, E_OVER, E_CLEAR, E_TOTAL,
    }


    private E_GAMESTATE m_eGameState;

    private enum E_GAMESCENE
    {
        E_NONE = -1, E_TITLE, E_STAGE_0
    }

    private E_GAMESCENE m_eNextGameScene;


    public E_GAMESTATE eGameState
    {
        set
        {
            m_eGameState = value;

            m_delGameEvent[(int)m_eGameState]();
        }

        get
        {
            return m_eGameState;
        }
    }

    private delegate void gameEvent();
    private gameEvent[] m_delGameEvent;

    private string m_strClearGrogress = "ClearGrogress";




    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        nClearGrogress = 0;




        DOTween.defaultAutoPlay = AutoPlay.None;
        DontDestroyOnLoad(this);


        m_delGameEvent = new gameEvent[(int)E_GAMESTATE.E_TOTAL];


        m_delGameEvent[(int)E_GAMESTATE.E_TITLE] = titleEvet;
        m_delGameEvent[(int)E_GAMESTATE.E_INGAME] = inGameEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_LODE] = LoadEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_OVER] = gameOverEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_CLEAR] = gameClearEvent;



        init();


        if (!m_bTestGame)
            eGameState = E_GAMESTATE.E_TITLE;
        else
            eGameState = E_GAMESTATE.E_INGAME;

    }

    private void init()
    {
        cUIManager.init();
    }





    public UIManager cUIManager
    {
        get
        {
            return m_UIManager;
        }
    }

    public StageManager cStageManager
    {
        set
        {
            m_cStageManger = value;
        }
        get
        {
            return m_cStageManger;
        }
    }



    public void gameOver()
    {

    }




    public void setStageManager(StageManager cStageManager)
    {
        m_cStageManger = cStageManager;
        m_cStageManger.init();
        m_cInputManager.init(m_cStageManger.cPlayer);

    }

    private void titleEvet()
    {
        cUIManager.allClear();
        cUIManager.cUI_Title.toggle(true);
    }

    public void LoadEvent()
    {
        StartCoroutine(loadProcess());
    }

    private IEnumerator loadProcess()
    {
        cUIManager.allClear();
        cUIManager.cUI_GameLoading.toggle(true);

        cUIManager.cUI_GameLoading.startLoadAnimaiton();


        AsyncOperation sceneLoadAsync = SceneManager.LoadSceneAsync((int)m_eNextGameScene, LoadSceneMode.Single);
        yield return sceneLoadAsync;

        yield return new WaitForSeconds(1.0f);

        cUIManager.cUI_GameLoading.stopLoadAnimation();

        cUIManager.cUI_GameLoading.toggle(false);




        if (m_eNextGameScene == E_GAMESCENE.E_TITLE)
        {
            eGameState = E_GAMESTATE.E_TITLE;
            yield break;
        }

        eGameState = E_GAMESTATE.E_INGAME;

    }


    protected void inGameEvent()
    {
        cUIManager.allClear();

        setStageManager(GameObject.FindObjectOfType<StageManager>());

        cUIManager.ingameStart();


        cUIManager.cUI_FadeInOut.toggle(true);
        cUIManager.cUI_FadeInOut.draw(true);

        cStageManager.cPlayer.isControl = false;
        cStageManager.cDirectionTalk.directionStart();
    }



    private void gameOverEvent()
    {
        cStageManager.cBGM.stop();
        m_UIManager.cUI_Gameover.toggle(true);
        Invoke("goIngame", 1.0f);
    }

    private void gameClearEvent()
    {
        cStageManager.cBGM.stop();
        StartCoroutine(clearEventCoroutine(0.1f, 0.5f));

    }

    private IEnumerator clearEventCoroutine(float fTimeScale , float fTime)
    {
        Time.timeScale = fTimeScale;

        yield return new WaitForSecondsRealtime(fTime);
        Time.timeScale = 1.0f;
        yield return new WaitForSeconds(10.0f);


        cStageManager.cPlayer.stop();
        cStageManager.cPlayer.isControl = false;

        cUIManager.cUI_GameClear.toggle(true);
        Invoke("goTitle", 3.0f);
    }



    public void goTitle()
    {
        m_eNextGameScene = E_GAMESCENE.E_TITLE;
        eGameState = E_GAMESTATE.E_LODE;
    }


    public void goIngame()
    {
        m_eNextGameScene = E_GAMESCENE.E_STAGE_0;
        eGameState = E_GAMESTATE.E_LODE;
    }


    public Interaction cInteraction
    {
        set
        {
            m_cInteraction = value;
            if (value == null || m_cInteraction.trIcon == null)
            {
                cUIManager.cUI_Dynamic.toggle(false);
                return;
            }


            cUIManager.cUI_Dynamic.toggle(true);
            cUIManager.cUI_Dynamic.drawInteractionIncon(m_cInteraction.trIcon.position);

        }
        get
        {
            return m_cInteraction;
        }
    }

    private bool m_bPauseControl;

    public void pause()
    {
        if (m_eGameState != E_GAMESTATE.E_INGAME)
            return;

        Time.timeScale = 0;

        m_bPauseControl = cStageManager.cPlayer.isControl;
        cStageManager.cPlayer.isControl = false;
    }

    public void resume()
    {
        if (m_eGameState != E_GAMESTATE.E_INGAME)
            return;

        Time.timeScale = 1;

        cStageManager.cPlayer.isControl = m_bPauseControl;

    }

    public void interActionEvent()
    {
        m_cInteraction.interactionEvent.Invoke();
    }

    private void saveClearGrogress(int nData)
    {
        PlayerPrefs.SetInt(m_strClearGrogress, 0);
    }

    public int nClearGrogress
    {
        set
        {
            PlayerPrefs.SetInt(m_strClearGrogress, value);
        }
        get
        {
            return PlayerPrefs.GetInt(m_strClearGrogress);
        }
    }

}
