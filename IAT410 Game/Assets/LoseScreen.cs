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

    private int currLevel;

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
        reset.ResetLevel();
        healthManager.ResetHealth();
        Time.timeScale = 1f;

        currLevel = SceneManager.GetActiveScene().buildIndex;
        if(currLevel >= 2 && currLevel < 5){
            SceneManager.LoadScene(2);
        } else if(currLevel >= 6 && currLevel < 9){
            SceneManager.LoadScene(6);
        } else if(currLevel >= 10 && currLevel < 13){
            SceneManager.LoadScene(10);
        }
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
