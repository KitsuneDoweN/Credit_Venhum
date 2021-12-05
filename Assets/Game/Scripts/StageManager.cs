using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayerUnit m_cPlayer;

    [SerializeField] private CameraManger m_cCameraManager;

    [SerializeField]
    private DirectionTalk m_cDirectionTalk;

    [SerializeField]
    private AreaManager m_cAreaManager;

    [SerializeField]
    private OneSound m_cBgm;

    public OneSound cBGM
    {
        get
        {
            return m_cBgm;
        }
    }

    public AreaManager cAreaManager
    {
        get
        {
            return m_cAreaManager;
        }
    }

    public PlayerUnit cPlayer
    {
        get
        {
            return m_cPlayer;
        }
    }

    public void init()
    {
        

        m_cPlayer.init();
        cAreaManager.init();
        m_cCameraManager.init(m_cPlayer.transform);
        m_cBgm.init();
        m_cBgm.play();

    }

    public DirectionTalk cDirectionTalk
    {
        get
        {
            return m_cDirectionTalk;
        }
    }


    public CameraManger cCameraManager
    {
        get
        {
            return m_cCameraManager;
        }
    }



}
