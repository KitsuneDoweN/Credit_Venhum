using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour
{
    [SerializeField]
    private GameManager.E_GAMESCENE m_eNextScene;
    [SerializeField]
    private bool m_bClearStage;

    public void goNextScene()
    {
        if (m_eNextScene == GameManager.E_GAMESCENE.E_RANDOM_DUGGEN)
        {
            m_eNextScene = randomDungeon();
        }


        GameManager.Instance.NextStage(m_eNextScene, m_bClearStage);
    }

    private GameManager.E_GAMESCENE randomDungeon()
    {
        int nRandom = Random.RandomRange((int)GameManager.E_GAMESCENE.E_STAGE_DUGGEN_LOOPTYPE0, (int)GameManager.E_GAMESCENE.E_STAGE_DUGGEN_LOOPTYPE1);
        return (GameManager.E_GAMESCENE)nRandom;
    }

    public void setNextScene(GameManager.E_GAMESCENE eNextScene)
    {
        m_eNextScene = eNextScene;
    }

}
