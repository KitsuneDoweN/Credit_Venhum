using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseUI;

    void Update()
    {
        Pause();
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseUI.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
