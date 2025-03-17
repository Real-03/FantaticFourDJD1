using System.Collections;
using UnityEngine;

public class PlayerMovementConfigurableKeys : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 5f; // Velocidade de movimento
    public float jumpForce = 7f; // Força do pulo
    public KeyCode moveRightKey = KeyCode.D; // Tecla para mover para a direita
    public KeyCode moveLeftKey = KeyCode.A; // Tecla para mover para a esquerda
    public KeyCode jumpKey = KeyCode.W; // Tecla para pular
    public Transform groundCheck; // Ponto de verificação do chão
    public LayerMask groundLayer; // Camada do chão
    
    private Rigidbody2D rb;
    private bool isGrounded;

    [Header("Dash Settings")]
    public float dashingPower = 100f;
    public float dashingTime = 1f;
    public float dashingCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    public KeyCode dashKey = KeyCode.E;

    [Header("KnockBack Setting")]

    public float knockBackForce = 500f;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Melhor detecção de chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        float move = 0;

        if (Input.GetKey(moveRightKey)) move = 1;
        else if (Input.GetKey(moveLeftKey)) move = -1;

        // Corrigir linearVelocity -> Usar velocity corretamente
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // Flip do personagem
        if (move > 0 && transform.localScale.x < 0) Flip();
        else if (move < 0 && transform.localScale.x > 0) Flip();

        // Pulo
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        //Dash
        if (Input.GetKeyDown(dashKey))
        {
            Debug.Log("Dash Hability Start " + move);
            if(canDash == true && isDashing == false)
                StartCoroutine(DashAbility());
            else
                Debug.Log("You cant dash"); 
            
        }
    }

    // Método para fazer o flip
    private void Flip()
    {
        // Inverte a escala no eixo X
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Detecta quando o personagem colide com o chão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            KnockBack(knockBackForce);
        }
    }

    // Detecta quando o personagem sai do chão
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
//KnockBack
    private void KnockBack(float knockBackForce)
    {
        Vector2 knockBackForceVector =  new Vector2(knockBackForce,0f);
        if(transform.localScale.x > 0)
            rb.AddForce(-knockBackForceVector, ForceMode2D.Force);
        else
            rb.AddForce(knockBackForceVector, ForceMode2D.Force);
    }

//Dash ability 
    private IEnumerator DashAbility()
    {
        Debug.Log("nada");
        canDash = false;
        isDashing = true;
        Vector2 dashingForceVector =  new Vector2(dashingPower,0f);
        if(transform.localScale.x > 0)
            rb.AddForce(dashingForceVector, ForceMode2D.Force);
        else
            rb.AddForce(-dashingForceVector, ForceMode2D.Force);


        
        Debug.Log("Velocity" +rb.linearVelocity.x);
        Debug.Log(dashingPower);
        
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        Debug.Log("dash end");
    }
}