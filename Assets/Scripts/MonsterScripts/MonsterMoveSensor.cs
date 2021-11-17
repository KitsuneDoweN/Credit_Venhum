using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterMoveSensor : MonoBehaviour
{
    private MonsterManager monsterManager;
    private MonsterAttackSensor monsterattackBox;

    private Transform target;

    public NavMeshAgent nav;
    public bool isChase;

    public Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 vec;
    public Vector2 lookVec;

    public GameObject mark;
    public GameObject mark2;

    public void Init(Transform target, MonsterAttackSensor attackBox, MonsterManager manager)
    {
        monsterattackBox = attackBox;
        monsterManager = manager;
        this.target = target;
        nav.updateRotation = false;
        nav.updateUpAxis = false;

        ChaseOff();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(monsterManager.isControl == false)
        {
            return;
        }
        if(isChase)
        {
            nav.SetDestination(target.position);
            if (monsterattackBox.eAttack != MonsterAttackSensor.AttackState.e_attack)
            {
                lookVecUpdate();
            }
        }
        
        if (monsterattackBox.eAttack != MonsterAttackSensor.AttackState.e_none)
        {
            anim.SetBool("Walk", false);
            rb.velocity = Vector2.zero;
            return;
        }
    }
    private void lookVecUpdate()
    //타겟의 벡터와 몬스터의 벡터를 빼준다.
    {
        lookVec = (Vector2)target.position - (Vector2)transform.position;
        lookVec = lookVec.normalized;
        spriteRenderer.flipX = true;
        if (lookVec.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //Invoke("markTime", 1f);
        if (!isChase)
        {
            return;
        }
        if (collision.gameObject == target.gameObject)
        {
            ChaseOn();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject == target.gameObject)
        {
            anim.SetBool("Walk", false);
            ChaseOff();
            //애니메이션 멈춤
        }
    }
    public void ChaseOn()
    { //추격시스템
        if (isChase)
        {
            mark.SetActive(false);
            mark2.SetActive(true);
            nav.isStopped = false;
            anim.SetBool("Walk", true);
            nav.velocity = Vector2.zero;
        }
    }
    public void ChaseOff()
    { //추격시스템
        mark.SetActive(true);
        mark2.SetActive(false);
        nav.isStopped = true;
        nav.velocity = Vector2.zero;
    }
    //private void markTime()
    //{
    //    mark.SetActive(true);
    //    mark2.SetActive(false);
    //}
}
