using UnityEngine;
using UnityEngine.UI;
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;

    [SerializeField] private Animator animator;
    [SerializeField] private Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        SetHealthUI(currentHealth);

    }

    public void TakeDamage(int damage, Transform attacker)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} levou {damage} de dano! Vida restante: {currentHealth}");

        SetHealthUI(currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
            Vector2 knockback = (transform.position - attacker.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(knockback * 100f);
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} morreu!");
        
        animator.SetTrigger("Die"); // Animação de cair/morrer
        gameObject.SetActive(false);
        this.enabled = false;
        // Desativa o inimigo após animação
        //GetComponent<Collider2D>().enabled = false;
    }

    void SetHealthUI(int Health)
    {
        float targerFillAmount = (float)Health/maxHealth;
        healthBar.fillAmount = targerFillAmount > 0 ? targerFillAmount : 0;
    }
}