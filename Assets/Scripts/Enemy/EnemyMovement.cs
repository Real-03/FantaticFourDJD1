using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float visionRange = 6f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip WalkSound;

    private Rigidbody2D rb;
    private EnemyCombat combatScript;
    private bool isGrounded;
    private Transform targetPlayer;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        combatScript = GetComponent<EnemyCombat>();
    }

    void Update()
    {
        ComputeGroundState();
        targetPlayer = FindClosestPlayer();

        if (targetPlayer == null || !isGrounded || combatScript == null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        // Distância entre o jogador e o ponto de ataque
        float distanceToPlayer = Vector2.Distance(combatScript.detectPoint.position, targetPlayer.position);

        if (distanceToPlayer <= combatScript.detectionRange)
        {
            // Jogador já está no range de ataque -> parar
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else if (Vector2.Distance(transform.position, targetPlayer.position) <= visionRange)
        {
            // Jogador dentro da visão -> perseguir
            StartCoroutine(WalkSoundPlay());
            Vector2 direction = (targetPlayer.position - transform.position).normalized;
            rb.linearVelocity = new Vector2(direction.x * moveSpeed, rb.linearVelocity.y);

            // Flip visual
            if (direction.x > 0)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (direction.x < 0)
                transform.rotation = Quaternion.identity;
        }
        else
        {
            // Fora do alcance → parar
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    Transform FindClosestPlayer()
    {
        Collider2D[] playersInRange = Physics2D.OverlapCircleAll(transform.position, visionRange, playerLayer);

        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D col in playersInRange)
        {
            float dist = Vector2.Distance(transform.position, col.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = col.transform;
            }
        }

        return closest;
    }

    void ComputeGroundState()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = collider != null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    IEnumerator WalkSoundPlay()
    {
        yield return new WaitForSeconds(1);
        
    }
}
