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

    public GameObject attackSensor;
    
    public bool isControl;
    public bool isTakeDamage;

    public ParticleSystem particle;

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
        particle.Stop();
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
            attackSensor.SetActive(false);
            anim.SetBool("Die", true);
            allManager.DeathCount += 1;
            movesensor.ChaseOff();
            Destroy(gameObject, 1.333f);
            isControl = false;
            return;
        }
        anim.SetBool("Hit", true);
        isTakeDamage = true;
        particle.Play();
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
                anim.SetBool("Hit", false);
                Debug.Log(stiff_count);
            }
            Invoke("StiffOff", 0.5f);
        }
            Invoke("StiffOff", 0.5f);

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

//몬스터 밑이나 위에 게이지 형식으로 경직 스택과 체력 추가
//(UI연결 / 경직은 원형 / 체력은 Bar)
//버그
//플레이어 공격 때 동시에 움직일 경우 애니메이션 스킵(이동 애니메이션이 나옴 -> 공격 우선으로변경)
//몬스터가 어택센서 안에서 공격받았을 때 Hit 애니메이션이 여러번 나오는 것.