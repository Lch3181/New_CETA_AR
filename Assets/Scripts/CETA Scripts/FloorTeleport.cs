using UnityEngine;

/// <summary>
/// Teleport player to different floor by using menu
/// </summary>
public class FloorTeleport : MonoBehaviour
{
    CharacterController controller;
    public GameObject[] Floors;
    public GameObject[] Triggers;

    /// <summary>
    /// Get PlayerController Component
    /// </summary>
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Teleport Player to Target position
    /// </summary>
    public void Teleport(GameObject target)
    {
        controller.enabled = false;
        gameObject.transform.position = target.transform.position;
        controller.enabled = true;
    }
}
