using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayerUnit m_cPlayer;

    [SerializeField] private CameraManger m_cCameraManager;


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

    public CameraManger cCameraManager
    {
        get
        {
            return m_cCameraManager;
        }
    }



}
