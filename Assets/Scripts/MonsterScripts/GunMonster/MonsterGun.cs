using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGun : MonoBehaviour
{
    MonsterStatus monster_status;
    MonsterMoveSensor monster_moveSensor;

    public Animator animator;

    public void Init(MonsterStatus hp, MonsterMoveSensor moveSensor)
    {
        monster_status = hp;
        monster_moveSensor = moveSensor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(GameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           
        }
    }
}
