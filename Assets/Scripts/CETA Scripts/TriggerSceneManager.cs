using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerSceneManager : MonoBehaviour
{
    //Name of the scene to load.
    private string triggerScene;

    //Used to transition between scenes.
    [SerializeField]
    private GameObject blackScreen;

    private void Start()
    {
        toggleBlackScreen();
    }

    public void toggleBlackScreen()
    {
        if (blackScreen.GetComponent<Image>().color == Color.clear)
        {
            blackScreenActive();
            iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
                "to", Color.black, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "changeScenes"));
        }
        else
            iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
               "to", Color.clear, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "blackScreenActive"));
    }

    public void setScene(string inputScene)
    {
        triggerScene = inputScene;
    }

    public void changeScenes()
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
