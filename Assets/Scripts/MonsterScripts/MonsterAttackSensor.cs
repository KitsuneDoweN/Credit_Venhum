using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackSensor : MonoBehaviour
{
    public GameObject attackBox;
    private float curTime;
    private float coolTime = 0.5f;
    private float attacktime = 0.67f;
    [SerializeField]
    MonsterMoveSensor moveSensor;

    public enum AttackState
    {
        e_none, e_ready ,e_attack
    }
    public AttackState eAttack;

    private void Update()
    {
        if(eAttack == AttackState.e_none)
        {
            return;
        }
        if(eAttack == AttackState.e_ready)
        {
            Stay();
        }
        if (eAttack == AttackState.e_attack)
        {
            Attack();
        }
    }

    public void setAttackPos(bool isRight) //적용안했음
    {
        attackBox.transform.localPosition = Vector2.left;

        if (isRight == true)
            attackBox.transform.localPosition = Vector2.right*2;
        
    }

    private void Stay()
    {
        curTime += Time.deltaTime;
        if(coolTime <= curTime)
        {
            eAttack = AttackState.e_attack;
            attackBox.SetActive(true);
            curTime = 0;
        }
    }
    private void Attack()
    {
        //0.67동안 켜져있어야한다.
        curTime += Time.deltaTime;
        if (curTime >= attacktime)
        {
            eAttack = AttackState.e_none;
            attackBox.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSensor.ChaseOff();
            eAttack = AttackState.e_ready;
            curTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //moveSensor.anim.SetBool("Walk", true);
            moveSensor.ChaseOn();
            eAttack = AttackState.e_none;
            attackBox.SetActive(false);
            curTime = 0;
        }
    }

}
