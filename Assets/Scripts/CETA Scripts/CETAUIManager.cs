using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class CETAUIManager : MonoBehaviour
{
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

    //Used to set the scroll bar back to the top.
    [SerializeField]
    private GameObject scrollBar;

    //Determines if the info menu is on screen.
    public bool infoMenuShown = false;

    //A link to a website.
    private string webLink = "";

    //Represents the close button for a configurable panel.
    Transform closeButton;

    //Represents the player.
    [SerializeField]
    private CharacterController playerControls;

    //Deactivate player movement if the menu is shown.
    private void FixedUpdate()
    {
        if(infoMenuShown)
        {
            playerControls.enabled = false;
            Debug.Log("Player Movement Off");
        }
        else
        {
            playerControls.enabled = true;
            Debug.Log("Player Movement On");
            
        }
    }

    public void setCommonInfo(string inputTriggerTitle, string inputTitle, string inputImageLink, string inputDesc)
    {

        triggerButton.GetComponentInChildren<TextMeshProUGUI>().text = inputTriggerTitle;
        infoTitle.text = inputTitle;
        StartCoroutine(getSetImage(inputImageLink));
        description.text = inputDesc;
        scrollBar.GetComponent<Scrollbar>().value = 1;
    }

    IEnumerator getSetImage(string URL)
    {
        UnityWebRequest imageGet = UnityWebRequestTexture.GetTexture(URL);
        yield return imageGet.SendWebRequest();

        if (imageGet.isNetworkError || imageGet.isHttpError)
        {
            Debug.Log("Error in retrieving image.");
            image.sprite = null;
        }
        else
        {
            Debug.Log("Retrieved image.");
            Texture2D imageTexture = DownloadHandlerTexture.GetContent(imageGet);
            Sprite textureToSprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), Vector2.zero);
            image.sprite = textureToSprite;
        }
    }

    public void setLink(string linkTitle, string inputLink)
    {
        if (inputLink == "")
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

    //Sets up whatever panel is needed for the action.
    public void panelSetup(GameObject panel, Transform panelHide)
    {
        actionButton.GetComponent<Button>().onClick.AddListener(() => ToggleUIObject(panel, menuShown));
        closeButton = panel.transform.Find("TitlePanel").transform.Find("Close");
        closeButton.GetComponent<Button>().onClick.AddListener(() => ToggleUIObject(panel, panelHide));
    }

    //No action.
    public void setAction()
    {
        actionButton.SetActive(false);
    }

    //Panel action.
    public void setAction(string actionTitle)
    {
        actionButton.SetActive(true);
        actionButton.GetComponentInChildren<TextMeshProUGUI>().text = actionTitle;
    }

    //Video action.
    public void setAction(string actionTitle, string inputURL)
    {
        actionButton.SetActive(true);
        actionButton.GetComponentInChildren<TextMeshProUGUI>().text = actionTitle;
        this.GetComponent<VideoManager>().setURL(inputURL);
        actionButton.GetComponent<Button>().onClick.AddListener(() => this.GetComponent<VideoManager>().startVideo());
    }

    public void openLink()
    {
        if (webLink == "")
        {
            return;
        }
        else
        {
            Application.OpenURL(webLink);
        }
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

    public void removeListeners()
    {
        actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        closeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log("Listeners Removed");
    }
}
