using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterStageEnemySpawnDataManger : SingleToon<MasterStageEnemySpawnDataManger>
{
    [SerializeField]
    private CSV m_cSpawnEnemyData;



    private List<List<SpawnData>> m_stageSpawnList;



    private string m_strStageID = "StageID";
    private string m_strWaveID = "Wave";
    private string m_strSpwnerID = "Spawner";
    private string m_strObjectName = "ObjectName";
    private string m_strAmount = "Amount";


    private void Awake()
    {
        init();
    }

    protected override void init()
    {
        base.init();
        m_cSpawnEnemyData.init();
        settingStageSpawnData();
    }

    private void settingStageSpawnData()
    {
        m_stageSpawnList = new List<List<SpawnData>>();
        int nIndex = 0;
        int nStageIndex = 0;

        List<SpawnData> newSpawnDataList = new List<SpawnData>();

        m_stageSpawnList.Add(newSpawnDataList);


        while (nIndex < m_cSpawnEnemyData.nListCount)
        {


            if (nStageIndex == (int)m_cSpawnEnemyData.getData( m_strStageID, nIndex))
            {
                SpawnData newSpawnData = new SpawnData();
                SpawnEntity newSpawnEntity = new SpawnEntity();

                string strEntityID = (string)m_cSpawnEnemyData.getData(m_strObjectName, nIndex);
                int nAmount = (int)m_cSpawnEnemyData.getData(m_strAmount, nIndex);
                int nSpawnIndex = (int)m_cSpawnEnemyData.getData(m_strSpwnerID, nIndex);
                int nWaveID = (int)m_cSpawnEnemyData.getData(m_strWaveID, nIndex);

                newSpawnEntity.init(strEntityID, nAmount);
                newSpawnData.init(newSpawnEntity, nSpawnIndex, nWaveID);

                newSpawnDataList.Add(newSpawnData);


                nIndex++;
            }
            else
            {
                newSpawnDataList = new List<SpawnData>();

                nStageIndex++;
                m_stageSpawnList.Add(newSpawnDataList);
            }

        }
        drawAllData();
    }

    private void drawAllData()
    {
        Debug.Log("===========================================");
        Debug.Log("Total Enemy Data");
        Debug.Log("===========================================");
        int nStage = 0;
        int nSpawnDataCount;

        while (nStage < m_stageSpawnList.Count)
        {
            nSpawnDataCount = 0;
            Debug.Log("Stage : " + nStage);
            while (nSpawnDataCount < m_stageSpawnList[nStage].Count)
            {
                Debug.Log(m_stageSpawnList[nStage][nSpawnDataCount]);
                nSpawnDataCount++;
            }
            Debug.Log("===========================================");
            nStage++;
        }
    }

    public List<SpawnData> getStageSpawnData(int nStage)
    {
        return m_stageSpawnList[nStage];
    }

   
    

}
