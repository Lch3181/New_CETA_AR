using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject pauseButton;
    public GameObject countdown;
    public GameObject scoreCounter;
    public GameObject endScore;

    public void toggleGameUI(GameObject UIObject)
    {
        UIObject.SetActive(!UIObject.activeSelf);
    }

    public void gameReset()
    {
        toggleGameUI(gameOverScreen);
        toggleGameUI(startScreen);
        toggleGameUI(countdown);
    }

    public void gameEnd()
    {
        toggleGameUI(gameOverScreen);
        toggleGameUI(pauseButton);
        toggleGameUI(scoreCounter);
    }

    public void setScore(int score)
    {
        Debug.Log("Score Up!");
        scoreCounter.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        endScore.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }

    public IEnumerator startCountdown()
    {
        toggleGameUI(startScreen);
        for (int i= 3; i>=0; i--)
        {
            if(i == 0)
            {
                countdown.GetComponent<TextMeshProUGUI>().text = "Start!";
            }
            else
            {
                countdown.GetComponent<TextMeshProUGUI>().text = i.ToString();
            }
            yield return new WaitForSeconds(.5f);
        }
        countdown.GetComponent<TextMeshProUGUI>().text = "";
        toggleGameUI(pauseButton);
        toggleGameUI(countdown);
        toggleGameUI(scoreCounter);
    }


}
