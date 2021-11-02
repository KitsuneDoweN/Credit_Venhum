using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAllManager : MonoBehaviour
{
    public List<MonsterManager> monsterManagers;
    public Transform target;

    public float DeathCount = 0;

    void Start()
    {
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Enemy");
        monsterManagers = new List<MonsterManager>();
        monsterManagers.Capacity = gameObject.Length;
        //Capacity = 리스트 전체크기
        foreach (GameObject game in gameObject)
        {
            monsterManagers.Add(game.GetComponent<MonsterManager>());
        }
        foreach(MonsterManager manager in monsterManagers)
        {
            manager.Init(target, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
