using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayControllerScript : MonoBehaviour
{
    float Selection = 2;

    //Play Button Sprites
    [Space(10)]
    [Header("Back Button")]
    [SerializeField] private GameObject BackOff;
    [SerializeField] private GameObject BackOn;

    [Space(10)]
    [Header("Scenes Settings")]
    [SerializeField] private GameObject HTP_Screen;
    [SerializeField] private GameObject MenuScreen;
    [SerializeField] private GameObject OptionsScreen;

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
            BackOff.SetActive(false);
            BackOn.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OptionsScreen.SetActive(false);
                HTP_Screen.SetActive(false);
                MenuScreen.SetActive(true);
            }
        }

        if(Selection == 2)
        {
            BackOff.SetActive(true);
            BackOn.SetActive(false);
        }
    }
}
