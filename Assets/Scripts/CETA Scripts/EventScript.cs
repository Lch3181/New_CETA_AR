using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// GetSet What should be shown as the title of the info UI.
/// </summary>
public class EventScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI date;

    [SerializeField]
    private TextMeshProUGUI startTime;

    [SerializeField]
    private TextMeshProUGUI endTime;

    [SerializeField]
    private TextMeshProUGUI eventTitle;

    [SerializeField]
    private TextMeshProUGUI eventLocation;

    [SerializeField]
    private string detailsLink = "";

    public void setDate(string inputInfo)
    {
        date.text = inputInfo;
    }

    public void setStart(string inputInfo)
    {
        startTime.text = inputInfo;
    }

    public void setEnd(string inputInfo)
    {
        endTime.text = inputInfo;
    }

    public void setTitle(string inputInfo)
    {
        eventTitle.text = inputInfo;
    }

    public void setLoc(string inputInfo)
    {
        eventLocation.text = inputInfo;
    }

    public void setLink(string inputLink)
    {
        detailsLink = inputLink;
    }

    /// <summary>
    /// Open website link
    /// </summary>
    public void openLink()
    {
        if (detailsLink == "")
        {
            Debug.Log("No link provided for event.");
            return;
        }
        else
        {
            Application.OpenURL(detailsLink);
        }
    }
}
