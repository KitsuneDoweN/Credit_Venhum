using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopTypeDungone : MonoBehaviour
{
    [SerializeField]
    private int m_nBossRoomStage;
    [SerializeField]
    private NextStage m_cNextStage;

    private void Start()
    {
        if (GameManager.Instance.nStage == m_nBossRoomStage)
            m_cNextStage.setNextScene(GameManager.E_GAMESCENE.E_STAGE_DUGGEN_BOSSROOM);
    }


}
