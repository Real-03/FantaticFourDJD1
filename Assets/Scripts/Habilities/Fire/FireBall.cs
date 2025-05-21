using System;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float lifetime = 5f; // Tempo para autodestruição
    private const string enemyTag = "Enemy"; // Tag dos Enemy
    private const string playerTag = "Player"; // Tag dos Player

    private Animator animator;

    private Rigidbody2D rb;
    void Start()
    {
        // Destroi a bola de fogo após X segundos se não atingir nada
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
                    rb.linearVelocity = new Vector2(0, 0);
                    animator.SetBool("Explosion", true);
                    EnemyHealth enemyhealth = other.gameObject.GetComponent<EnemyHealth>();
                    enemyhealth.TakeDamage(50, other.gameObject.transform);


                    break;
                }
            default:
                {
                    rb.linearVelocity = new Vector2(0, 0);
                    animator.SetBool("Explosion", true);
                    break;
                }

        }

    }
    
    public void DestroyMe()
    {
        Destroy(gameObject);
    }

}
