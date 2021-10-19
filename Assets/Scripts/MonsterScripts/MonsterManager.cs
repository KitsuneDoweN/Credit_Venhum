using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    float monsterHp = 5.0f;
    public float monsterAttack = 1;
    public GameObject monster;
    
    public static float monsterDeathCount = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
    }


    public void TakeDamage(float damage)
    {
        monsterHp = monsterHp - damage;
        if(monsterHp <= 0)
        {
            monsterDeathCount += 1;
            Destroy(monster);
        }
    }

    

    
}
