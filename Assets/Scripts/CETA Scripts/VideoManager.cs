using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    public void setURL(string URL)
    {
        videoPlayer.url = URL;
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
    }
}
