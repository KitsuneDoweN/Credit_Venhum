using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    private float horizontal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        p_Animator();
    }

    public void p_Animator()
    {
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    }


}
