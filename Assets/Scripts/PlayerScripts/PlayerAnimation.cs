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
        Animator();
    }

    public void Animator()
    {
        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    }


}
