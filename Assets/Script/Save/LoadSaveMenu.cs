using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenu : MonoBehaviour
{

    private void Start()
    {
        SaveMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public static bool GameIsPaused = false;
    public GameObject SaveMenu;
    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        SaveMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        SaveMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
