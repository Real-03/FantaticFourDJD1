using System;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float lifetime = 5f; // Tempo para autodestruição
    private const string enemyTag = "Enemy"; // Tag dos Enemy
    private const string playerTag = "Player"; // Tag dos Player
    
    private const string habilityTag = "Hability"; // Tag das Hability
    

    private Animator animator;

    private Rigidbody2D rb;
    void Start()
    {
        
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // Destroi a bola de fogo após X segundos se não atingir nada
        Destroy(gameObject, lifetime);

    }

    void OnTriggerEnter2D(Collider2D other)
    {

        
        switch (other.tag)
        {
            
            case playerTag:// Se tocar no Player, ignora a colisão com ele
                {
                    break;
                }
            case habilityTag:// Se tocar nuam Hability, ignora a colisão com ela
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
