using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    private Rigidbody2D m_rigid;
    private Vector2 movement = new Vector2();

    private float curTime;
    private float coolTime = 0.2f;

    public float playerStamina = 100.0f;

    private Vector3 lookDirection;

    public Transform AttackBox;

    Vector3 playerDir;
    bool isHorizontalMove;
    public void Init(Rigidbody2D rigid)
    {
        m_rigid = rigid;
    }

    Vector3 v3PlayerDir // 관리하기 편하게 쓰기위함.
    {
        set
        {
            playerDir = value;
            //히트박스 로컬포지션
            AttackBox.localPosition = playerDir;
        }
        get
        {
            return playerDir;
        }
    }
    

    public void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        m_rigid.velocity = movement * moveSpeed;
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");

        if (hInput == 0 && vInput != 0)
        {
            isHorizontalMove = false;
            if (vInput == 1)
                v3PlayerDir = Vector3.up;
            else if (vInput == -1)
                v3PlayerDir = Vector3.down;
        }
        else
        {
            isHorizontalMove = true;
            if (hInput == 1)
                v3PlayerDir = Vector3.right;
            else if (hInput == -1)
                v3PlayerDir = Vector3.left;
        }
        //Debug.DrawRay(m_rigid.position, v3PlayerDir * 1.0f, Color.red);
        //RaycastHit2D raycast = Physics2D.Raycast(m_rigid.position, v3PlayerDir * 1.0f);
    }

    public void Dash(float stamina)
    {
        if (curTime <= 0)
        {
             if (Input.GetKeyDown(KeyCode.Space))
             {
                moveSpeed = 10.0f;
                playerStamina = playerStamina - stamina;
                curTime = coolTime;
                
                if(playerStamina <= 0)
                {
                    // 대쉬 x
                }
            }
             else
             {
                moveSpeed = 4.0f;
             }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
            
    }
    
}
