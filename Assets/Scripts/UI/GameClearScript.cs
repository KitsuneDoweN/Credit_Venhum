using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearScript : MonoBehaviour
{
    public GameObject ClearUI;
    public MonsterAllManager monsterManager;

    private void Update()
    {
        //GameClear();
    }

    public void GameClear()
    {
        if (monsterManager.DeathCount == 5)
        {
            ClearUI.SetActive(true);
            Time.timeScale = 0;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClearUI.SetActive(false);
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
                monsterManager.DeathCount = 0;
            }
        }
    }
}
