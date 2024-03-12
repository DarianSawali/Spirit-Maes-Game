using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public Animator transition;

    // void Update()
    // {
        
    // }

    public void FadeToLevel(int levelIndex){
        transition.SetTrigger("FadeOut");
    }

    public void Fade(){
        transition.SetTrigger("FadeOut");
    }
}
