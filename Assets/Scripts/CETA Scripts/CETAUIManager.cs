using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Firebase.Storage;

public class CETAUIManager : MonoBehaviour
{
    //database
    FirebaseStorage storage;
    StorageReference storage_ref;

    //UI
    public Canvas canvas;

    //The info panel itself;
    public GameObject infoPanel;
    private RectTransform InfoRectTransform;

    Vector3 panelShow;
    Vector3 panelHide;

    //The button that triggers the info panel.
    public GameObject TriggerButton;
    private RectTransform TriggerButtonRectTransform;

    Vector3 buttonShow;
    Vector3 buttonHide;

    //The menu button and the panel that disables the menu.
    public GameObject menuButton;
    public GameObject disablePanel;

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

    //Determines if the panel associated with a trigger is shown.
    public bool triggerPanelShown = false;

    //A link to a website.
    private string webLink;

    //Represents the close button for a configurable panel.
    Transform closeButton = null;

    //Represents the close button for the info panel.
    Transform infoPanelClose;

    //Represents the player.
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storage_ref = storage.GetReferenceFromUrl("gs://root-wharf-237820.appspot.com");
        InfoRectTransform = infoPanel.GetComponent<RectTransform>();
        TriggerButtonRectTransform = TriggerButton.GetComponent<RectTransform>();
        infoPanelClose = infoPanel.transform.Find("TitlePanel").transform.Find("Close");
    }

    private void Update()
    {
        panelShow = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        panelHide = new Vector3(-Screen.width * 3, Screen.height / 2f, 0f);
    }

    public void setCommonInfo(string inputTriggerTitle, string inputTitle, Sprite sprite, string inputDesc)
    {
        TriggerButton.GetComponentInChildren<Text>().text = inputTriggerTitle;
        infoTitle.text = inputTitle;
        image.sprite = sprite;
        description.text = inputDesc;
        scrollBar.GetComponent<Scrollbar>().value = 1;
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
    public void panelSetup(GameObject panel, Vector3 panelHide)
    {
        actionButton.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(moveUIObject(panel.GetComponent<RectTransform>(), panelShow, 1f)));
        closeButton = panel.transform.Find("TitlePanel").transform.Find("Close");
        closeButton.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(moveUIObject(panel.GetComponent<RectTransform>(), panelHide, .5f)));
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
    public async void setAction(string actionTitle, string inputURL)
    {
        actionButton.SetActive(true);
        actionButton.GetComponentInChildren<TextMeshProUGUI>().text = actionTitle;
        var uri = await storage_ref.Child(inputURL).GetDownloadUrlAsync(); //get link from database
        this.GetComponent<VideoManager>().setURL(uri.ToString());
        actionButton.GetComponent<Button>().onClick.AddListener(() => this.GetComponent<VideoManager>().startVideo());
    }

    //Scene action.
    public void setActionS(string actionTitle, string inputScene)
    {
        actionButton.SetActive(true);
        actionButton.GetComponentInChildren<TextMeshProUGUI>().text = actionTitle;
        this.GetComponent<ScenesManager>().setScene(inputScene);
        actionButton.GetComponent<Button>().onClick.AddListener(() => this.GetComponent<ScenesManager>().toggleSceneWindow());
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

    public IEnumerator moveUIObject(RectTransform UIObject, Vector3 target, float addTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + addTime)
        {
            UIObject.position = Vector3.Lerp(UIObject.position, target, (Time.time - startTime) / addTime);
            yield return null;
        }
        UIObject.position = target;
    }

    public void triggerButtonOn()
    {
        buttonShow = new Vector3(Screen.width - TriggerButtonRectTransform.rect.width * canvas.scaleFactor / 1.5f, TriggerButtonRectTransform.rect.height * canvas.scaleFactor, 0f);
        StartCoroutine(moveUIObject(TriggerButtonRectTransform, buttonShow, .5f));
    }

    public void triggerButtonOff()
    {
        buttonHide = new Vector3(Screen.width + TriggerButtonRectTransform.rect.width * canvas.scaleFactor / 2, TriggerButtonRectTransform.rect.height * canvas.scaleFactor, 0f);
        StartCoroutine(moveUIObject(TriggerButtonRectTransform, buttonHide, .5f));
    }

    public void ToggleInfoMenu()
    {
        infoMenuShown = !infoMenuShown;
        if(infoMenuShown)
        {
            StartCoroutine(moveUIObject(InfoRectTransform, panelShow, .5f));
            player.GetComponent<PlayerController>().toggleMove();
            triggerButtonOff();
            menuButton.SetActive(false);
        }
        else
        {
            StartCoroutine(moveUIObject(InfoRectTransform, panelHide, .5f));
            player.GetComponent<PlayerController>().toggleMove();
            triggerButtonOn();
            menuButton.SetActive(true);
        }
    }

    public void eventToggleInfoMenu()
    {
        infoMenuShown = !infoMenuShown;
        if (infoMenuShown)
        {
            StartCoroutine(moveUIObject(InfoRectTransform, panelShow, .5f));
            player.GetComponent<PlayerController>().toggleMove();
            menuButton.GetComponent<Button>().interactable = false;
            disablePanel.SetActive(true);
        }
        else
        {
            StartCoroutine(moveUIObject(InfoRectTransform, panelHide, .5f));
            player.GetComponent<PlayerController>().toggleMove();
            menuButton.GetComponent<Button>().interactable = true;
            disablePanel.SetActive(false);
            removeListeners();
        }
    }

    //Designates info close if opened from a trigger.
    public void setUpInfoClose(bool eventOpened)
    {
        if (eventOpened)
        {
            infoPanelClose.GetComponent<Button>().onClick.AddListener(() => eventToggleInfoMenu());
        }
        else
        {
            infoPanelClose.GetComponent<Button>().onClick.AddListener(() => ToggleInfoMenu());
        }
    }

    public void removeListeners()
    {
        actionButton.GetComponent<Button>().onClick.RemoveAllListeners();
        if(closeButton != null)
        {
            closeButton.GetComponent<Button>().onClick.RemoveAllListeners();
            closeButton = null;
        }
        else
        {
            Debug.Log("No panel for close button.");
        }
        infoPanelClose.GetComponent<Button>().onClick.RemoveAllListeners();
        Debug.Log("Removed All Listeners");
    }
}
