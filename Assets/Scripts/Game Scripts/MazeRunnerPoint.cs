using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRunnerPoint : MonoBehaviour
{
    public Transform pointPosition;

    /// <summary>
    /// Start is called before the first frame update
    ///  
    /// Reset Player back to middle
    /// </summary>
    void Start()
    {
        pointPosition.position = new Vector3(Random.Range(-18,18),1,(Random.Range(-18,18)));
    }

    /// <summary>
    /// When player GOT STUCK into the wall, reset player back to middle
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Wall")
        {
            pointPosition.position = new Vector3(Random.Range(-18, 18), 1, (Random.Range(-18, 18)));
            Debug.Log("Teleported into Wall.");
        }
    }

    /// <summary>
    /// When player HITS the wall, reset player back to middle
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pointPosition.position = new Vector3(Random.Range(-18, 18), 1, (Random.Range(-18, 18)));
            Debug.Log("Ding!");
        }
    }
}
