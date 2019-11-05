using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.Networking;

public class CETAUIManager : MonoBehaviour
{
    public Canvas canvas;

    public GameObject videoScreen;
    public VideoPlayer videoPlayer;
    private RectTransform VideoScreenRectTransform;
    private bool VideoScreenShow;

    //The info panel itself;
    public GameObject infoPanel;
    private RectTransform InfoRectTransform;

    //The button that triggers the info panel.
    public GameObject TriggerButton;
    private RectTransform TriggerButtonRectTransform;
    private bool TriggerButtonShow;

    //What should be shown as the title of the info UI.
    [SerializeField]
    private TextMeshProUGUI infoTitle;

    //What image should be shown.
    [SerializeField]
    private Image image;

    //The description of the specified location.
    [SerializeField]
    private TextMeshProUGUI description;

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
    public bool infoMenuShown;

    //A link to a website.
    private string webLink;

    //Represents the close button for a configurable panel.
    Transform closeButton;

    //Represents the player.
    [SerializeField]
    private CharacterController playerControls;

    private void Start()
    {
        InfoRectTransform = infoPanel.GetComponent<RectTransform>();
        TriggerButtonRectTransform = TriggerButton.GetComponent<RectTransform>();
        VideoScreenRectTransform = videoScreen.GetComponent<RectTransform>();
    }

    private void Update()
    {
        Display();
    }

    private void Display()
    {
        if (infoMenuShown)
        {
            InfoRectTransform.position = Vector3.Lerp(InfoRectTransform.position, new Vector3(Screen.width / 2f, Screen.height / 2f, 0f), Time.deltaTime * 10f);
        }
        else
        {
            InfoRectTransform.position = Vector3.Lerp(InfoRectTransform.position, new Vector3(-Screen.width / 2f, Screen.height / 2f, 0f), Time.deltaTime * 10f);
        }

        //Trigger Button
        if (TriggerButtonShow)
        {
            TriggerButtonRectTransform.position = Vector3.Lerp(TriggerButtonRectTransform.position, new Vector3(Screen.width - TriggerButtonRectTransform.rect.width * canvas.scaleFactor / 1.5f, TriggerButtonRectTransform.rect.height * canvas.scaleFactor, 0f), Time.deltaTime * 10f);
        }
        else
        {
            TriggerButtonRectTransform.position = Vector3.Lerp(TriggerButtonRectTransform.position, new Vector3(Screen.width + TriggerButtonRectTransform.rect.width * canvas.scaleFactor / 2, TriggerButtonRectTransform.rect.height * canvas.scaleFactor, 0f), Time.deltaTime * 10f);
        }

        //Video Screen
        if (VideoScreenShow)
        {
            VideoScreenRectTransform.position = Vector3.Lerp(VideoScreenRectTransform.position, new Vector3(Screen.width / 2f, Screen.height / 2f, 0f), Time.deltaTime * 10f);
        }
        else
        {
            VideoScreenRectTransform.position = Vector3.Lerp(VideoScreenRectTransform.position, new Vector3(Screen.width / 2f, Screen.height * 2, 0f), Time.deltaTime * 10f);
        }
    }

    public void setCommonInfo(string inputTriggerTitle, string inputTitle, string inputImageLink, string inputDesc)
    {
        TriggerButton.GetComponentInChildren<Text>().text = inputTriggerTitle;
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

    public void startVideo()
    {
        VideoScreenShow = true;
        videoPlayer.Play();
    }

    public void pauseToggle()
    {
        if (videoPlayer.isPaused)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }

    }

    public void closeVideo()
    {
        videoPlayer.Stop();
        VideoScreenShow = false;
    }

    public void triggerButtonOn()
    {
        if (infoMenuShown)
        {
            triggerButtonOff();
        }
        else
        {
            TriggerButtonShow = true;
        }
    }

    public void triggerButtonOff()
    {
        TriggerButtonShow = false;
    }

    public void ToggleInfoMenu()
    {
        infoMenuShown = !infoMenuShown;

    }

    public void removeListeners()
    {
        actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        closeButton.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log("Listeners Removed");
    }
}
