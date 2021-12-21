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

    public bool isRoomIn
    {
        set
        {
            m_bRoomIn = value;
        }
        get
        {
            return m_bRoomIn;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_bRoomIn = false;
    }

    public void roomIn(UnitBase unit)
    {
        GameManager.Instance.cStageManager.cCameraManager.setStatePivot(m_trPivot, m_fZoom);
        isRoomIn = true;
        m_cBoss.HandleWakeUp(unit);
    }


}
