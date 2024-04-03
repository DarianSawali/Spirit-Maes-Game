using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject gameplayCanvas;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1; // Pause or resume time

        if (pauseCanvas != null && gameplayCanvas != null)
        {
            pauseCanvas.SetActive(isPaused); // Show or hide pause menu
            gameplayCanvas.SetActive(!isPaused); // Show or hide gameplay elements
        }
        
    }
}
