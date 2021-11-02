using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterMoveSensor : MonoBehaviour
{
    private MonsterManager monsterManager;
    private Transform target;
    public NavMeshAgent nav;
    public bool isChase;

    public Rigidbody2D rb;
    public Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 vec;
    public Vector2 lookVec;

    MonsterAttackSensor monsterattackBox;

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
            spriteRenderer.flipX = true;
            if (lookVec.x >= 0)
            {
                spriteRenderer.flipX = false;
            }
            //Ÿ���� ���Ϳ� ������ ���͸� ���ش�.
            lookVec = (Vector2)target.position - (Vector2)transform.position;
            lookVec = lookVec.normalized;
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
            //�ִϸ��̼� ����
        }
    }
    public void ChaseOn()
    { //�߰ݽý���
        if (isChase)
        {
            nav.isStopped = false;
            anim.SetBool("Walk", true);
            nav.velocity = Vector2.zero;
        }
    }
    public void ChaseOff() // ����
    { //�߰ݽý���
        nav.isStopped = true;
        nav.velocity = Vector2.zero;
    }
}
