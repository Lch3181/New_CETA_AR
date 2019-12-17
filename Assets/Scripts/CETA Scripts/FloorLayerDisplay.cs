using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLayerDisplay : MonoBehaviour
{
    public GameObject[] Floors;
    public GameObject[] Triggers;

    public void SetCurrentLayer(GameObject go)
    {
        ResetLayer();
        go.layer = LayerMask.NameToLayer("CurrentFloor");
        for (int i = System.Array.IndexOf(Floors, go); i >= 0; i--)
        {
            foreach (Transform child in Triggers[i].transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("CurrentFloor");
            }
            foreach (Transform child in Floors[i].transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("CurrentFloor");
            }
        }
    }

    public void ResetLayer()
    {
        foreach (GameObject floor in Floors)
        {
            floor.layer = 10;
            foreach (Transform child in floor.transform)
            {
                child.gameObject.layer = 10;
            }
        }

        foreach (GameObject trigger in Triggers)
        {
            trigger.layer = 10;
            foreach (Transform child in trigger.transform)
            {
                child.gameObject.layer = 10;
            }
        }
    }
}
