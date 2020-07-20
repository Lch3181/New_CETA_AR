using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ARManager : MonoBehaviour
{
    ///Used to transition between scenes.
    [SerializeField]
    private GameObject blackScreen;

    ///Gives users a choice on switching modes.
    [SerializeField]
    private GameObject arChoice;

    [SerializeField]
    private TextMeshProUGUI sceneText;

    ///Various Elements to turn off/on when the AR mode is changed.
    [SerializeField]
    private GameObject menuButton;

    [SerializeField]
    private GameObject miniMap;

    [SerializeField]
    private GameObject arBackButton;
    
    ///Represents the movement and camera touch controls.
    [SerializeField]
    private GameObject joyMove;

    [SerializeField]
    private GameObject joyCamera;

    /// <summary>
    /// Enable AR Yes.No UI
    /// </summary>
    public void toggleARWindow()
    {
        arChoice.SetActive(!arChoice.activeSelf);
    }

    /// <summary>
    /// Toggle UI for AR Mode
    /// </summary>
    public void toggleARTourObjects()
    {
        menuButton.SetActive(!menuButton.activeSelf);
        miniMap.SetActive(!miniMap.activeSelf);
        arBackButton.SetActive(!arBackButton.activeSelf);
    }

    /// <summary>
    /// Switch scene with a black screen from AR/Virtual
    /// </summary>
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

    /// <summary>
    /// Switch Scene to AR
    /// </summary>
    private void toAR()
    {
        SceneManager.LoadScene("AR", LoadSceneMode.Additive);
        toggleARTourObjects();
        joyMove.SetActive(false);
        joyCamera.SetActive(false);
        iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
            "to", Color.clear, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "arBlackscreenActive"));
        sceneText.text = "Would you like to go back to the Virtual Tour Mode?";
        GameObject.Find("Side Menu Manager").GetComponent<SideMenu>().ToggleSideMenu();
    }

    /// <summary>
    /// Switch Scene to Virtual
    /// </summary>
    private void toTour()
    {
        SceneManager.UnloadSceneAsync("AR");
        toggleARTourObjects();
        joyMove.SetActive(true);
        joyCamera.SetActive(true);
        iTween.ValueTo(gameObject, iTween.Hash("from", blackScreen.GetComponent<Image>().color,
    "to", Color.clear, "time", 1.5f, "onupdate", "UpdateBlackScreenColor", "oncomplete", "arBlackscreenActive"));
        sceneText.text = "Would you like to switch to AR Mode?";
        GameObject.Find("Player").GetComponent<PlayerController>().toggleTriggerCollide();
    }

    /// <summary>
    /// Enable Black Screen for Scene Transfering
    /// </summary>
    private void arBlackscreenActive()
    {
        blackScreen.SetActive(!blackScreen.activeSelf);
        Debug.Log("Black Screen Active Changed");
    }

    /// <summary>
    /// Set Black Screen Color
    /// </summary>
    private void UpdateBlackScreenColor(Color aColor)
    {
        blackScreen.GetComponent<Image>().color = aColor;
    }
}
