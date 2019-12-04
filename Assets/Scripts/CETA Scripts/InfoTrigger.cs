using Firebase.Storage;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class InfoTrigger : MonoBehaviour
{
    //Used to call to the CETA UI Manager.
    public CETAUIManager managerCall;

    //database
    FirebaseStorage storage;
    StorageReference storage_ref;

    //What the button that triggers the info UI should display.
    public string TriggerTitle;
    //The title of the UI.
    public string displayTitle;
    //What image should be shown. (The URL link to the image.)
    public string ImageLocation;

    //loading.gif
    private GameObject loadingGif;

    //store image texture in scope
    public Sprite sprite;

    //Website button text.
    public string webButtonTitle;
    //Website Link.
    public string webLink;

    //Action button title.
    public string actionTitle;
    //What the action does.

    public enum actionType { video, panel, scene, none };

    public actionType setType;

    //The panel to show.
    public GameObject panel;
    //Where the panel should hide.
    Vector3 panelHide;

    //The video URL to play;
    public string videoURL;

    //The scene the trigger should load.
    public string triggerScene;

    //Denotes if the player is in a trigger.
    bool inTrigger = false;

    private void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storage_ref = storage.GetReferenceFromUrl("gs://root-wharf-237820.appspot.com");
        loadingGif =  GameObject.Find("CETA Manager/Canvas/loading.gif");
    }

    private void Update()
    {
        if (setType == actionType.video)
        {
            panelHide = new Vector3(Screen.width / 2f, Screen.height * 8 / 2f, 0f);
        }
        else
        {
            panelHide = new Vector3(-Screen.width * 3, Screen.height / 2f, 0f);
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            if (managerCall.infoMenuShown)
            {
                Debug.Log("Panel Already Shown.");
            }
            else
            {
                getInfo(false);
            }
        }
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "Player")
        { 
            if(!inTrigger)
            {
                inTrigger = !inTrigger;
            }
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player")
        {
            managerCall.triggerButtonOff();
            managerCall.removeListeners();
        }
    }

    private void prepareAction()
    {
        if (setType != actionType.none && setType != actionType.scene)
        {
            managerCall.panelSetup(panel,panelHide);
        }

        switch (setType)
        {
            case actionType.video:
                managerCall.setAction(actionTitle, videoURL);
                break;
            case actionType.panel:
                managerCall.setAction(actionTitle);
                break;
            case actionType.scene:
                managerCall.setActionS(actionTitle, triggerScene);
                break;
            default:
                managerCall.setAction();
                break;
        }
    }

    public void ButtonActiveEvent()
    {
        getInfo(true);
    }

    async void getInfo(bool isEvent)
    {
        if (sprite == null)
        {
            var task = await storage_ref.Child(ImageLocation).GetDownloadUrlAsync();
            StartCoroutine(SetInfo(task,isEvent));
        }
        else
        {
            ManagerCalls(gameObject.GetComponentInChildren<Text>().text, isEvent);
        }
    }

    IEnumerator SetInfo(Uri URL, bool isEvent)
    {
        loadingGif.SetActive(true);
        UnityWebRequest imageGet = UnityWebRequestTexture.GetTexture(URL);
        yield return imageGet.SendWebRequest();

        if (imageGet.isNetworkError || imageGet.isHttpError)
        {
            Debug.Log("Error in retrieving image.");
            ManagerCalls("NetworkError", isEvent);
        }
        else
        {
            Texture2D imageTexture = DownloadHandlerTexture.GetContent(imageGet);
            sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), Vector2.zero);
            ManagerCalls(gameObject.GetComponentInChildren<Text>().text, isEvent);
        }
    }

    void ManagerCalls(string text, bool isEvent)
    {
        loadingGif.SetActive(false);
        managerCall.setUpInfoClose(isEvent);
        managerCall.setCommonInfo(TriggerTitle, displayTitle, sprite, text);
        managerCall.setLink(webButtonTitle, webLink);
        prepareAction();

        if (isEvent)
        {
            managerCall.eventToggleInfoMenu();
        }
        else
        {
            managerCall.triggerButtonOn();
        }
    }
}
