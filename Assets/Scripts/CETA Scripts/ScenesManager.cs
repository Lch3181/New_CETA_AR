using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    //Name of the scene to load.
    private string triggerScene;

    //Used to transition between scenes.
    [SerializeField]
    private GameObject blackScreen;

    //Gives users a choice on leaving the current scene.
    [SerializeField]
    private GameObject sceneChoice;

    private void Start()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
               "to", Color.clear, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "blackScreenActive"));
    }

    public void setScene(string inputScene)
    {
        triggerScene = inputScene;
    }

    public void toggleSceneWindow()
    {
        sceneChoice.SetActive(!sceneChoice.activeSelf);
    }

    public void sceneOut()
    {
        toggleSceneWindow();
        blackScreenActive();
        iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
            "to", Color.black, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "changeScenes"));
    }

    private void changeScenes()
    {
        SceneManager.LoadScene(triggerScene, LoadSceneMode.Single);
    }

    private void UpdateBlackScreenColor(Color aColor)
    {
        blackScreen.GetComponent<Image>().color = aColor;
    }

    private void blackScreenActive()
    {
        blackScreen.SetActive(!blackScreen.activeSelf);
        Debug.Log("Black Screen Active Changed");
    }


}
