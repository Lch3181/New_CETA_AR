using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public Canvas canvas;
    public GameObject miniMap;
    private bool Show;
    private RectTransform rectTransform;
    public GameObject disablePanel;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        disablePanel.SetActive(false);
    }

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

    public void ToggleMenu(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
