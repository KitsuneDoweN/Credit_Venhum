using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObjectPool :MonoBehaviour
{

    private Dictionary<string, List<GameObject>> m_spawnObjectDictionary = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, int> m_spawnObjectCountDictionary = new Dictionary<string, int>();
    
    private Dictionary<string, ObjectPoolMaxSpawnData> m_maxObjectPoolDataDictionary = new Dictionary<string, ObjectPoolMaxSpawnData>();

    private List<string> m_spawnObjectKeyList = new List<string>();

    public void init(int nStage)
    {

        settingSpawnAtOnce(nStage);
        setttingSpawnObject();

    }

    private void settingSpawnAtOnce(int nStage)
    {
        List<SpawnData> spawnDataList = MasterStageEnemySpawnDataManger.Instance.getStageSpawnData(nStage);

        Dictionary<string, ObjectPoolMaxSpawnData> objectPoolDataDictionary = new Dictionary<string, ObjectPoolMaxSpawnData>();

        SpawnEntity cSpawnEntity;

        List<string> objectPoolDataKeyList = new List<string>();


        int nWave = 0;
        int i = 0;

        string strSpawnObjectKey;

        while (i < spawnDataList.Count)
        {
            if (nWave == spawnDataList[i].nWave)
            {
                cSpawnEntity = spawnDataList[i].cSapwnEntity;

                updateKeyList(ref m_spawnObjectKeyList, cSpawnEntity.strEntiyID);
                updateKeyList(ref objectPoolDataKeyList, cSpawnEntity.strEntiyID);

                if (!objectPoolDataDictionary.ContainsKey(cSpawnEntity.strEntiyID))
                {
                    ObjectPoolMaxSpawnData cNewObjectPool = new ObjectPoolMaxSpawnData();

                    cNewObjectPool.spawnObject = Resources.Load(cSpawnEntity.strEntiyID) as GameObject;
                    cNewObjectPool.nSpawnCountAtOnce = cSpawnEntity.nAmount;

                    objectPoolDataDictionary.Add(cSpawnEntity.strEntiyID, cNewObjectPool);

                }
                else
                {
                    objectPoolDataDictionary[cSpawnEntity.strEntiyID].nSpawnCountAtOnce += cSpawnEntity.nAmount;
                }
                i++;
            }
            else
            {
                if (m_maxObjectPoolDataDictionary == null)
                {
                    m_maxObjectPoolDataDictionary = objectPoolDataDictionary;
                }
                else
                {
                    foreach (string strKey in objectPoolDataKeyList)
                    {
                        if (!m_maxObjectPoolDataDictionary.ContainsKey(strKey))
                        {
                            ObjectPoolMaxSpawnData cCopyObjectPool = new ObjectPoolMaxSpawnData();
                            cCopyObjectPool.spawnObject = objectPoolDataDictionary[strKey].spawnObject;
                            cCopyObjectPool.nSpawnCountAtOnce = objectPoolDataDictionary[strKey].nSpawnCountAtOnce;


                            m_maxObjectPoolDataDictionary.Add(strKey, cCopyObjectPool);
                        }
                        else
                        {
                            if (m_maxObjectPoolDataDictionary[strKey].nSpawnCountAtOnce < objectPoolDataDictionary[strKey].nSpawnCountAtOnce)
                            {
                                m_maxObjectPoolDataDictionary[strKey].nSpawnCountAtOnce = objectPoolDataDictionary[strKey].nSpawnCountAtOnce;
                            }
                        }
                    }
                }


                objectPoolDataDictionary.Clear();
                objectPoolDataKeyList.Clear();

                nWave++;

            }
        }


        foreach (string strKey in m_spawnObjectKeyList)
        {
            Debug.Log(m_maxObjectPoolDataDictionary[strKey].spawnObject + "   " + m_maxObjectPoolDataDictionary[strKey].nSpawnCountAtOnce);
        }


    }

    private void setttingSpawnObject()
    {
        foreach(string strKey in m_spawnObjectKeyList)
        {
            m_spawnObjectDictionary[strKey] = new List<GameObject>();
            m_spawnObjectCountDictionary[strKey] = 0;


            for (int i = 0; i < m_maxObjectPoolDataDictionary[strKey].nSpawnCountAtOnce; i++)
            {
                addSpawnObject(strKey);
            }

           
        }
    }

    private void addSpawnObject(string strKey) 
    {
        

        GameObject goObj = Instantiate(m_maxObjectPoolDataDictionary[strKey].spawnObject);
        goObj.transform.parent = transform;
        goObj.gameObject.name = strKey + "_" + m_spawnObjectDictionary[strKey].Count;
        goObj.transform.localPosition = Vector3.zero;


        UnitBase unit = goObj.GetComponent<UnitBase>();
        if (unit)
        {
            unit.init();
        }

        goObj.SetActive(false);


        m_spawnObjectDictionary[strKey].Add(goObj);


        
    }



    private void updateKeyList(ref List<string> keyList,string strKey)
    {
        int i = 0;
        bool bNewKey = true;
        while (bNewKey && i < keyList.Count)
        {
            if (keyList[i] == strKey)
                bNewKey = false;

            i++;
        }
        if (bNewKey)
            keyList.Add(strKey);

    }





    public GameObject spawnObject(string strKey)
    {
        bool isSpawnAble = false;
        int nCount = 0;
        GameObject goSpawnObject;


        while(!isSpawnAble && nCount < m_spawnObjectDictionary[strKey].Count)
        {
            if (m_spawnObjectDictionary[strKey][nCount].transform.parent == transform)
                isSpawnAble = true;
            else
                nCount++;
        }


        if (!isSpawnAble)
        {
            addSpawnObject(strKey);
        }

        goSpawnObject = m_spawnObjectDictionary[strKey][nCount];

        goSpawnObject.transform.parent = null;
        goSpawnObject.SetActive(true);

        return m_spawnObjectDictionary[strKey][nCount];
    }

    public void returnObject(GameObject goObj)
    {
        goObj.transform.parent = transform;
    }


}
