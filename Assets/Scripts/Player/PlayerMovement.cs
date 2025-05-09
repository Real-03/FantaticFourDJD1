<<<<<<< HEAD
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
=======
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 velocity;
    [SerializeField] private string horizontalAxisName;
    [SerializeField] private string jumpAxisName;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 2.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpMaxDuration = 0.1f;
    [SerializeField] private float jumpGravityScale = 1f;
    [SerializeField] private float jumpForce;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private float jumpTimer;
    private float originalGravity;
    private float moveDir;
    private PlayerAttack playerAttack; // Referência ao script do player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAttack = GetComponent<PlayerAttack>(); //Pega o script no mesmo objeto
        horizontalAxisName = gameObject.name.ToLower() == "thing" ? "Horizontal_P1" : "Horizontal_P2"; // Troca os controlos para cada jogador no eixo Horizontal
        jumpAxisName = gameObject.name.ToLower() == "thing" ? "Jump_P1" : "Jump_P2"; // Troca os controlos para cada jogador no eixo Horizontal
>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
    }

    void Update()
    {
<<<<<<< HEAD
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
=======
        ComputeGroundState();

        moveDir = Input.GetAxis(horizontalAxisName);
        Vector2 currentVelocity = rb.linearVelocity;

        //Impede movimento e rotação durante ataque
        if (playerAttack != null && playerAttack.isAttacking)
        {
            moveDir = 0f; //zera movimento
        }
        else
        {
            // Permite virar se não estiver a atacar
            if ((moveDir < 0) && (transform.right.x > 0))
            {
                transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            }
            else if ((moveDir > 0) && (transform.right.x < 0))
            {
                transform.rotation = Quaternion.identity;
            }
            currentVelocity.x = moveDir * velocity.x;
        }

        

        //Lógica de salto
        if (Input.GetButtonDown(jumpAxisName))
        {
            
            if (isGrounded)
            {
                currentVelocity.y = velocity.y * jumpForce;
                jumpTimer = 0.0f;
                rb.gravityScale = jumpGravityScale;
            }
        }
        else if (jumpTimer < jumpMaxDuration)
        {
            jumpTimer += Time.deltaTime;
            if (Input.GetButton(jumpAxisName))
            {
                rb.gravityScale = Mathf.Lerp(jumpGravityScale, originalGravity, jumpTimer / jumpMaxDuration);
            }
            else
            {
                jumpTimer = jumpMaxDuration;
                rb.gravityScale = originalGravity;
            }
        }
        else
        {
            rb.gravityScale = originalGravity;
        }

        rb.linearVelocity = currentVelocity;
    }

    void ComputeGroundState()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = collider != null;
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public float GetMoveDir()
    {
        return moveDir;
    } 
}
>>>>>>> d57e1163c2f9efb881409f72dd2d4dbb858892b1
