using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSensor : MonoBehaviour
{
    private float moveSpeed = 1.5f;
    private float contactDistance = 0.8f;
    Transform target = null;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (target == null)
            return;
        if(Vector2.Distance(transform.position, target.position) > contactDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            target = collision.transform;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            target = null;
        }
    }



}
