using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public Button RestartButton;
    public Button TitleButton;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        EventSystem.current.SetSelectedGameObject(RestartButton.gameObject);

        RestartButton.onClick.AddListener(Restart);
        TitleButton.onClick.AddListener(Title);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(RestartButton.gameObject);
        }
    }

    void Restart()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(1);
    }

    void Title()
    {
        Debug.Log("Title Screen");
        SceneManager.LoadScene(0);
    }
}
