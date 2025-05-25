using UnityEngine;

public class ShootFireBall : MonoBehaviour
{
    [SerializeField] private GameObject FireBall; // Prefab da fireball para a direita
    [SerializeField] private Transform firePoint;            // Ponto de spawn da fireball
    [SerializeField] private float fireballSpeed = 10f;      // Velocidade da fireball
    [SerializeField] private KeyCode shootKey = KeyCode.J;   // Tecla para atirar
    [SerializeField] private AudioSource shootSound;
    private PlayerMovement movementScript;

    void Update()
    {
        movementScript = GetComponent<PlayerMovement>();
        if (Input.GetKeyDown(shootKey))
        {
                Shoot();  
        }
    }

    void Shoot()
    {
        shootSound.Play();
        float direction = transform.right.x > 0 ? 1 : -1;

        Quaternion rotation = direction == 1 ? Quaternion.Euler(0.0f, 0.0f, 0.0f): Quaternion.Euler(0.0f, 180.0f, 0.0f); 

        GameObject fireball = Instantiate(FireBall, firePoint.position, rotation);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();


        rb.linearVelocity = new Vector2(fireballSpeed * direction, 0);
    }
}
