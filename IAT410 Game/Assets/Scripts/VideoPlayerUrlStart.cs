using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerUrlStart : MonoBehaviour
{
    private void Awake()
    {
        VideoPlayer videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoPlayer.url.EndsWith(".mp4") ? videoPlayer.url 
                                                                                                                   : videoPlayer.url + ".mp4");
        videoPlayer.Play();
    }
}
