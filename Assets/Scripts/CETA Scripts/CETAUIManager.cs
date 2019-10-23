using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CETAUIManager : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.Video.VideoPlayer videoPlayer;

    //The info panel itself;
    [SerializeField]
    private GameObject infoPanel;

    //The button that triggers the info panel.
    [SerializeField]
    private GameObject triggerButton;

    //What should be shown as the title of the info UI.
    [SerializeField]
    private TextMeshProUGUI infoTitle;

    //What image should be shown.
    [SerializeField]
    private Image image;

    //The description of the specified location.
    [SerializeField]
    private TextMeshProUGUI description;

    //Location of the menu once activated.
    [SerializeField]
    private Transform menuShown;

    //Location of the menu once hidden.
    [SerializeField]
    private Transform menuHidden;

    //Location of the trigger button for hidden and shown.
    [SerializeField]
    private Transform triggerHidden;
    [SerializeField]
    private Transform triggerShown;

    //Describes the button in the description.
    [SerializeField]
    private GameObject actionButton;

    //Describes the button that acts as a link.
    [SerializeField]
    private GameObject linkButton;

    [SerializeField]
    private GameObject scrollBar;

    //Determines if the info menu is on screen.
    public bool infoMenuShown = false;

    //A link to a website.
    private string webLink = "";

    //The Video screen.
    [SerializeField]
    private GameObject videoScreen;

    //Location of the video screen for hidden and shown.
    [SerializeField]
    private Transform videoHidden;
    [SerializeField]
    private Transform videoShown;

    public void setCommonInfo(string inputTriggerTitle,string inputTitle, Sprite inputImage, string inputDesc)
    {

        triggerButton.GetComponentInChildren<TextMeshProUGUI>().text = inputTriggerTitle;
        infoTitle.text = inputTitle;
        image.sprite = inputImage;
        description.text = inputDesc;
        scrollBar.GetComponent<Scrollbar>().value = 1;
    }

    public void setLink(string linkTitle, string inputLink)
    {
        if(inputLink == "")
        {
            linkButton.SetActive(false);
        }
        else
        {
            linkButton.SetActive(true);
            webLink = inputLink;
            linkButton.GetComponentInChildren<TextMeshProUGUI>().text = linkTitle;
        }
    }

    //No action.
    public void setAction()
    {
        actionButton.SetActive(false);
    }

    //Video action.
    public void setAction(string actionTitle, UnityEngine.Video.VideoClip inputClip)
    {
        actionButton.SetActive(true);
        actionButton.GetComponentInChildren<TextMeshProUGUI>().text = actionTitle;
        actionButton.GetComponent<Button>().onClick.AddListener(() => startVideo());
        videoPlayer.clip = inputClip;
        videoPlayer.Prepare();

    }

    public void openLink()
    {
        if(webLink == "")
        {
            return;
        }
        else
        {
            Application.OpenURL(webLink);
        }
    }

    public void startVideo()
    {
        ToggleUIObject(videoScreen, videoShown);
        videoPlayer.Play();
    }

    public void closeVideo()
    {
        ToggleUIObject(videoScreen, videoHidden);
        videoPlayer.Stop();
    }

    public void ToggleUIObject(GameObject aObject, Transform aTransform)
    {
        iTween.MoveTo(aObject, iTween.Hash("position", aTransform.position, "time", 1));
    }

    public void toggleInfoMenu()
    {
        infoMenuShown = !infoMenuShown;
        if (infoMenuShown)
        {
            ToggleUIObject(infoPanel, menuShown);
        }
        else
        {
            ToggleUIObject(infoPanel, menuHidden);
            actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        }

    }

    public void triggerButtonOn()
    {
        if (infoMenuShown)
        {
            triggerButtonOff();
        }
        else
        {
            ToggleUIObject(triggerButton, triggerShown);
        }
    }

    public void triggerButtonOff()
    {
        ToggleUIObject(triggerButton, triggerHidden);
    }
}
