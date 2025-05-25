using UnityEngine;
using UnityEngine.SceneManagement;
public class FinisGame : MonoBehaviour
{
    [SerializeField] private string winSceneName = "WinScreen"; // Nome da cena de vitória
    private GameData gameData; // Referência ao GameData no Inspector
    void Start()
    {
        gameData =  FindFirstObjectByType<GameData>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou tem o componente PlayerMovement
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (gameData != null && gameData.GetBeacons()==6)
            {
                Debug.Log("Todos os beacons estão ativos. Carregando cena de vitória...");
                SceneManager.LoadScene(winSceneName);
            }
            else
            {
                Debug.Log("Nem todos os beacons estão ativos ainda.");
            }
        }
    }
}
