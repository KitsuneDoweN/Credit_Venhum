using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStageHandler : MonoBehaviour
{
    [SerializeField]
    private NextStage m_cNextStage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_cNextStage.goNextScene();
        }
    }
}
