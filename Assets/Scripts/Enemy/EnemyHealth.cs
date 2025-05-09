using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Animator animator;


    void Start()
    {
        currentHealth = maxHealth;

    }

    public void TakeDamage(int damage, Transform attacker)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} levou {damage} de dano! Vida restante: {currentHealth}");


        
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
}