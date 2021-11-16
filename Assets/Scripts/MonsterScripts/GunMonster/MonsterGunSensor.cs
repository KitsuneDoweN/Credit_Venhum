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
    private float attacktime = 0.67f;

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
        if (coolTime <= curTime)
        {
            eGun = GunState.e_Gun;
            Instantiate(monsterBullet, pos.position, transform.rotation).GetComponent<Bullet>().Init(pos.localPosition.normalized);
            manager.anim.SetBool("Attack", true);
            curTime = 0;
        }
    }
    private void Attack()
    {
        //0.67동안 켜져있어야한다.
        curTime += Time.deltaTime;
        if (curTime >= attacktime)
        {
            eGun = GunState.e_none;
            manager.anim.SetBool("Attack", false);

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
}
