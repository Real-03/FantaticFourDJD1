using UnityEngine;

public class PlayerMovementConfigurableKeys : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento
    public float jumpForce = 7f; // Força do pulo
    public Transform groundCheck; // Ponto de verificação do chão
    public LayerMask groundLayer; // Camada do chão

    public KeyCode moveRightKey = KeyCode.D; // Tecla para mover para a direita
    public KeyCode moveLeftKey = KeyCode.A; // Tecla para mover para a esquerda
    public KeyCode jumpKey = KeyCode.W; // Tecla para pular

    private Rigidbody2D rb;
    private bool isGrounded;

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
    }

    // Detecta quando o personagem sai do chão
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}