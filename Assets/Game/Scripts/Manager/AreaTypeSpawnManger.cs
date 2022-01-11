using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTypeSpawnManger : EnemyWaveSpawnMangerBase
{


    public override void init(int nStage)
    {
        base.init(nStage);
        
    }

    public override void spawnerEntitiyAllClear()
    {
        base.spawnerEntitiyAllClear();
    }

    protected override void nextWave()
    {
        base.nextWave();
    }

    protected override void clear()
    {
        base.clear();
    }

    public void handleSpawn()
    {
        if (m_nCurrentWave == 0)
            spawnWave();
        else
            nextWave();
    }
}
