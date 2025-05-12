using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public void Restart()
    {
        Debug.Log("Restart Game");
        SceneManager.LoadScene(1);
    }

    public void Title()
    {
        Debug.Log("Title Screen");
        SceneManager.LoadScene(0);
    }
}
