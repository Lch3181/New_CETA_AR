using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// Manage Video Player
/// </summary>
public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private AudioSource audioSource;

    /// <summary>
    /// initial video source from URL
    /// </summary>
    /// <param name="URL"></param>
    public void setURL(string URL)
    {
        videoPlayer.url = URL;
        audioSource = gameObject.AddComponent<AudioSource>();
        videoPlayer.SetTargetAudioSource(0,audioSource);
        videoPlayer.Prepare();
    }

    /// <summary>
    /// Play the video
    /// </summary>
    public void startVideo()
    {
        videoPlayer.Play();
    }

    /// <summary>
    /// Pause/Play the Video
    /// </summary>
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

    /// <summary>
    /// Stop and Close Video Player
    /// </summary>
    public void closeVideo()
    {
        videoPlayer.Stop();
        videoPlayer.Prepare();
    }
}
