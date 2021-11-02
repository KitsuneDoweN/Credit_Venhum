using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSensor : MonoBehaviour
{
    [SerializeField]
    PlayerAttack m_attack;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyHp")
        {
            collision.transform.parent.GetComponent<MonsterManager>().TakeDamage(m_attack.attackDamage);
            Debug.Log("P_Attack");
        }
    }
}
