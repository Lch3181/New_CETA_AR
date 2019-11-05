using UnityEngine;

public class SideMenu : MonoBehaviour
{
    public Canvas canvas;
    private bool Show;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
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

    public void ToggleGameObject(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
