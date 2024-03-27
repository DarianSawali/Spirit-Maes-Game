using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HoverPanel : MonoBehaviour
{

    public Animator transition;

    public void Easy(){
        SceneManager.LoadScene(2);
    }

    public void Medium(){
        SceneManager.LoadScene(6);
    }

    public void Hard(){
        SceneManager.LoadScene(9);
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
