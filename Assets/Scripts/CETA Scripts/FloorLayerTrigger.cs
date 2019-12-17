using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLayerTrigger : MonoBehaviour
{
    public GameObject floorLayer;
    private FloorLayerDisplay layerDisplay;

    private void Start()
    {
        layerDisplay = GameObject.Find("Player").GetComponent<FloorLayerDisplay>();
    }


    private void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player")
        {
            Debug.Log("Setting Layer.");
            layerDisplay.SetCurrentLayer(floorLayer);
        }
    }
}
