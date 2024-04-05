using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject gameplayCanvas;
    public AudioManager audioManager;

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

        if (pauseCanvas != null && gameplayCanvas != null)
        {
            pauseCanvas.SetActive(isPaused); // Show or hide pause menu
            gameplayCanvas.SetActive(!isPaused); // Show or hide gameplay elements
        }

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        audioManager.PauseBackgroundMusic();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Unpause the game
        audioManager.ResumeBackgroundMusic();
    }

    public void ToggleResume()
    {
        isPaused = false; 
        Time.timeScale = 1f; // Unpause the game
        audioManager.ResumeBackgroundMusic();
        pauseCanvas.SetActive(false); // Hide pause menu
        gameplayCanvas.SetActive(true); // Show gameplay elements
    }
}
