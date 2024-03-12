using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        
        // if(SceneManager.GetActiveScene().buildIndex == sceneCount){
            
        // } else {
        //     videoPlayer.loopPointReached += OnVideoFinished;
        // }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings - 1;
        if(SceneManager.GetActiveScene().buildIndex == sceneCount){
            SceneManager.LoadScene(0);
        } else if(SceneManager.GetActiveScene().buildIndex == 2){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
