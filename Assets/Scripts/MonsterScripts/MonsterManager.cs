using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]
    MonsterStatus status;
    [SerializeField]
    MonsterAttack attack;
    [SerializeField]
    MonsterAttackSensor attacksensor;
    [SerializeField]
    MonsterMoveSensor movesensor;

    MonsterAllManager allManager;

    public bool isControl;
    public bool isTakeDamage;

    private Animator anim;
    private float power = 1;
    private float stiff_count = 0;

    private float stiff_coolTime = 0.3f;
    private float stiff_curTime;

    public void Init(Transform target, MonsterAllManager monsterAllManager)
    {
        anim = GetComponent<Animator>();
        allManager = monsterAllManager;
        isControl = true;
        movesensor.Init(target, attacksensor, this);
        attack.Init(status, movesensor);
    }

    public void TakeDamage(float damage)
    {
        if(isTakeDamage == true)
        {
            return;
        }
        status.hp -= damage;
        if (status.hp <= 0)
        {
            anim.SetBool("Die", true);
            allManager.DeathCount += 1;
            Destroy(gameObject, 1.333f);
            isControl = false;
            return;
        }
        anim.SetBool("Hit", true);
        isTakeDamage = true;
        isControl = false;
        movesensor.ChaseOff();
        movesensor.rb.velocity = (movesensor.lookVec * -1) * power;

        Invoke("TakeDamageEnd", 0.5f);
    }
    void TakeDamageEnd()
    {
        anim.SetBool("Hit", false);
        isTakeDamage = false;
        isControl = true;
        movesensor.ChaseOn();
        movesensor.rb.velocity = Vector2.zero;
    }

    public void Stiff(float count)
    {
        stiff_count += count;
        Debug.Log(stiff_count);

        if (stiff_count >= 5)
        {
            anim.SetBool("Hit", true);
            stiff_curTime += Time.deltaTime;
            movesensor.rb.velocity = Vector2.zero;
            movesensor.ChaseOff();
            stiff_count = 0;
            if (stiff_curTime >= stiff_coolTime)
            {
                movesensor.ChaseOn();
                Debug.Log(stiff_count);
            }
        }
        Invoke("StiffOff", 0.8f);
    }

    private void StiffOff()
    {
        if (stiff_count == 0)
        {
            anim.SetBool("Hit", false);
            movesensor.ChaseOn();
        }
    }
}


