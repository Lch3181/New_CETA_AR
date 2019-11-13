using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class close_video : MonoBehaviour
{
    public Transform play;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
         //  VuforiaBehaviour.Instance.enabled = false;
            Destroy(player);
            VuforiaBehaviour.Instance.enabled = true;
        
    }
}
