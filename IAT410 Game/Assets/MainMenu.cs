using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    //private SceneTransition sceneTransition;
    //private SceneTransition sceneTransition;

    public int playerLives;

    // public void Awake()
    // {
    // sceneTransition = GameObject.Find("TransitionObject").GetComponent<SceneTransition>();
    // if (sceneTransition == null)
    // {
    //     Debug.LogError("SceneTransition component not found.");
    // }
    // }

    public void PlayGame()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        transition.SetTrigger("FadeOut");
    }

    public void Fade()
    {
        transition.SetTrigger("FadeOut");
    }


}
