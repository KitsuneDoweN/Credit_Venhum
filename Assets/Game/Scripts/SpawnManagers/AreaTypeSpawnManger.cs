using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AreaTypeSpawnManger : EnemyWaveSpawnMangerBase
{
    [SerializeField]
    private UnityEvent[] m_waveClearEvents;

    public override void init(int nStage, int nCurrentWave)
    {
        base.init(nStage, nCurrentWave);
        skipWave();
        m_nCurrentWave--;


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
        if (m_waveClearEvents[m_nCurrentWave] != null)
            m_waveClearEvents[m_nCurrentWave].Invoke();
    }

    public void handleSpawn()
    {
        nextWave();
    }
}
