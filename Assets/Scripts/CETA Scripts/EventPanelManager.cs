using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPanelManager : MonoBehaviour
{
    public GameObject eventEntry;
    public GameObject scrollContent;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= 10; i++)
        {
            GameObject newEvent = Instantiate(eventEntry) as GameObject;
            EventScript eventDetails = newEvent.GetComponent<EventScript>();
            eventDetails.setDate("11/4/19");
            eventDetails.setStart("12:00","AM");
            eventDetails.setEnd(i + ":00","PM");
            eventDetails.setTitle("Custom Test Event #" + i);
            eventDetails.setLoc("CETA Hub #" + i);

            newEvent.transform.SetParent(scrollContent.transform, false);
        }
    }
}
