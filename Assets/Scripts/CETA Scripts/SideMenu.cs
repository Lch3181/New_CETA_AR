using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manager for Side Menu
/// </summary>
public class SideMenu : MonoBehaviour
{
    public Canvas canvas;
    public GameObject[] sideMenus;
    public GameObject miniMap;
    private bool Show;
    private RectTransform rectTransform;
    public GameObject disablePanel;

    /// <summary>
    /// Start is called on the frame when a script is enabled
    /// </summary>
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        disablePanel.SetActive(false);
    }

    /// <summary>
    /// Update is called once every frame
    /// </summary>
    void Update()
    {
        Display();
    }

    /// <summary>
    /// Animation for Show/Hide Side Menu
    /// </summary>
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

    /// <summary>
    /// Show/Hide Side Menu
    /// </summary>
    public void ToggleSideMenu()
    {
        Show = !Show;

        if (!Show)
        {
            ResetMenu();
            sideMenus[0].SetActive(true);
        }
    }

    /// <summary>
    /// Toggle target menu page
    /// </summary>
    public void ToggleMenu(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    /// <summary>
    /// Disable all menu pages
    /// </summary>
    private void ResetMenu()
    {
        foreach (var gameObject in sideMenus)
        {
            gameObject.SetActive(false);
        }
    }

}
