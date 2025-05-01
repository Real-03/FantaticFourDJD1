using UnityEngine;

public class ShootFireBall : MonoBehaviour
{
    public GameObject FireBallRight; // Prefab da fireball para a direita
    public GameObject FiraBallLeft;  // Prefab da fireball para a esquerda
    public Transform firePoint;            // Ponto de spawn da fireball
    public float fireballSpeed = 10f;      // Velocidade da fireball
    public KeyCode shootKey = KeyCode.J;   // Tecla para atirar

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
                Shoot();  
        }
    }

    void Shoot()
    {
        float direction = transform.localScale.x > 0 ? 1 : -1;
        

        GameObject fireballPrefab = direction > 0 ? FireBallRight : FiraBallLeft;


        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();


        rb.linearVelocity = new Vector2(fireballSpeed * direction, 0);
    }
}
