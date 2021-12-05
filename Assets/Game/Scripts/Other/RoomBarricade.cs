using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBarricade : MonoBehaviour
{
    [SerializeField]
    private BossRoom m_cBossRoom;
    [SerializeField]
    private BoxCollider2D m_boxCollider2D;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_cBossRoom.isRoomIn)
            return;

        if (collision.tag == "Player" && collision.transform.position.y > transform.position.y)
        {
            m_cBossRoom.roomIn(collision.GetComponent<UnitBase>());
            m_boxCollider2D.isTrigger = false;
        }


    }
}
