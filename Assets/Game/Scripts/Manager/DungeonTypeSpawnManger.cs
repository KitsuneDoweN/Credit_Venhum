using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonTypeSpawnManger : EnemyWaveSpawnMangerBase
{
    [SerializeField]
    private GameObject m_goClearDoor;

    public override void init(int nStage)
    {
        base.init(nStage);

    }



    public override void spawnerEntitiyAllClear()
    {
        base.spawnerEntitiyAllClear();
        if (isSpawnerAllClear)
            nextWave();
    }

    protected override void nextWave()
    {
        base.nextWave();
        if(m_nCurrentWave == m_nWave)
        {
            clear();
        }
        else
        {
            spawnWave();
        }
    }

    protected override void clear()
    {
        base.clear();
        m_goClearDoor.SetActive(false);
    }


}
