using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDataComponent : MonoBehaviour
{
    [SerializeField]
    private SpawnData m_cSpawnData;

    public SpawnData cSpawnData
    {
        get
        {
            return m_cSpawnData;
        }
    }

    public void setSpawnIndex(int nIndex)
    {
        m_cSpawnData.setIndex(nIndex);
    }

}
