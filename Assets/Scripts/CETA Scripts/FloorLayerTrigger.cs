using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detect when Player walk inbetween Floors, Show/Hide above Floors/Triggers
/// </summary>
public class FloorLayerTrigger : MonoBehaviour
{
    public GameObject floorLayer;
    private FloorLayerDisplay layerDisplay;

    /// <summary>
    /// GetSet Player to the script
    /// </summary>
    private void Start()
    {
        layerDisplay = GameObject.Find("Player").GetComponent<FloorLayerDisplay>();
    }

    /// <summary>
    /// Detect when play walk into trigger inbetween floors
    /// </summary>
    private void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player")
        {
            Debug.Log("Setting Layer.");
            layerDisplay.SetCurrentLayer(floorLayer);
        }
    }
}
