using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public float speed = 15;
    public float distance;
    public LayerMask isLayer;

    private float weaponDamage = 1;
    private PlayerAttack m_attack;

    private Vector2 m_dir;

    private float stiffCount = 1;

    [SerializeField]
    private Rigidbody2D rigid;
    public void Init(Vector2 dir)
    {
        m_dir = dir;
        Invoke("DestroyBullet", 2);
        rigid.velocity = dir * speed;
        if(dir == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        else if (dir == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (dir == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }
        else if (dir == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }

        //else if(dir.x > 0 && dir.y > 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, 0);
        //}
        //else if (dir.x > 0 && dir.y < 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, -90);
        //}
        //else if (dir.x < 0 && dir.y < 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, -180);
        //}
        //else if (dir.x < 0 && dir.y > 0)
        //{
        //    transform.rotation = Quaternion.Euler(0, 0, -270);
        //}

    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHp")
        {
            collision.transform.parent.GetComponent<MonsterManager>().Stiff(stiffCount);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right, distance, isLayer);
        if(ray.collider != null)
        {
            if(ray.collider.tag == "EnemyHp")
            {
                Debug.Log("ИэСп");
            }
            Destroy(gameObject);
        }
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
