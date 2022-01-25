using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoWavePushTypeSpawnManager : EnemyWaveSpawnMangerBase
{
    public override void init(int nStage, int nCurrentWave)
    {
        m_pushedSpawnDataList = new List<SpawnData>();
        spawenrSetting();
        m_spawnDataList = new List<SpawnData>();
    }


    public void pushSpawnData(List<SpawnData> spawnDataList)
    {
        foreach(SpawnData cSpawnData in spawnDataList)
        {
            m_spawnDataList.Add(cSpawnData);
        }
        spawnWave();
    }

    

    
    
}
