using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class close_video : MonoBehaviour
{
   
    public GameObject player;
    public GameObject button;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
         //  VuforiaBehaviour.Instance.enabled = false;
            Destroy(player);
        Destroy(button);
       
        VuforiaBehaviour.Instance.enabled = true;
        
    }
}
