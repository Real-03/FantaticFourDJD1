using UnityEngine;

public class ShootFireBall : MonoBehaviour
{
    [SerializeField] private GameObject FireBallRight; // Prefab da fireball para a direita
    [SerializeField] private GameObject FiraBallLeft;  // Prefab da fireball para a esquerda
    [SerializeField] private Transform firePoint;            // Ponto de spawn da fireball
    [SerializeField] private float fireballSpeed = 10f;      // Velocidade da fireball
    [SerializeField] private KeyCode shootKey = KeyCode.J;   // Tecla para atirar
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
        float direction = transform.right.x > 0 ? 1 : -1;

        GameObject fireballPrefab = direction > 0 ? FireBallRight : FiraBallLeft;


        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();


        rb.linearVelocity = new Vector2(fireballSpeed * direction, 0);
    }
}
