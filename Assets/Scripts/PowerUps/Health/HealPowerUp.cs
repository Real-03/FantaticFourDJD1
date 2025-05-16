using UnityEngine;

public class HealPowerUp : MonoBehaviour
{
    public float healRadius = 0.5f; // Raio de detecção
    public LayerMask playerLayer;   // Layer do jogador
    public float healAmount = 25f;

    void Update()
    {
        DetectPlayerAndHeal();
    }

    void DetectPlayerAndHeal()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, healRadius, playerLayer);

        foreach (Collider2D hit in hits)
        {
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.HealPlayer(healAmount);
                Destroy(gameObject); // Destroi o power-up após uso
                break; // Só cura um jogador
            }
        }
    }

    // Gizmo para visualizar o raio no editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
}