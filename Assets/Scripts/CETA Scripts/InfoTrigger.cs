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
                getInfo();
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
        getInfo();
    }

    async void getInfo()
    {
        if (sprite == null)
        {
            var task = await storage_ref.Child(ImageLocation).GetDownloadUrlAsync();
            StartCoroutine(SetInfo(task));
        }
        else
        {
            ManagerCalls(gameObject.GetComponentInChildren<Text>().text);
        }
    }

    IEnumerator SetInfo(Uri URL)
    {
        loadingGif.SetActive(true);
        UnityWebRequest imageGet = UnityWebRequestTexture.GetTexture(URL);
        yield return imageGet.SendWebRequest();

        if (imageGet.isNetworkError || imageGet.isHttpError)
        {
            Debug.Log("Error in retrieving image.");
            ManagerCalls("NetworkError");
        }
        else
        {
            Texture2D imageTexture = DownloadHandlerTexture.GetContent(imageGet);
            sprite = Sprite.Create(imageTexture, new Rect(0, 0, imageTexture.width, imageTexture.height), Vector2.zero);
            ManagerCalls(gameObject.GetComponentInChildren<Text>().text);
        }
    }

    void ManagerCalls(string text)
    {
        loadingGif.SetActive(false);
        managerCall.setUpInfoClose(false);
        managerCall.setCommonInfo(TriggerTitle, displayTitle, sprite, text);
        managerCall.setLink(webButtonTitle, webLink);
        prepareAction();
        managerCall.ToggleInfoMenu();
    }
}
