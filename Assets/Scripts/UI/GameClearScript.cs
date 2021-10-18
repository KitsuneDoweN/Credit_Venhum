using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearScript : MonoBehaviour
{
    [SerializeField]
    MonsterManager m_monstermanager;
    public GameObject ClearUI;

    private void Start()
    {
        m_monstermanager = GetComponent<MonsterManager>();
    }

    private void Update()
    {
        //GameClear();
    }

    public void GameClear()
    {
        if (m_monstermanager.monsterDeathCount == 5)
        {
            ClearUI.SetActive(true);
        }
    }
}
