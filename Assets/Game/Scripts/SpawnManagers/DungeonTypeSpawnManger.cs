using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DungeonTypeSpawnManger : EnemyWaveSpawnMangerBase
{
    [SerializeField]
    private UnityEvent m_dungeonClearEvent;

    public override void init(int nStage, int nCurrentWave)
    {
        base.init(nStage, nCurrentWave);
        skipWave();
        spawnWave();
    }



    public override void spawnerEntitiyAllClear()
    {
        base.spawnerEntitiyAllClear();
        if (isSpawnerAllClear)
            waveClear();
    }

    protected override void nextWave()
    {
        base.nextWave();
    }

    protected override void waveClear()
    {
        base.waveClear();
        if (m_nCurrentWave == m_nWave)
            m_dungeonClearEvent.Invoke();
        else
        {
            nextWave();
        }
    }


}
