using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearScript : MonoBehaviour
{
    public GameObject ClearUI;

    private void Update()
    {
        GameClear();
    }

    public void GameClear()
    {
        if (MonsterManager.monsterDeathCount == 5)
        {
            ClearUI.SetActive(true);
        }
    }
}
