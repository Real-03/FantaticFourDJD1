using UnityEngine;

public class ShootFireBall : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject FireBallRight; // Prefab da fireball para a direita
    public GameObject FiraBallLeft;  // Prefab da fireball para a esquerda
    public Transform firePoint;            // Ponto de spawn da fireball
    public float fireballSpeed = 10f;      // Velocidade da fireball
    public KeyCode shootKey = KeyCode.Q;   // Tecla para atirar

    void Update()
    {
=======
    [SerializeField] private GameObject FireBallRight; // Prefab da fireball para a direita
    [SerializeField] private GameObject FiraBallLeft;  // Prefab da fireball para a esquerda
    [SerializeField] private Transform firePoint;            // Ponto de spawn da fireball
    [SerializeField] private float fireballSpeed = 10f;      // Velocidade da fireball
    [SerializeField] private KeyCode shootKey = KeyCode.J;   // Tecla para atirar
    private PlayerMovement movementScript;

    void Update()
    {
        movementScript = GetComponent<PlayerMovement>();
>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
        if (Input.GetKeyDown(shootKey))
        {
                Shoot();  
        }
    }

    void Shoot()
    {
<<<<<<< HEAD
        float direction = transform.localScale.x > 0 ? 1 : -1;
        
        // Escolher o prefab correto baseado na direção
        GameObject fireballPrefab = direction > 0 ? FireBallRight : FiraBallLeft;

        // Criar a bola de fogo
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        // Aplicar velocidade
=======
        float direction = transform.right.x > 0 ? 1 : -1;

        GameObject fireballPrefab = direction > 0 ? FireBallRight : FiraBallLeft;


        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();


>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
        rb.linearVelocity = new Vector2(fireballSpeed * direction, 0);
    }
}
