using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGunSensor : MonoBehaviour
{
    public GameObject gunObject;

    public GameObject monsterBullet;
    public Transform pos;

    private float curTime;
    private float coolTime = 0.5f;
    private float attacktime = 0.75f;

    [SerializeField]
    GunMonsterMoveSensor moveSensor;
    [SerializeField]
    GunMonsterManager manager;

    public enum GunState
    {
        e_none, e_ready, e_Gun
    }
    public GunState eGun;

    private void Update()
    {
        if (eGun == GunState.e_none)
        {
            return;
        }
        if (eGun == GunState.e_ready)
        {
            Stay();
        }
        if (eGun == GunState.e_Gun)
        {
            Attack();
        }
    }

    public void setAttackPos(bool isRight)
    {
        gunObject.transform.localPosition = Vector2.left;

        if (isRight == true)
            gunObject.transform.localPosition = Vector2.right * 2;

    }

    private void Stay()
    {
        curTime += Time.deltaTime;
        moveSensor.nav.isStopped = true;
        manager.anim.SetBool("Attack", true);
        if (coolTime <= curTime)
        {
            eGun = GunState.e_Gun;
            Instantiate(monsterBullet, pos.position, transform.rotation).GetComponent<Bullet>().Init(pos.localPosition.normalized);
            Invoke("AttackTime", 0.75f);
            curTime = 0;
        }
    }
    private void Attack()
    {
        //0.67동안 켜져있어야한다.
        manager.anim.SetBool("Walk", true);
        curTime += Time.deltaTime;
        if (curTime >= attacktime)
        {
            moveSensor.nav.isStopped = false;
            eGun = GunState.e_none;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSensor.ChaseOff();
            eGun = GunState.e_ready;
            curTime = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            moveSensor.ChaseOn();
            eGun = GunState.e_none;
            curTime = 0;
        }
    }

    private void AttackTime()
    {
        manager.anim.SetBool("Attack", false);
    }
}
