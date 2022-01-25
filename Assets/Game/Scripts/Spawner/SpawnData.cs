using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnData
{
    [SerializeField]
    private SpawnEntity m_cSpawnEntity;

    [SerializeField]
    private int m_nSpawnIndex;

    [SerializeField]
    private int m_nWave;

    public SpawnEntity cSapwnEntity
    {
        get
        {
            return m_cSpawnEntity;
        }
    }

    public int nSpawnIndex
    {
        get
        {
            return m_nSpawnIndex;
        }
    }

    public int nWave
    {
        get
        {
            return m_nWave;
        }
    }

    public void init(SpawnEntity spawnEntity, int spawnIndex, int nWave)
    {
        m_cSpawnEntity = spawnEntity;
        m_nSpawnIndex = spawnIndex;
        m_nWave = nWave;
    }

    public void setIndex(int nIndex)
    {
        m_nSpawnIndex = nIndex;
    }
}
