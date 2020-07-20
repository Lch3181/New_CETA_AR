using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARTrigger : MonoBehaviour
{
    public InfoTrigger triggerCounterpart;
    public string stringCounterpart;

    /// <summary>
    /// Start is called on the frame when a script is enabled
    /// 
    /// Get all Triggers
    /// </summary>
    void Start()
    {
        triggerCounterpart = GameObject.Find(stringCounterpart).GetComponent<InfoTrigger>();
        Debug.Log(triggerCounterpart);
    }

    /// <summary>
    /// Show Trigger UI on Mouse Click on any Trigger
    /// </summary>
    private void OnMouseDown()
    {
        if(triggerCounterpart != null && !GameObject.Find("CETA Manager").GetComponent<CETAUIManager>().infoMenuShown)
        {
            triggerCounterpart.ButtonActiveEvent();
        }
    }
}
