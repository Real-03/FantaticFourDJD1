using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth;
    [SerializeField] private Image healthBar;

    private Animator animator;
    private PlayerMovement movementScript;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Respawn respawnManager;
    

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

    public void TakeDamage(float damage, Transform attacker)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} levou {damage} de dano! Vida restante: {currentHealth}");
        
        SpriteFlash.Flash(spriteRenderer, 0.2f, 1);
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
        StartCoroutine(respawnManager.RespawnPlayer(this.gameObject));
    }
    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SetHealthUI(currentHealth);

    }

    void SetHealthUI(float Health)
    {
        float targerFillAmount = (float)Health/maxHealth;
        healthBar.fillAmount = targerFillAmount > 0 ? targerFillAmount : 0;
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        SetHealthUI(currentHealth);
    }

    private void OnAnimationEnd()
    {
        Debug.Log("Ended");
    }
}