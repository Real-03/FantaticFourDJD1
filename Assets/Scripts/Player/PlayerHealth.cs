using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private Image healthBar;

    private Animator animator;
    private PlayerMovement movementScript;

    

    void Start()
    {
        currentHealth = maxHealth;
        movementScript = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();

        if (healthBar != null)
        {
            SetHealthUI(currentHealth);
        }
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
            
        }


    }

    void Die()
    {
        Debug.Log($"{gameObject.name} morreu!");

        animator.SetTrigger("Die");
        movementScript.enabled = false;
        gameObject.SetActive(false);
        this.enabled = false;
    }

    void SetHealthUI(int Health)
    {
        float targerFillAmount = (float)Health/maxHealth;
        healthBar.fillAmount = targerFillAmount > 0 ? targerFillAmount : 0;
    }

    private void OnAnimationEnd()
    {
        Debug.Log("Ended");
    }
}