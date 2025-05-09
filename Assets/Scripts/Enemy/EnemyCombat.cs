using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    public LayerMask playerLayer;

    // Área de detecção
    public Transform detectPoint;
    public float detectionRange = 2f;

    // Ataques
    public Transform punchPoint;
    public float punchRange = 0.6f;
    public int punchDamage = 10;

    public Transform kickPoint;
    public float kickRange = 0.9f;
    public int kickDamage = 15;

    private Animator animator;
    private bool isAttacking = false;
    private EnemyMovement enemyMovementScript;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovementScript = GetComponent<EnemyMovement>();
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(1f, 2.5f);
            yield return new WaitForSeconds(waitTime);

            if (!isAttacking && PlayerInDetectionArea())
            {
                int attackType = Random.Range(0, 2); // 0 = punch, 1 = kick
                string attackName = attackType == 0 ? "Punch" : "Kick";
                StartCoroutine(PerformAttack(attackName));
            }
        }
    }

    IEnumerator PerformAttack(string type)
    {
        isAttacking = true;
        enemyMovementScript.enabled = false;
        animator.SetTrigger(type);

        yield return new WaitForSeconds(0.3f); // tempo até conectar o golpe

        Transform attackPoint = type == "Punch" ? punchPoint : kickPoint;
        float range = type == "Punch" ? punchRange : kickRange;
        int damage = type == "Punch" ? punchDamage : kickDamage;

        Collider2D playerHit = Physics2D.OverlapCircle(attackPoint.position, range, playerLayer);
        if (playerHit != null)
        {
            PlayerHealth player = playerHit.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage , transform);
                Debug.Log($"{type} atingiu o jogador! Dano: {damage}");
            }
        }

        yield return new WaitForSeconds(0.5f); // tempo restante da animação
        enemyMovementScript.enabled = true;
        isAttacking = false;
    }

    bool PlayerInDetectionArea()
    {
        Collider2D player = Physics2D.OverlapCircle(detectPoint.position, detectionRange, playerLayer);
        return player != null;
    }

    void OnDrawGizmosSelected()
    {
        // Área de detecção
        if (detectPoint != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(detectPoint.position, detectionRange);
        }

        // Punch
        if (punchPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(punchPoint.position, punchRange);
        }

        // Kick
        if (kickPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(kickPoint.position, kickRange);
        }
    }
}
