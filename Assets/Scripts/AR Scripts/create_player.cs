using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
public class create_player : MonoBehaviour
{
    public Transform play;
    public GameObject player;

    /// Endable AR and create new player
    private void OnMouseDown()
    {
        VuforiaBehaviour.Instance.enabled = false;
        Instantiate(player, new Vector3(0, 0, 2), play.rotation);
    }

}
