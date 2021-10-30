using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearScript : MonoBehaviour
{
    public GameObject ClearUI;

    private void Update()
    {
        GameClear();
    }

    public void GameClear()
    {
        if (MonsterStatus.monsterDeathCount == 5)
        {
            ClearUI.SetActive(true);
            Time.timeScale = 0;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClearUI.SetActive(false);
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
                MonsterStatus.monsterDeathCount = 0;
            }
        }
    }
}
