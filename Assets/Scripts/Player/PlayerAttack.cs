using UnityEngine;
using System.Collections;
public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public float comboResetTime = 1.5f;
    public float attackCooldown = 0.5f; // Tempo entre ataques para evitar spam

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private int punchCount = 0;
    private float lastPunchTime = 0f;
    public bool isAttacking = false;

    [SerializeField] private string punchlAxisName;
    [SerializeField] private string kickAxisName;

    void Start()
    {
        punchlAxisName = gameObject.name.ToLower() == "thing" ? "Fire1_P1" : "Fire1_P2";
        kickAxisName = gameObject.name.ToLower() == "thing" ? "Fire2_P1" : "Fire2_P2";
    }
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (isAttacking) return;

        if (Input.GetButtonDown(punchlAxisName)) // Punch
        {
            Punch();
        }
        else if (Input.GetButtonDown(kickAxisName)) // Kick
        {
            Kick();
        }
    }

    void Punch()
    {
        float currentTime = Time.time;

        if (currentTime - lastPunchTime > comboResetTime)
        {
            punchCount = 0;
        }

        punchCount++;
        lastPunchTime = currentTime;
        isAttacking = true;

        if (punchCount >= 3)
        {
            Uppercut();
            punchCount = 0;
        }
        else
        {
            animator.SetTrigger("Punch");
            DetectEnemiesHit("Punch");
        }

        animator.SetInteger("Combo", punchCount);
        StartCoroutine(ResetAttackFlag());
    }

    void Kick()
    {
        isAttacking = true;
        animator.SetTrigger("Kick");
        DetectEnemiesHit("Kick");
        StartCoroutine(ResetAttackFlag());
    }

    void Uppercut()
    {
        isAttacking = true;
        animator.SetTrigger("Uppercut");
        DetectEnemiesHit("Uppercut");
        StartCoroutine(ResetAttackFlag());
    }

    private IEnumerator ResetAttackFlag()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    void DetectEnemiesHit(string attackType)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyScript = enemy.GetComponent<EnemyHealth>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(25, transform);
                Debug.Log($"{attackType} hit: " + enemy.name);
            }
        }

        if (hitEnemies.Length == 0)
        {
            Debug.Log($"{attackType} missed.");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}