using UnityEngine;

public class FloorTeleport : MonoBehaviour
{
    CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void Teleport(GameObject target)
    {
        controller.enabled = false;
        gameObject.transform.position = target.transform.position;
        controller.enabled = true;
    }
}
