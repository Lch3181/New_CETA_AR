using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manage Scene switching with a blacckscreen for scene transfer
/// </summary>
public class ScenesManager : MonoBehaviour
{
    ///Name of the scene to load.
    private string triggerScene;

    ///Used to transition between scenes.
    [SerializeField]
    private GameObject blackScreen;

    ///Gives users a choice on leaving the current scene.
    [SerializeField]
    private GameObject sceneChoice;

    /// <summary>
    /// Start is called on the frame when a script is enabled
    /// </summary>
    private void Start()
    {
        //Makes it so that the screen is still interactible if the black screen is turned off in the editor.
        if (blackScreen.activeSelf)
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
              "to", Color.clear, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "blackScreenActive"));
        }
        else
        {
            blackScreen.GetComponent<Image>().color = Color.clear;
        }
    }

    public void setScene(string inputScene)
    {
        triggerScene = inputScene;
    }

    /// <summary>
    /// Toggle Switch scene Yes/No window
    /// </summary>
    public void toggleSceneWindow()
    {
        sceneChoice.SetActive(!sceneChoice.activeSelf);
    }

    /// <summary>
    /// fade out scene with a blackscreen
    /// </summary>
    public void sceneOut()
    {
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
    }
}
