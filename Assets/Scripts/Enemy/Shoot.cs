using UnityEngine;

public class Shoot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float lifetime = 5f; // Tempo para autodestruição
    private const string enemyTag = "Enemy"; // Tag dos Enemy
    private const string playerTag = "Player"; // Tag dos Player

    void Start()
    {
        // Destroi a bola de fogo após X segundos se não atingir nada
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        // Se tocar no Player, ignora a colisão com ele
        switch (other.tag)
        {
            case playerTag:
            {

                break;
            }
            case "Beast":
            {

                break;
            }
            case enemyTag:
            {

                break;
            }
            default:
            {
                Destroy(gameObject);
                break;
            }
                
        }

    }
}
