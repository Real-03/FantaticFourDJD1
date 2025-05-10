using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Button PlayButton;
    public Button TutorialButton;
    public Button OptionsButton;
    public Button QuitButton;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        EventSystem.current.SetSelectedGameObject(PlayButton.gameObject);

        // Liga os botões às funções
        PlayButton.onClick.AddListener(Play);
        OptionsButton.onClick.AddListener(Options);
        TutorialButton.onClick.AddListener(Tutorial);
        QuitButton.onClick.AddListener(Quit);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(PlayButton.gameObject);
        }
    }

    void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(1);
    }

    void Tutorial()
    {
        Debug.Log("Tutorial");
    }

    void Options()
    {
        Debug.Log("Options");
        
    }

    void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}