using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Animator transition;

    public void Resume(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }

    public void OnFadeComplete(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex){
        transition.SetTrigger("FadeOut");
    }

    public void Fade(){
        transition.SetTrigger("FadeOut");
    }

    
}