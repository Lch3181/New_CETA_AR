using UnityEngine;

public class FloorTeleport : MonoBehaviour
{
    CharacterController controller;
    public GameObject buttons;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            buttons.SetActive(false);
        }
        else
        {
            buttons.SetActive(true);
        }
    }

    public void Teleport(GameObject target)
    {
        controller.enabled = false;
        gameObject.transform.position = target.transform.position;
        controller.enabled = true;
    }
}
