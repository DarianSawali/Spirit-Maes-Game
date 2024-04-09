using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverPanel : MonoBehaviour
{

    public Animator transition;

    public int playerLives;

    public void Easy()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        SceneManager.LoadScene(2);
    }

    public void Medium()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        SceneManager.LoadScene(6);
    }

    public void Hard()
    {
        PlayerPrefs.SetInt("PlayerCurrentLives", playerLives);
        SceneManager.LoadScene(10);
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
