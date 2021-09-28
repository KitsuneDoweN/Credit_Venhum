using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform pos;
    [SerializeField]
    private float cooltime;
    [SerializeField]
    private float curtime;

    void Update()
    {
        if(curtime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Instantiate(bullet, pos.position, transform.rotation);
            }
            curtime = cooltime;
        }
        curtime -= Time.deltaTime;
        
    }
}
