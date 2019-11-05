using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public Canvas canvas;
    public GameObject TeleportMenu;
    public GameObject MiniMap;
    private bool Show;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Display();
    }

    private void Display()
    {
        if (Show)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, new Vector3(rectTransform.rect.width * rectTransform.localScale.x * canvas.scaleFactor / 2f, Screen.height / 2f, 0f), Time.deltaTime * 10f);
        }
        else
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, new Vector3(-rectTransform.rect.width * rectTransform.localScale.x * canvas.scaleFactor / 2f, Screen.height / 2f, 0f), Time.deltaTime * 10f);

        }
    }

    public void ToggleSideMenu()
    {
        Show = !Show;
    }

    public void ToggleTeleportMenu()
    {
        TeleportMenu.SetActive(!TeleportMenu.activeSelf);
    }

    public void ToggleMiniMap()
    {
        MiniMap.SetActive(!MiniMap.activeSelf);
    }
}
