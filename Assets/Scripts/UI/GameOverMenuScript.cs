using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    float Selection = 2;

    //Play Button Sprites
    [Space(10)]
    [Header("Restart Button")]
    [SerializeField] private GameObject RestartOff;
    [SerializeField] private GameObject RestartOn;

    //Title Button Sprites
    [Space(10)]
    [Header("Title Button")]
    [SerializeField] private GameObject TitleOff;
    [SerializeField] private GameObject TitleOn;

    [Space(10)]
    [Header("Scenes Settings")]
    [SerializeField] private int GameScene;

    private GameData gameData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Selection = 1;
        gameData = FindFirstObjectByType<GameData>();
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
            RestartOff.SetActive(true);
            RestartOn.SetActive(false);
            TitleOff.SetActive(false);
            TitleOn.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameData.ResetScore();
                SceneManager.LoadScene(0);
            }
        }

        if (Selection == 2)
        {
            RestartOff.SetActive(false);
            RestartOn.SetActive(true);
            TitleOff.SetActive(true);
            TitleOn.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameData.ResetScore();
                SceneManager.LoadScene(GameScene);
            }
        }
    }
}
