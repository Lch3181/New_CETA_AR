using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTrigger : MonoBehaviour
{
    //Used to call to the CETA UI Manager.
    public CETAUIManager managerCall;

    //What the button that triggers the info UI should display.
    public string triggerTitle;
    //The title of the UI.
    public string displayTitle;
    //What image should be shown. (The URL link to the image.)
    public string imageURL;
    //The description of the location of the trigger.
    public GameObject description;

    //Website button text.
    public string webButtonTitle;
    //Website Link.
    public string webLink;
    
    //Action button title.
    public string actionTitle;
    //What the action does.
    public enum actionType{video,panel,none};
    public actionType setType;

    //The panel to show.
    public GameObject panel;
    //The where the panel should hide.
    public Transform panelHide;

    //The video URL to play;
    public string videoURL;
   

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.tag == "Player")
        {
            if(managerCall.infoMenuShown)
            {
                Debug.Log("Panel Already Shown.");
            }
            else
            {
                managerCall.setCommonInfo(triggerTitle, displayTitle, imageURL, getDescriptionText());
                managerCall.setLink(webButtonTitle, webLink);
                prepareAction();
            }
            
        }
    }

    private void OnTriggerStay(Collider hit)
    {
        managerCall.triggerButtonOn();
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            managerCall.triggerButtonOff();
            managerCall.removeListeners();
        }
    }

    private string getDescriptionText()
    {
        return description.GetComponent<Text>().text;
    }

    private void prepareAction()
    {
        if(setType != actionType.none)
        {
            managerCall.panelSetup(panel, panelHide);
        }

        switch(setType)
        {
            case actionType.video:
                managerCall.setAction(actionTitle,videoURL);
                break;
            case actionType.panel:
                managerCall.setAction(actionTitle);
                break;
            default:
                managerCall.setAction();
                break;
        }
    }
}
