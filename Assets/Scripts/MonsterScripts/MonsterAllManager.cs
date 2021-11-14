using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAllManager : MonoBehaviour
{
    public List<MonsterManager> monsterManagers;
    public List<GunMonsterManager> gunmonsterManagers;

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


        GameObject[] gameObject2 = GameObject.FindGameObjectsWithTag("Enemy_Gun");
        gunmonsterManagers = new List<GunMonsterManager>();
        gunmonsterManagers.Capacity = gameObject2.Length;
        //Capacity = 리스트 전체크기
        foreach (GameObject game2 in gameObject2)
        {
            gunmonsterManagers.Add(game2.GetComponent<GunMonsterManager>());
        }
        foreach (GunMonsterManager manager2 in gunmonsterManagers)
        {
            manager2.Init(target, this);
        }
    }
}
