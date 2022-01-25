using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    [SerializeField]
    private UnitBossBase m_cBoss;

    // Start is called before the first frame update





    public void bossStart()
    {
        m_cBoss.handleWakeUp(GameManager.Instance.cStageManager.cPlayer);
    }


}
