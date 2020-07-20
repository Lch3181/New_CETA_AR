using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRunnerManager : MonoBehaviour
{
    public GameUIManager UICall;
    public GameObject Player;
    public GameObject controls;

    public int score = 0;

    /// <summary>
    /// Thread Game Setup
    /// </summary>
    public void gameSetup()
    {
        StartCoroutine(gameStart());
    }

    /// <summary>
    /// Thread startup UI and wait for 2 seconds,
    /// 
    /// Then enable player controll
    /// </summary>
    private IEnumerator gameStart()
    {
        StartCoroutine(UICall.startCountdown());
        yield return new WaitForSeconds(2f);

        UICall.toggleGameUI(controls);
        Player.GetComponent<MazeRunnerPlayer>().isPlaying = true;
    }

    /// <summary>
    /// Pause the UI and disable the player controll
    /// </summary>
    public void gamePause()
    {
        UICall.toggleGameUI(UICall.pauseScreen);
        Player.GetComponent<MazeRunnerPlayer>().isPlaying = !Player.GetComponent<MazeRunnerPlayer>().isPlaying;
    }

    /// <summary>
    /// Reset UI and player position back to middle
    /// </summary>
    public void gameReset()
    {
        UICall.gameReset();
        score = 0;
        UICall.setScore(score);
        Player.GetComponent<Transform>().position = new Vector3(0, .5f, 0);
    }

    /// <summary>
    /// Enable game end UI
    /// </summary>
    public void gameEnd()
    {
        UICall.gameEnd();
        UICall.toggleGameUI(controls);
    }

    /// <summary>
    /// Score++ and update UI
    /// </summary>
    public void addPoint()
    {
        score++;
        UICall.setScore(score);
    }
}
