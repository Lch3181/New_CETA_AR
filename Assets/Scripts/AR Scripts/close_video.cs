using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class close_video : MonoBehaviour
{
   
    public GameObject player;
    public GameObject button;

    /// Destory Player and Button, ReEnable AR.
    private void OnMouseDown()
    {
        Destroy(player);
        Destroy(button);
       
        VuforiaBehaviour.Instance.enabled = true;
    }
}
