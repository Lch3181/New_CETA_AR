using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRunnerPoint : MonoBehaviour
{
    public Transform pointPosition;
    
    /// Start is called before the first frame update
    void Start()
    {
        pointPosition.position = new Vector3(Random.Range(-18,18),1,(Random.Range(-18,18)));
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Wall")
        {
            pointPosition.position = new Vector3(Random.Range(-18, 18), 1, (Random.Range(-18, 18)));
            Debug.Log("Teleported into Wall.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            pointPosition.position = new Vector3(Random.Range(-18, 18), 1, (Random.Range(-18, 18)));
            Debug.Log("Ding!");
        }
    }
}
