using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    float monsterHp = 5.0f;
    public float monsterAttack = 1;
    public GameObject monster;
    
    public float monsterDeathCount = 0;

    private void Update()
    {
        //GameClear();
    }


    public void TakeDamage(float damage)
    {
        monsterHp = monsterHp - damage;
        if(monsterHp <= 0)
        {
            Destroy(monster);
            monsterDeathCount += 1;
        }
    }

    

    
}
