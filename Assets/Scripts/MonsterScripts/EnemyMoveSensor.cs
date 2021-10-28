using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSensor : MonoBehaviour
{
    private float moveSpeed = 1.5f;
    Transform target = null;

    Rigidbody2D rb;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private Vector2 vec;
    [SerializeField]
    EnemyAttackSensor monsterattackBox;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(monsterattackBox.eAttack != EnemyAttackSensor.AttackState.e_none)
        {
            anim.SetBool("Walk", false);
            rb.velocity = Vector2.zero;
            return;
        }
        FollowTarget();
        updateAnimation();
    }

    public void FollowTarget()
    {
        rb.velocity = Vector2.zero;
        if (target == null)
            return;
        setDirection((Vector2)(target.position - transform.position).normalized);
        rb.velocity = vec * moveSpeed;
        //가고자하는 방향으로 애니메이션이 안움직인다
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            target = collision.transform;
        }
        
    }
    private void updateAnimation()
    {
        anim.SetBool("Walk", true);
        spriteRenderer.flipX = true;

        if (target == null)
        {
            anim.SetBool("Walk", false);
        }

        if(vec.x >= 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
        }
    }

    private void setDirection(Vector2 dir)
    {
        vec = dir;
        spriteRenderer.flipX = true;
        monsterattackBox.setAttackPos(false);

        if (vec.x >= 0)
        {
            spriteRenderer.flipX = false;
            monsterattackBox.setAttackPos(true);

        }


    }



}
