using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    public float flySpeed = 5f;               // Velocidade de voo
    public float flightTime = 5f;             // Tempo máximo de voo
    private float currentFlightTime;          // Tempo restante de voo
    private bool isFlying = false;            
    private Rigidbody2D rb;
    private PlayerMovement playerMovementScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentFlightTime = flightTime;  // Inicializa o tempo de voo
        playerMovementScript =  GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (rb == null) return;


        if (Input.GetKeyDown(KeyCode.Period) && currentFlightTime > 0)
        {
            playerMovementScript.enabled = false;
            isFlying = !isFlying;


            rb.gravityScale = isFlying ? 0 : 1;


            if (isFlying)
            {
                currentFlightTime = flightTime;
            }
        }

        
        if (isFlying && currentFlightTime > 0)
        {
            
            float vertical = Input.GetAxis("Jump_P2");
            float horizontal = Input.GetAxis("Horizontal_P2"); 
            if ((horizontal < 0) && (transform.right.x > 0))
            {
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            else if ((horizontal > 0) && (transform.right.x < 0))
            {
                transform.rotation = Quaternion.identity;
            }
            rb.linearVelocity = new Vector2(horizontal*flySpeed, vertical * flySpeed);

            
            currentFlightTime -= Time.deltaTime;
        }
        else if (!isFlying && currentFlightTime < flightTime)
        {
            
            currentFlightTime += Time.deltaTime;
        }

        
        if (currentFlightTime <= 0)
        {
            isFlying = false;
            rb.gravityScale = 1;  
            playerMovementScript.enabled = true;
        }
    }
}
