using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dash : MonoBehaviour
{
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private KeyCode dashKey = KeyCode.E;
    [SerializeField] private AudioSource dashSound;

    [Header("References")]
    [SerializeField] private Image dashCooldownUI;
    [SerializeField] private Rigidbody2D rb;
     
    [SerializeField] private GameObject dashParticle;
    [SerializeField] private Transform dashParticleSpawn;
    private bool canDash = true;
    private bool isDashing = false;
    private float originalGravity;

    private SpawnParticle particleManager;
    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        originalGravity = rb.gravityScale;
        particleManager = FindFirstObjectByType<SpawnParticle>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        particleManager.SpawnParticlesY(dashParticle,dashParticleSpawn,transform.right.normalized.x);
        canDash = false;
        isDashing = true;

        float startTime = Time.time;
        Vector2 dashDirection = transform.right.normalized;
        dashSound.Play();

        // Desativa gravidade para manter o dash reto
        rb.gravityScale = 0;
        rb.linearVelocity = dashDirection * dashSpeed;

        // Mantém velocidade constante durante a duração do dash
        while (Time.time < startTime + dashDuration)
        {
            rb.linearVelocity = dashDirection * dashSpeed;
            yield return null;
        }

        rb.gravityScale = originalGravity;
        rb.linearVelocity = Vector2.zero;
        isDashing = false;

        // Cooldown
        float cooldownTimer = dashCooldown;
        while (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            UpdateCooldownUI(cooldownTimer);
            yield return null;
        }

        canDash = true;
        UpdateCooldownUI(0f);
    }

    private void UpdateCooldownUI(float timeRemaining)
    {
        if (dashCooldownUI != null)
        {
            dashCooldownUI.fillAmount = Mathf.Clamp01(timeRemaining / dashCooldown);
        }
    }

    public bool getIsDashing()
    {
        return isDashing;
    }

}