using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerFly : MonoBehaviour
{
    public float flySpeed = 5f;               // Velocidade de voo
    public float flightTime = 5f;             // Tempo máximo de voo
    public float cooldownFlightTime = 5f;  
    private bool isFlying = false;            
    private Rigidbody2D rb;
    private PlayerMovement playerMovementScript;
    [SerializeField] private Animator animator;
    [SerializeField] private Image flightCooldownUI;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        playerMovementScript =  GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period) && !isFlying)
        {

            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        Debug.Log("Start");
        animator.SetBool("Fly", !isFlying);
        playerMovementScript.enabled = false;
        isFlying = !isFlying;
        rb.gravityScale = 0;

        float Timer = flightTime;
        
        while (Timer > 0)
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
            Timer -= Time.deltaTime;
            UpdateCooldownUI(Timer);
            yield return null;
        }
        animator.SetBool("Fly", !isFlying);
        rb.gravityScale =1;
        playerMovementScript.enabled = true;
        Timer = 0;
        while (Timer < cooldownFlightTime)
        {
            Timer += Time.deltaTime;
            UpdateCooldownUI(Timer);
            yield return null;
        }
        isFlying = false; 
        UpdateCooldownUI(cooldownFlightTime);
    }

    private void UpdateCooldownUI(float timeRemaining)
    {
        if (flightCooldownUI != null)
        {
            flightCooldownUI.fillAmount = Mathf.Clamp01(timeRemaining / cooldownFlightTime);
        }
    }
    
}
