using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackSensor : MonoBehaviour
{
    public GameObject attack;
    private float curTime;
    private float coolTime = 0.5f;
    private float attacktime = 0.67f;

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

    public void setAttackPos(bool isRight)
    {
        attack.transform.localPosition = Vector2.left;

        if (isRight == true)
            attack.transform.localPosition = Vector2.right*2;
        
    }

    private void Stay()
    {
        curTime += Time.deltaTime;
        if(coolTime <= curTime)
        {
            eAttack = AttackState.e_attack;
            attack.SetActive(true);
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
            attack.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            eAttack = AttackState.e_ready;
            curTime = 0;
        }
    }

}
