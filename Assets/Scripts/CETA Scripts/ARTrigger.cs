using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTrigger : MonoBehaviour
{
    public InfoTrigger triggerCounterpart;
    public string stringCounterpart;

    void Start()
    {
        triggerCounterpart = GameObject.Find(stringCounterpart).GetComponent<InfoTrigger>();
        Debug.Log(triggerCounterpart);
    }

    private void OnMouseDown()
    {
        if(triggerCounterpart != null)
        {
            triggerCounterpart.ButtonActiveEvent();
        }
    }
}
