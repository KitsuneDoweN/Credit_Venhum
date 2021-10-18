using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSensor : MonoBehaviour
{
    public GameObject attack;
    private float curTime;
    private float coolTime = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (curTime <= 0)
        {
            if (collision.gameObject.tag == "Player")
            {
                attack.SetActive(true);
                curTime = coolTime;
            }
            else
            {
                attack.SetActive(false);
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }
}
