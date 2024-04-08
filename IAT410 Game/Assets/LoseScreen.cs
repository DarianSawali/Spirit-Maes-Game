using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreen : MonoBehaviour
{
    public GameObject loseScreenCanvas;
    public AudioManager audioManager;
    public HealthManager healthManager;
    public LevelReset reset;
    private bool lost = false;

    void Start()
    {
        loseScreenCanvas.SetActive(false);
        HealthManager healthManager = GetComponent<HealthManager>();
        LevelReset reset = GetComponent<LevelReset>();
    }

    void Update(){
        if(healthManager.health <= 0){
            ShowLoseScreen();
        }
    }

    public void ShowLoseScreen(){
        Time.timeScale = 0f;
        audioManager.PauseBackgroundMusic();
        loseScreenCanvas.SetActive(true);
    }

    public void Retry()
    {
        // reset.ResetLevel();
        healthManager.health = 3;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
