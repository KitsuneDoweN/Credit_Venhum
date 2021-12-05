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

        m_cCameraManager.init(m_cPlayer.transform);


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
