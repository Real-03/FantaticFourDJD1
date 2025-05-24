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
    }

    void Update()
    {
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

    public void ComputeGroundState()
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

    public bool GetGrouncCheck()
    {
        return isGrounded;
    } 
}
