using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawnMangerBase : MonoBehaviour
{

    [SerializeField]
    private List<SpawnData> m_spawnDataList;

    private List<SpawnData> m_pushedSpawnDataList;

    private List<Spawner> m_spawnerList;

    [SerializeField]
    protected int m_nWave;
    [SerializeField]
    protected int m_nCurrentWave;

    private int m_nClearSpawner;


    public  virtual void init(int nStage)
    {
        m_pushedSpawnDataList = new List<SpawnData>();

        m_spawnDataList = MasterStageEnemySpawnDataManger.Instance.getStageSpawnData(nStage);

        spawenrSetting();
        m_nCurrentWave = 0;
        m_nWave = m_spawnDataList[m_spawnDataList.Count - 1].nWave;
    }

    private void spawenrSetting()
    {
        m_spawnerList = new List<Spawner>();
        int nChildCount = 0;

        while (nChildCount < transform.GetChildCount())
        {
            Spawner cSpawner = transform.GetChild(nChildCount).GetComponent<Spawner>();
            cSpawner.gameObject.name = "Spawner_" + nChildCount;
            cSpawner.init(this);

            m_spawnerList.Add(cSpawner);
            nChildCount++;
        }
    }

    private void pushSpawnData(int nCurrentWave)
    {
        int nSpawnerIndex;
        SpawnEntity cSpawnEntity;


        while (m_spawnDataList.Count > 0 && nCurrentWave == m_spawnDataList[0].nWave)
        {
            nSpawnerIndex = m_spawnDataList[0].nSpawnIndex;
            cSpawnEntity = m_spawnDataList[0].cSapwnEntity;

            m_spawnerList[nSpawnerIndex].addSpawnEntity(cSpawnEntity);
            m_pushedSpawnDataList.Add(m_spawnDataList[0]);

            m_spawnDataList.RemoveAt(0);
        }

        foreach (Spawner cSpawner in m_spawnerList)
        {
            if (!cSpawner.isSpawnEntity())
                spawnerEntitiyAllClear();
        }
    }

    protected virtual void nextWave()
    {
        m_nCurrentWave++;
    }

    protected void spawnWave()
    {
        m_nClearSpawner = 0;
        pushSpawnData(m_nCurrentWave);
    }

    public virtual void spawnerEntitiyAllClear()
    {
        m_nClearSpawner++;
    }

    protected bool isSpawnerAllClear
    {
        get
        {
            return m_nClearSpawner == m_spawnerList.Count;
        }
    }

   protected virtual void clear()
    {
        Debug.Log("ClearWave");
    }

}
