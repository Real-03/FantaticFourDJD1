using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private int MainMenuScreen;
    [SerializeField] private int GameLevelScene;

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(MainMenuScreen);
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(GameLevelScene);
    }
}
