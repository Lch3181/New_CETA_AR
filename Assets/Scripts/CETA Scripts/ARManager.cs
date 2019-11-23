using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ARManager : MonoBehaviour
{
    //Used to transition between scenes.
    [SerializeField]
    private GameObject blackScreen;

    //Gives users a choice on switching modes.
    [SerializeField]
    private GameObject arChoice;

    [SerializeField]
    private TextMeshProUGUI sceneText;

    //Various Elements to turn off/on when the AR mode is changed.
    [SerializeField]
    private GameObject menuButton;

    [SerializeField]
    private GameObject miniMap;

    [SerializeField]
    private GameObject arBackButton;

    public void toggleARWindow()
    {
        arChoice.SetActive(!arChoice.activeSelf);
    }

    public void toggleARTourObjects()
    {
        menuButton.SetActive(!menuButton.activeSelf);
        miniMap.SetActive(!miniMap.activeSelf);
        arBackButton.SetActive(!arBackButton.activeSelf);
    }

    public void switchMode()
    {
        arBlackscreenActive();
        if (SceneManager.GetSceneByName("AR").isLoaded)
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
             "to", Color.black, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "toTour"));
        }
        else
        {
            iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
            "to", Color.black, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "toAR"));
        }
    }

    private void toAR()
    {
        SceneManager.LoadScene("AR", LoadSceneMode.Additive);
        //toggleARTourObjects();
        iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
            "to", Color.clear, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "arBlackscreenActive"));
        sceneText.text = "Would you like to go back to the Virtual Tour Mode?";
    }

    private void toTour()
    {
        SceneManager.UnloadSceneAsync("AR");
        //toggleARTourObjects();
        iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
    "to", Color.clear, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "arBlackscreenActive"));
        sceneText.text = "Would you like to switch to AR Mode?";
    }

    private void arBlackscreenActive()
    {
        blackScreen.SetActive(!blackScreen.activeSelf);
        Debug.Log("Black Screen Active Changed");
    }

    private void UpdateBlackScreenColor(Color aColor)
    {
        blackScreen.GetComponent<Image>().color = aColor;
    }
}
