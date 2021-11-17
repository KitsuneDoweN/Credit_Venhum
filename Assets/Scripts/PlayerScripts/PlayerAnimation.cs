using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private float curTime;
    private float coolTime = 0.5f;
    private float horizontal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        p_Animator();
        p_Attack();
    }

    public void p_Animator()
    {
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    }

    public void p_Attack()
    {
        if (curTime <= 0)
        {

            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetBool("Attack", true);
                curTime = coolTime;
                if (curTime == coolTime && Input.GetKeyDown(KeyCode.C))
                {
                    animator.SetBool("Attack2", true);
                    Invoke("isAttack2", 0.8f);
                }
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
    private void isAttack2()
    {
        animator.SetBool("Attack2", false);
    }

}
