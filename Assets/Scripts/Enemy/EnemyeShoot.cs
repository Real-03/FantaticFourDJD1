using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float fireRate = 1.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform player1;
    public Transform player2;
    public float detectionRange = 10f;
    public float bulletSpeed = 10f;

    private float nextFireTime;

    void Update()
    {
        Transform closestPlayer = GetClosestPlayer();
        
        if (closestPlayer != null && Vector2.Distance(transform.position, closestPlayer.position) <= detectionRange)
        {
            if (Time.time >= nextFireTime) 
            {
                Shoot(closestPlayer);
                nextFireTime = Time.time + fireRate; // Controla o tempo do próximo tiro
            }
        }
    }

    Transform GetClosestPlayer()
    {
        if (player1 == null && player2 == null) return null;
        if (player1 == null) return player2;
        if (player2 == null) return player1;

        float dist1 = Vector2.Distance(transform.position, player1.position);
        float dist2 = Vector2.Distance(transform.position, player2.position);

        return dist1 < dist2 ? player1 : player2;
    }

    void Shoot(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        // Direção normalizada para o alvo
        Vector2 direction = (target.position - firePoint.position).normalized;
        bulletRb.linearVelocity = direction * bulletSpeed;

        // Ajusta a rotação da bala
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    
    
    
}
