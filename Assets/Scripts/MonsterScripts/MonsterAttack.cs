using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    MonsterManager monster_attack;
    [SerializeField]
    EnemyMoveSensor moveSensor;
    public Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(monster_attack.monsterAttack);
            animator.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            moveSensor.FollowTarget();
            animator.SetBool("Attack", false);
        }
    }
}
