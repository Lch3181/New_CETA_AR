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
  
    // Start is called before the first frame update
   
    // Update is called once per frame
    private void OnMouseDown()
    {
        if(player.isPlaying)
        {
          //  Instantiate(button, new Vector3(0, 0, 2), play.rotation);
           
            player.Pause();
        }
        else
        {
           // Destroy(button);
           
            player.Play();

        }


    }
}
