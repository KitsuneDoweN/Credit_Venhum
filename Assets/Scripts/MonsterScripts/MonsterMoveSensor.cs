using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterMoveSensor : MonoBehaviour
{
    private Transform player;
    public NavMeshAgent nav;
    public bool isChase;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 vec;

    MonsterAttackSensor monsterattackBox;

    public void Init(Transform target, MonsterAttackSensor attackBox)
    {
        monsterattackBox = attackBox;
        player = target;
        nav.updateRotation = false;
        nav.updateUpAxis = false;

        ChaseOff();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isChase)
        {
            nav.SetDestination(player.position);
        }

        if (monsterattackBox.eAttack != MonsterAttackSensor.AttackState.e_none)
        {
            anim.SetBool("Walk", false);
            rb.velocity = Vector2.zero;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isChase)
        {
            return;
        }
        if (collision.gameObject == player.gameObject)
        {
            ChaseOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            ChaseOff();
            //애니메이션 멈춤
        }
    }

    public void ChaseOn()
    { //추격시스템
        if (isChase)
        {
            nav.isStopped = false;
            anim.SetBool("Walk", true);
            spriteRenderer.flipX = true;
            if (vec.x >= 0)
            {
                spriteRenderer.flipX = false;
            }
        }

    }
    public void ChaseOff()
    { //추격시스템
        nav.isStopped = true;
        nav.velocity = Vector2.zero;
    }
}
