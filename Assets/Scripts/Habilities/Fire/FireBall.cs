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
<<<<<<< HEAD
=======
        
>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
    }

    void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
=======

>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
        // Se tocar no Player, ignora a colisão com ele
        switch (other.tag)
        {
            case playerTag:
            {
<<<<<<< HEAD
                Debug.Log(other.tag);
=======
                break;
            }
            case "Thing":
            {
>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
                break;
            }
            case enemyTag:
            {
<<<<<<< HEAD
                Debug.Log(other.tag);
                Destroy(other.gameObject);
=======
                EnemyHealth enemyhealth =other.gameObject.GetComponent<EnemyHealth>();
                enemyhealth.TakeDamage(50, other.gameObject.transform);
>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
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
