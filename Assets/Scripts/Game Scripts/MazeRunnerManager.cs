using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRunnerManager : MonoBehaviour
{
    public GameUIManager UICall;
    public GameObject Player;
    public GameObject controls;

    public int score = 0;

    public void gameSetup()
    {
        StartCoroutine(gameStart());
    }

    private IEnumerator gameStart()
    {
        StartCoroutine(UICall.startCountdown());
        yield return new WaitForSeconds(2f);

        UICall.toggleGameUI(controls);
        Player.GetComponent<MazeRunnerPlayer>().isPlaying = true;
    }

    public void gamePause()
    {
        UICall.toggleGameUI(UICall.pauseScreen);
        Player.GetComponent<MazeRunnerPlayer>().isPlaying = !Player.GetComponent<MazeRunnerPlayer>().isPlaying;
    }

    public void gameReset()
    {
        UICall.gameReset();
        score = 0;
        UICall.setScore(score);
        Player.GetComponent<Transform>().position = new Vector3(0, .5f, 0);
    }

    public void gameEnd()
    {
        UICall.gameEnd();
        UICall.toggleGameUI(controls);
    }

    public void addPoint()
    {
        score++;
        UICall.setScore(score);
    }
}
