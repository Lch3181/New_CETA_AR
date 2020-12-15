using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Layer Contol for Player's Camera
/// 
/// Hide Floors&Trigger Above Player's current Floor
/// </summary>
public class FloorLayerDisplay : MonoBehaviour
{
    public GameObject[] Floors;
    public GameObject[] Triggers;

    /// <summary>
    /// Set the selected Floor and Triggers to Layer(CurrentFloor)
    /// </summary>
    /// <param name="go"></param>
    public void SetCurrentLayer(GameObject go)
    {
        ResetLayer();
        go.layer = LayerMask.NameToLayer("CurrentFloor");
        for (int i = System.Array.IndexOf(Floors, go); i >= 0; i--)
        {
            foreach (Transform child in Triggers[i].GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("CurrentFloor");
            }
            foreach (Transform child in Floors[i].GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = LayerMask.NameToLayer("CurrentFloor");
            }
        }
    }

    /// <summary>
    /// Reset all Floors and Triggers to Layer 10 (Floor)
    /// </summary>
    public void ResetLayer()
    {
        foreach (GameObject floor in Floors)
        {
            floor.layer = 10;
            foreach (Transform child in floor.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = 10;
            }
        }

        foreach (GameObject trigger in Triggers)
        {
            trigger.layer = 10;
            foreach (Transform child in trigger.GetComponentsInChildren<Transform>())
            {
                child.gameObject.layer = 10;
            }
        }
    }
}
