using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float moveSpeed = 1.5f;
    private Vector2 targetPosition;
    
    void Update()
    {
        //대상 체크
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y);
            transform.position = Vector2.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
