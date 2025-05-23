using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
    private GameData gameData;
    void Strat()
    {
        gameData = FindFirstObjectByType<GameData>();
    } 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Verifica se a tecla ESC foi pressionada
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        gameData.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a cena atual
    }
}