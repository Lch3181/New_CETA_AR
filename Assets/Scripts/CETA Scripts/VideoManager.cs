using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private AudioSource audioSource;

    public void setURL(string URL)
    {
        videoPlayer.url = URL;
        audioSource = gameObject.AddComponent<AudioSource>();
        videoPlayer.SetTargetAudioSource(0,audioSource);
        videoPlayer.Prepare();
    }

    public void startVideo()
    {
        videoPlayer.Play();
    }

    public void pauseToggle()
    {
        if (videoPlayer.isPaused)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }

    }

    public void closeVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Prepare();
    }
}
