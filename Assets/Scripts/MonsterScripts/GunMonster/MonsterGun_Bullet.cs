using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGun_Bullet : MonoBehaviour
{
    public GameObject monsterBullet;
    public Transform pos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(monsterBullet, pos.position, transform.rotation).GetComponent<RangedWeapon>().Init(pos.localPosition.normalized);
        } 
    }

}
