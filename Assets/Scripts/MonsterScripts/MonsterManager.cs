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

    private float stiff_coolTime = 0.5f;
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

    public void Stiff()
    {
        stiff_curTime += Time.deltaTime;
        movesensor.rb.velocity = Vector2.zero;
        movesensor.ChaseOff();
        if(stiff_curTime >= stiff_coolTime)
        {
            movesensor.ChaseOn();
        }
    }

}
