using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{




    [SerializeField] private PlayerUnit m_cPlayer;

    [SerializeField] private CameraManger m_cCameraManager;


    [SerializeField]
    private StageObjectPool m_cObjectPoolData;
    [SerializeField]
    private EnemyWaveSpawnMangerBase m_cEnemyWaveSpawnManager;



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
        if (GameManager.Instance.isPlayerDataLoad())
            GameManager.Instance.cSaveManager.playerDataLoad(m_cPlayer);

         m_cCameraManager.init(m_cPlayer.transform);

        m_cObjectPoolData.init(GameManager.Instance.nStage);
        m_cEnemyWaveSpawnManager.init(GameManager.Instance.nStage);

    }





    public CameraManger cCameraManager
    {
        get
        {
            return m_cCameraManager;
        }
    }

    public StageObjectPool cStageObjectPool
    {
        get
        {
            return m_cObjectPoolData;
        }
    }


}
