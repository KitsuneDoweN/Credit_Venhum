using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private UIManager m_UIManager;
    [SerializeField] private InputManager m_cInputManager;

    private StageManager m_cStageManger;

    private string m_strNextStageName;

    private Interaction m_cInteraction;


    public enum E_GAMESTATE
    {
        E_NONE = -1, E_TITLE, E_LODE , E_INGAME, E_OVER, E_CLEAR, E_TOTAL
    }

    private E_GAMESTATE m_eGameState;

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



    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        
        DOTween.defaultAutoPlay = AutoPlay.None;
        DontDestroyOnLoad(this);


        m_delGameEvent = new gameEvent[(int)E_GAMESTATE.E_TOTAL];


        m_delGameEvent[(int)E_GAMESTATE.E_TITLE] = titleEvet;
        m_delGameEvent[(int)E_GAMESTATE.E_INGAME] = inGameEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_LODE] = LoadEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_OVER] = gameOverEvent;
        m_delGameEvent[(int)E_GAMESTATE.E_CLEAR] = gameClearEvent;



        init();

        eGameState = E_GAMESTATE.E_INGAME;


    }

    private void init()
    {
        cUIManager.init();

        setStageManager(GameObject.FindObjectOfType<StageManager>());
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

    public void gameStart(string strStageName)
    {
        m_strNextStageName = strStageName;
        eGameState = E_GAMESTATE.E_LODE;
    }

    public void goTitle()
    {
        StartCoroutine(goTitleProcess());
    }

    private IEnumerator goTitleProcess()
    {
        cUIManager.allClear();
        cUIManager.cUI_GameLoading.toggle(true);

        cUIManager.cUI_GameLoading.startLoadAnimaiton();

        AsyncOperation sceneLoadAsync = SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        yield return sceneLoadAsync;

        cUIManager.cUI_GameLoading.stopLoadAnimation();

        cUIManager.cUI_GameLoading.toggle(false);

        eGameState = E_GAMESTATE.E_TITLE;
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
        StartCoroutine(inGameProcess());
    }

    private IEnumerator inGameProcess()
    {
        cUIManager.allClear();
        cUIManager.cUI_GameLoading.toggle(true);

        cUIManager.cUI_GameLoading.startLoadAnimaiton();

         AsyncOperation sceneLoadAsync = SceneManager.LoadSceneAsync(m_strNextStageName, LoadSceneMode.Single);
        yield return sceneLoadAsync;

        cUIManager.cUI_GameLoading.stopLoadAnimation();

        cUIManager.cUI_GameLoading.toggle(false);
        cUIManager.cUI_InGame.ingameStart();


       setStageManager(GameObject.FindObjectOfType<StageManager>());

        m_cStageManger.init();
        m_cInputManager.setPlayer(cStageManager.cPlayer);
        m_eGameState = E_GAMESTATE.E_LODE;
    }


    protected void inGameEvent()
    {
        cUIManager.cUI_InGame.ingameStart();
    }



    private void gameOverEvent()
    {
        cUIManager.allClear();
        m_UIManager.cUI_Gameover.toggle(true);
    }

    private void gameClearEvent()
    {
        cUIManager.allClear();
        cUIManager.cUI_GameClear.toggle(true);
    }





    public Interaction cInteraction
    {
        set
        {
            m_cInteraction = value;
            if (value == null)
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



}
