using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Script by Venus
    public static bool IsPaused;
    public Canvas OwnCanvas;

    public int TitleIndex;  // **PLACEHOLDER** Title screen does not yet exist
    
    void Start()
    {
        OwnCanvas = GetComponent<Canvas>();
        OwnCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))    // Can I better optimise this with events instead?
        {
            switch (IsPaused)
            {
                case true:
                    Resume();
                    break;
                case false:
                    Pause();
                    break;
            }
        }
    }

    // Functions for each button on pause menu
    public void Resume()
    {
        OwnCanvas.enabled = false;
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        OwnCanvas.enabled = true;
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    // **PLACEHOLDER FUNCTIONS** Their functionalities are not yet done due to missing data
    public void Save()
    {
        print("Game Saved!");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene(TitleIndex);
    }
}
