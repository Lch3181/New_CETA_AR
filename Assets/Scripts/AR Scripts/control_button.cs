using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;
public class control_button : MonoBehaviour
{
    public Transform play;
    public VideoPlayer player;
    public GameObject button;

    /// Pause or Play video player
    private void OnMouseDown()
    {
        if (player.isPlaying)
        {
            player.Pause();
        }
        else
        {
            player.Play();
        }
    }
}
