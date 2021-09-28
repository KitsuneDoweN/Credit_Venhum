using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScripts : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Destroy(gameObject);
        }
    }
   
}
