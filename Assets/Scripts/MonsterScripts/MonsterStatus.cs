using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField]
    float monsterHp;
    public float monsterAttack;
    public float monsterSpeed;
    public GameObject monster;
    
    public static float monsterDeathCount = 0;

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
