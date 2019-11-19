using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPanelManager : MonoBehaviour
{
    public GameObject eventEntry;
    public GameObject scrollContent;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= 3; i++)
        {
            GameObject newEvent = Instantiate(eventEntry) as GameObject;
            EventScript eventDetails = newEvent.GetComponent<EventScript>();
            if(i == 1)
            {
                eventDetails.setDate("3/2/19");
                eventDetails.setStart("12:00", "AM");
                eventDetails.setEnd("1:00", "PM");
                eventDetails.setTitle("CETA Challenge Cup");
                eventDetails.setLoc("CETA Annex");
                eventDetails.setLink("https://www.dropbox.com/s/e5tnsosfsn8jwph/CETA%20Challenge%20Cup.jpg?dl=0");
                newEvent.GetComponent<Button>().onClick.AddListener(() => eventDetails.openLink());
            }
            else
            {
                eventDetails.setDate("11/4/19");
                eventDetails.setStart("12:00", "AM");
                eventDetails.setEnd(i + ":00", "PM");
                eventDetails.setTitle("Custom Test Event #" + i);
                eventDetails.setLoc("CETA Hub #" + i);
                newEvent.GetComponent<Button>().onClick.AddListener(() => eventDetails.openLink());
            }

            newEvent.transform.SetParent(scrollContent.transform, false);
        }
    }
}
