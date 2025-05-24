using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    float Selection = 2;

    //Play Button Sprites
    [Space(10)]
    [Header("Replay Button")]
    [SerializeField] private GameObject ReplayOff;
    [SerializeField] private GameObject ReplayOn;

    //Title Button Sprites
    [Space(10)]
    [Header("Title Button")]
    [SerializeField] private GameObject TitleOff;
    [SerializeField] private GameObject TitleOn;

    [Space(10)]
    [Header("Scenes Settings")]
    [SerializeField] private int GameScene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Selection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (Selection <= 2)
            {
                Selection++;
            }

            if (Selection > 2)
            {
                Selection = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (Selection >= 1)
            {
                Selection--;
            }

            if (Selection < 1)
            {
                Selection = 2;
            }
        }

        if (Selection == 1)
        {
            ReplayOff.SetActive(false);
            ReplayOn.SetActive(true);
            TitleOff.SetActive(true);
            TitleOn.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(GameScene);
            }
        }

        if (Selection == 2)
        {
            ReplayOff.SetActive(true);
            ReplayOn.SetActive(false);
            TitleOff.SetActive(false);
            TitleOn.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(0);
            }
        }     
    }
}
