using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTypeWaveHandler : MonoBehaviour
{
    private bool m_bEventOn = true;
    [SerializeField]
    private AreaTypeSpawnManger m_cAreaTypeSpawnManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!m_bEventOn)
            return;

        if(collision.tag == "Player")
        {
            m_cAreaTypeSpawnManager.handleSpawn();
            m_bEventOn = false;
        }
    }
}
