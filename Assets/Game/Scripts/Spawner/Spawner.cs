using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IUpdate
{
    [SerializeField]
    private Vector2 m_v2SpawnArea;

    private Vector2 m_v2SpawnMin;
    private Vector2 m_v2SpawnMax;

    private Vector2 m_v2RandomSpawnPoint;


    private List<SpawnEntity> m_spawnEntityList;




    private EnemyWaveSpawnMangerBase m_cStageSpawnManager;

    private int m_nSpawnEntityCount;
    private int m_nSpawnEntityDieCount;

    public string id
    {
        get
        {
            return gameObject.name;
        }
    }

    public void init(EnemyWaveSpawnMangerBase cStageSpawnManager)
    {
        m_cStageSpawnManager = cStageSpawnManager;

        m_spawnEntityList = new List<SpawnEntity>();


        Vector2 v2Center = (Vector2)gameObject.transform.position;

        float fHalfAreaX = m_v2SpawnArea.x * 0.5f;
        float fHalfAreaY = m_v2SpawnArea.y * 0.5f;

        m_v2SpawnMin = new Vector2(v2Center.x - fHalfAreaX, v2Center.y - fHalfAreaY);
        m_v2SpawnMax = new Vector2(v2Center.x + fHalfAreaX, v2Center.y + fHalfAreaY);

        UpdateManager.Instance.addProcesses(this);
    }


    public void updateProcesses()
    {
        if (m_spawnEntityList.Count == 0)
            return;


        SpawnEntity cSpawnEntity = m_spawnEntityList[0];

        for (int i = 0; i < cSpawnEntity.nAmount; i++)
        {
            spawnEnemyEntiy(GameManager.Instance.cStageManager.cStageObjectPool.spawnObject(cSpawnEntity.strEntiyID));
            m_nSpawnEntityCount++;
        }


        m_spawnEntityList.RemoveAt(0);
    }

    private void spawnEnemyEntiy(GameObject entity)
    {
        
        entity.transform.position = getRandomRange();
        entity.transform.rotation = Quaternion.identity;

        UnitEnemyBase cEnemy = entity.GetComponent<UnitEnemyBase>();

        cEnemy.handleSpawn(this, GameManager.Instance.cStageManager.cPlayer);

    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector3)m_v2SpawnArea);
    }


    private Vector2 getRandomRange()
    {
        m_v2RandomSpawnPoint.x = Random.Range(m_v2SpawnMin.x, m_v2SpawnMax.x);
        m_v2RandomSpawnPoint.y = Random.Range(m_v2SpawnMin.y, m_v2SpawnMax.y);

        return m_v2RandomSpawnPoint;
    }

    public void addSpawnEntity(SpawnEntity entity)
    {
        m_spawnEntityList.Add(entity);
    }

    public void unitDie()
    {
        m_nSpawnEntityDieCount++;
        if (m_nSpawnEntityDieCount == m_nSpawnEntityCount && !isSpawnEntity())
            m_cStageSpawnManager.spawnerEntitiyAllClear();
    }

    public bool isSpawnEntity()
    {
        return m_spawnEntityList.Count > 0;
    }



    private void OnDestroy()
    {
        UpdateManager.Instance.removeProcesses(this);
    }

}
