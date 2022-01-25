using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayerUnit m_cPlayer;

    [SerializeField] private CameraManger m_cCameraManager;


    [SerializeField]
    private StageEventManager m_cStageEventManager;

    [SerializeField]
    private StageObjectPool m_cObjectPoolData;
    [SerializeField]
    private EnemyWaveSpawnMangerBase m_cEnemyWaveSpawnManager;

    private int m_nSkipWave;

    [SerializeField]
    private BossRoom m_cBossRoom;

    [SerializeField]
    private SoundPlay m_cBGM;

    private bool m_bInitProcessed = false;

    public bool isInitProcessed
    {
        get
        {
            return m_bInitProcessed;
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
        if (m_cStageEventManager != null)
            m_cStageEventManager.init(this);

        if (m_cObjectPoolData != null)
            m_cObjectPoolData.init(GameManager.Instance.nStage);

        if (m_cEnemyWaveSpawnManager != null)
            m_cEnemyWaveSpawnManager.init(GameManager.Instance.nStage, m_nSkipWave);

        if (m_cBossRoom != null)
            m_cBossRoom.init();


        m_cPlayer.init();


      
        if (GameManager.Instance.isPlayerDataLoad())
            GameManager.Instance.cSaveManager.playerDataLoad(m_cPlayer);

        
        m_cCameraManager.init(m_cPlayer.transform);

        
        m_cBGM.init();
        m_cBGM.play();

        m_bInitProcessed = true;
    }


    public void skipWave()
    {
        m_nSkipWave++;
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

    public void save()
    {
        GameManager.Instance.cSaveManager.save();
    }

    private void OnDestroy()
    {
        GameManager.Instance.cStageManager = null;
    }

}
