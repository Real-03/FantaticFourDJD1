using UnityEngine;

public class FireBall : MonoBehaviour
{
   public float lifetime = 5f; // Tempo para autodestruição
    public string enemyTag = "Enemy"; // Tag dos inimigos
    public string playerTag = "Player"; // Tag do jogador

    private Collider2D fireballCollider;

    void Start()
    {
        // Pega o próprio Collider2D
        fireballCollider = GetComponent<Collider2D>();

        // Destroi a bola de fogo após X segundos se não atingir nada
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Se atingir um inimigo, destrói o inimigo e a bola de fogo
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Destroy(collision.gameObject); // Destroi o inimigo
            Destroy(gameObject); // Destroi a bola de fogo
        }
        // Se atingir uma parede, chão ou plataforma, destrói a bola de fogo
        else if (collision.gameObject.CompareTag("Ground") || 
                 collision.gameObject.CompareTag("Platform") || 
                 collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se tocar no Player, ignora a colisão com ele
        if (other.CompareTag(playerTag))
        {
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), fireballCollider);
        }
    }

}
