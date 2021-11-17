using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float distance;
    private Vector2 m_dir;
    [SerializeField]
    private Rigidbody2D rigid;
    public void Init(Vector2 dir)
    {
        m_dir = dir;
        Invoke("DestroyBullet", 3);
        rigid.velocity = dir * speed;
        if (dir == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (dir == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(1);
            Bulletoff();
        }
        if (collision.gameObject.tag == "Wall")
        {
            Bulletoff();
        }
    }
    
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    void Bulletoff()
    {
        gameObject.SetActive(false);
    }
}
