using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuleWitchRoomEvent : MonoBehaviour
{
    [SerializeField]
    private Animator m_ani;

    [SerializeField]
    private GameObject m_goBossRoomCollider;

    [SerializeField]
    private BossRoom m_cBossRoom;

    public void eventOn()
    {
        m_ani.SetTrigger("eventStart");
        GameManager.Instance.cStageManager.cPlayer.isControl = false;
    }

    public void battleStart()
    {
        m_goBossRoomCollider.SetActive(true);
        m_cBossRoom.roomIn(GameManager.Instance.cStageManager.cPlayer);
        GameManager.Instance.cStageManager.cPlayer.isControl = true;

        gameObject.SetActive(false);

    }


}
