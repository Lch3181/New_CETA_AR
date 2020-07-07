using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class create_player : MonoBehaviour
{
    public Transform play;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per fram

    private void OnMouseDown()
    {
           
                VuforiaBehaviour.Instance.enabled = false;
                //  VuforiaBehaviour.Instance.enabled = false;
                Instantiate(player, new Vector3(0, 0, 2), play.rotation);
               
           
        }
    
}
