using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusicClip;
    public AudioClip soundEffectClip;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    private bool isBackgroundMusicPaused = false;
    private float pausedBackgroundMusicTime = 0f;

    public float backgroundMusicVolume = 0.5f;
    public float soundEffectVolume = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        SetBackgroundMusicVolume(backgroundMusicVolume);
        SetSoundEffectVolume(soundEffectVolume);

        PlayBackgroundMusic(backgroundMusicClip);
    }

    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PauseBackgroundMusic()
    {
        if (!isBackgroundMusicPaused)
        {
            pausedBackgroundMusicTime = musicSource.time;
            musicSource.Pause();
            isBackgroundMusicPaused = true;
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (isBackgroundMusicPaused)
        {
            musicSource.Play();
            musicSource.time = pausedBackgroundMusicTime;
            isBackgroundMusicPaused = false;
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        // Clamp the volume value between 0 and 1
        backgroundMusicVolume = Mathf.Clamp01(volume);
        musicSource.volume = backgroundMusicVolume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        // Clamp the volume value between 0 and 1
        soundEffectVolume = Mathf.Clamp01(volume);
        sfxSource.volume = soundEffectVolume;
    }
}
