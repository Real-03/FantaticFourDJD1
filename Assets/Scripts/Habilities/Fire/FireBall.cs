using System;
using UnityEngine;

public class FireBall : MonoBehaviour
{
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
            case "Thing":
            {
                break;
            }
            case enemyTag:
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
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
