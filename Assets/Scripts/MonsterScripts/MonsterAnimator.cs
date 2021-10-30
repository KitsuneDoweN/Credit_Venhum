using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator animator;
    private bool b_walk = false;
    SpriteRenderer spriteRenderer;

    public void Init()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Animator();
    }

    public void Animator() 
    {
        animator.SetBool("Walk", true);
    }
}
