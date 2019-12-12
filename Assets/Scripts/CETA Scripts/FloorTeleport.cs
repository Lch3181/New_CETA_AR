using UnityEngine;

public class FloorTeleport : MonoBehaviour
{
    CharacterController controller;
    public GameObject[] Floors;
    public GameObject[] Triggers;

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

    public void SetCurrentLayer(GameObject gameObject)
    {
        ResetLayer();

        gameObject.layer = 9;
        gameObject.SetActive(true);
        foreach(Transform child in gameObject.transform)
        {
            child.gameObject.layer = 9;
        }
    }

    private void ResetLayer()
    {
        foreach(GameObject floor in Floors)
        {
            floor.layer = 10;
            foreach(Transform child in floor.transform)
            {
                child.gameObject.layer = 10;
            }
        }

        foreach (GameObject trigger in Triggers)
        {
            trigger.SetActive(false);
        }
    }
}
