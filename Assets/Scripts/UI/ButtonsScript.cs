using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsScript : MonoBehaviour
{
    float Selection = 4;

    //Play Button Sprites
    [Space(10)]
    [Header("Play Button")]
    [SerializeField] private GameObject PlayOff;
    [SerializeField] private GameObject PlayOn;

    //How To Play Button Sprites
    [Space(10)]
    [Header("How To Play Button")]
    [SerializeField] private GameObject HTP_Off;
    [SerializeField] private GameObject HTP_On;

    //Options Button Sprites
    [Space(10)]
    [Header("Options Button")]
    [SerializeField] private GameObject OptionsOff;
    [SerializeField] private GameObject OptionsOn;

    //Quit Button Sprites
    [Space(10)]
    [Header("Quit Button")]
    [SerializeField] private GameObject QuitOff;
    [SerializeField] private GameObject QuitOn;

    [Space(10)]
    [Header("Scenes Settings")]
    [SerializeField] private int GameScene;
    [SerializeField] private GameObject HTP_Screen;
    [SerializeField] private GameObject OptionsScreen;
    [SerializeField] private GameObject MenuScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Selection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (Selection <= 4)
            {
                Selection++;
            }

            if (Selection > 4)
            {
                Selection = 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (Selection >= 1)
            {
                Selection--;
            }

            if (Selection < 1)
            {
                Selection = 4;
            }
        }

        if (Selection == 1)
        {
            PlayOff.SetActive(false);
            PlayOn.SetActive(true);
            HTP_Off.SetActive(true);
            HTP_On.SetActive(false);
            OptionsOff.SetActive(true);
            OptionsOn.SetActive(false);
            QuitOff.SetActive(true);
            QuitOn.SetActive(false);

            if (Input.GetKeyUp(KeyCode.Return))
            {
                SceneManager.LoadScene(GameScene);
            }
        }

        if (Selection == 2)
        {
            PlayOff.SetActive(true);
            PlayOn.SetActive(false);
            HTP_Off.SetActive(false);
            HTP_On.SetActive(true);
            OptionsOff.SetActive(true);
            OptionsOn.SetActive(false);
            QuitOff.SetActive(true);
            QuitOn.SetActive(false);

            if (Input.GetKeyUp(KeyCode.Return))
            {
                MenuScreen.SetActive(false);
                HTP_Screen.SetActive(true);
            }
        }

        if (Selection == 3)
        {
            PlayOff.SetActive(true);
            PlayOn.SetActive(false);
            HTP_Off.SetActive(true);
            HTP_On.SetActive(false);
            OptionsOff.SetActive(false);
            OptionsOn.SetActive(true);
            QuitOff.SetActive(true);
            QuitOn.SetActive(false);

            if (Input.GetKeyUp(KeyCode.Return))
            {
                MenuScreen.SetActive(false);
                OptionsScreen.SetActive(true);
            }
        }

        if (Selection == 4)
        {
            PlayOff.SetActive(true);
            PlayOn.SetActive(false);
            HTP_Off.SetActive(true);
            HTP_On.SetActive(false);
            OptionsOff.SetActive(true);
            OptionsOn.SetActive(false);
            QuitOff.SetActive(false);
            QuitOn.SetActive(true);

            if (Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("Quit");
                Application.Quit();
            }
        }
    }
}
