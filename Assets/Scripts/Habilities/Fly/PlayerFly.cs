using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerFly : MonoBehaviour
{
    [SerializeField] private float flySpeed = 5f;               // Velocidade de voo
    [SerializeField] private float flightTime = 5f;             // Tempo máximo de voo
    [SerializeField] private float cooldownFlightTime = 5f;  
    private bool isFlying = false;            
    private Rigidbody2D rb;
    private PlayerMovement playerMovementScript;
    [SerializeField] private Animator animator;
    [SerializeField] private Image flightCooldownUI;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioClip flightSound;
    [SerializeField] private AudioSource AudioSource;
    private string jumpAxisName =  "Jump_P2";
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>(); 
        playerMovementScript =  GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetButtonDown(jumpAxisName) && !isFlying && playerMovementScript.GetGrouncCheck() == false)
        {

            StartCoroutine(PerformFly());
        }
    }

    private IEnumerator PerformFly()
    {
        Debug.Log("Start");
        animator.SetBool("Fly", !isFlying);
        playerMovementScript.enabled = false;
        isFlying = !isFlying;
        rb.gravityScale = 0;

        float Timer = flightTime;
        particle.Play();
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
            AudioSource.PlayOneShot(flightSound);
            AudioSource.loop = true;
            yield return null;
        }
        particle.Stop();
        AudioSource.Stop();
        AudioSource.loop = false;
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
