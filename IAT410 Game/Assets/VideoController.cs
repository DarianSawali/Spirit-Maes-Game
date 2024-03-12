using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    private bool hasVideoFinished;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0) && !hasVideoFinished)
        {
            LoadNextScene();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        hasVideoFinished = true;
        LoadNextScene();
    }

    void LoadNextScene()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings - 1;
        if (SceneManager.GetActiveScene().buildIndex == sceneCount)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
