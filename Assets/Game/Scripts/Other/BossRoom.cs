using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom : MonoBehaviour
{
    private bool m_bRoomIn;
    [SerializeField]
    private Transform m_trPivot;
    [SerializeField]
    private float m_fZoom;
    [SerializeField]
    private BossTheCosastofHand m_cBoss;

    // Start is called before the first frame update
    void Start()
    {
        m_bRoomIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_bRoomIn)
            return;

        if (collision.tag == "Player")
        {
            GameManager.instance.cStageManager.cCameraManager.setStatePivot(m_trPivot, m_fZoom);
            m_bRoomIn = true;
            m_cBoss.HandleWakeUp(collision.GetComponent<UnitBase>());
        }
            

    }
}
