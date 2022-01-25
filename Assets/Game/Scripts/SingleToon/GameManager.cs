using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : SingleToon<GameManager>
{

    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private InputManager m_cInputManager;
    [SerializeField] private SaveManger m_cSaveManager;

    private StageManager m_cStageManger;

    private string m_strNextStageName;

    private Interaction m_cInteraction;

    [SerializeField] private bool m_bTestGame;


    private UnityEvent m_clearEvent;

    public enum E_GAMESTATE
    {
        E_NONE = -1, E_TITLE, E_LODE, E_INGAME, E_OVER, E_CLEAR, E_TOTAL,
    }


    private E_GAMESTATE m_eGameState;

    [SerializeField]
    public enum E_GAMESCENE
    {
        E_NONE = -1, E_TITLE, E_STAGE_CITY, E_STAGE_DUGGEN_BEGINE,
        E_STAGE_DUGGEN_LOOPTYPE0, E_STAGE_DUGGEN_LOOPTYPE1,
        E_STAGE_DUGGEN_ROOST, E_STAGE_DUGGEN_BOSSROOM, E_RANDOM_DUGGEN
    }

    private E_GAMESCENE m_eScene;




    public E_GAMESTATE eGameState
    {
        private set
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

    private int m_nStage;



    private void Start()
    {
        init();

    }



    protected override bool init()
    {
        bool bOverlap =  !base.init();

        if (bOverlap)
            return false;



        DOTween.defaultAutoPlay = AutoPlay.None;


        m_delGameEvent = new gameEvent[(int)E_GAMESTATE.E_TOTAL];

        m_delGameEvent[(int)E_GAMESTATE.E_TITLE] = titleEvet;
        m_delGameEvent[(int)E_GAMESTATE.E_INGAME] = inGameEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_LODE] = LoadEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_OVER] = gameOverEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_CLEAR] = gameClearEvent;



        
        cUIManager.init();
        m_cInputManager.init();
        m_cSaveManager.init();





        if (!m_bTestGame)
            eGameState = E_GAMESTATE.E_TITLE;
        else
            eGameState = E_GAMESTATE.E_INGAME;

        return true;
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



    public void gameover()
    {
        eGameState = E_GAMESTATE.E_OVER;
    }




    public void setStageManager(StageManager cStageManager)
    {
        m_cStageManger = cStageManager;
        m_cStageManger.init();
        
        m_cInputManager.setPlayer(m_cStageManger.cPlayer);

    }

    private void titleEvet()
    {
        cUIManager.allClear();
        cUIManager.cUI_Title.toggle(true);
    }

    public void NextStage(E_GAMESCENE eStage, bool bClearStage)
    {
        m_eScene = eStage;
        if (bClearStage)
            m_nStage++;

        eGameState = E_GAMESTATE.E_LODE;
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


        AsyncOperation sceneLoadAsync = SceneManager.LoadSceneAsync((int)m_eScene, LoadSceneMode.Single);
        yield return sceneLoadAsync;



        if (m_eScene == E_GAMESCENE.E_TITLE)
        {
            eGameState = E_GAMESTATE.E_TITLE;
            yield break;
        }

        eGameState = E_GAMESTATE.E_INGAME;

        yield return null;

        cUIManager.cUI_GameLoading.stopLoadAnimation();

        cUIManager.cUI_GameLoading.toggle(false);


    }





    protected void inGameEvent()
    {
        cUIManager.allClear();

        setStageManager(GameObject.FindObjectOfType<StageManager>());

        cUIManager.ingameStart();


        cUIManager.cUI_FadeInOut.toggle(false);
        

        cStageManager.cPlayer.isControl = true;
       
    }



    private void gameOverEvent()
    {
        m_UIManager.cUI_Gameover.toggle(true);
        Invoke("goIngame", 1.0f);
    }

    private void gameClearEvent()
    {

        cUIManager.cUI_GameClear.toggle(true);
        Invoke("goTitle", 3.0f);
    }




    public void goTitle()
    {
        
        if(m_eGameState == E_GAMESTATE.E_TITLE)
        {
            cUIManager.cUI_Option.toggle(false);
            return;
        }

        cUIManager.allClear();
        m_eScene = E_GAMESCENE.E_TITLE;
        eGameState = E_GAMESTATE.E_LODE;
    }


    public void goIngame()
    {
        m_nStage = cSaveManager.getStage();
        m_eScene = cSaveManager.getSenceID();

        eGameState = E_GAMESTATE.E_LODE;
    }

    public void gameOver()
    {

    }

    public void gameClear()
    {
        eGameState = E_GAMESTATE.E_CLEAR;
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
        if (eGameState != E_GAMESTATE.E_INGAME)
            return;

        Time.timeScale = 0;

        m_bPauseControl = cStageManager.cPlayer.isControl;
        cStageManager.cPlayer.isControl = false;
    }

    public void pose()
    {
        if (eGameState != E_GAMESTATE.E_INGAME || cStageManager == null)
            return;


        Time.timeScale = 1;

        cStageManager.cPlayer.isControl = m_bPauseControl;

    }

    public void interActionEvent()
    {
        m_cInteraction.interactionEvent.Invoke();
    }






    public SaveManger cSaveManager
    {
        get
        {
            return m_cSaveManager;
        }
    }

    public int nStage
    {
        get
        {
            return m_nStage;
        }
    }

    public GameManager.E_GAMESCENE eScene
    {
        get
        {
            return m_eScene;
        }
    }


    public bool isPlayerDataLoad()
    {
        return (m_eScene == cSaveManager.getSenceID() && nStage == cSaveManager.getStage());
    }

    public void exitGame()
    {
        Application.Quit();
    }

}
