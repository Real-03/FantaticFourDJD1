using UnityEngine;

public class FragileBridge2D : MonoBehaviour
{
    [Header("Deteção de Jogadores")]
    public Transform detectPoint;
    public Vector2 detectSize = new Vector2(3f, 0.5f);
    public LayerMask playerLayer;

    [Header("Efeitos")]
    public GameObject warningParticlesPrefab;
    public GameObject bridgeBreakEffectPrefab;
    public Animator animator;
    public AudioSource AudioSource;
    public AudioClip bridgeCreak;
    public AudioClip bridgeBreak;

    [Header("Configuração")]
    public float breakDelay = 2f; // Tempo que os 2 jogadores precisam estar na ponte

    private bool bridgeBroken = false;
    private bool warningShown = false;
    private float timer = 0f;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (bridgeBroken) return;

        Collider2D[] players = Physics2D.OverlapBoxAll(detectPoint.position, detectSize, 0f, playerLayer);
        int playerCount = players.Length;

        if (playerCount >= 2)
        {
            timer += Time.deltaTime;

            if (timer >= breakDelay)
            {
                BreakBridge();
            }
        }
        else
        {
            timer = 0f; // Reinicia o tempo se sair algum jogador

            if (playerCount == 1 && !warningShown)
            {
                ShowWarning();
            }
        }
    }

    private void ShowWarning()
    {
        //AudioSource.PlayOneShot(bridgeCreak);
        warningShown = true;
        if (warningParticlesPrefab != null)
        {
            Instantiate(warningParticlesPrefab, detectPoint.position, Quaternion.identity);
        }
    }

    private void BreakBridge()
    {
        bridgeBroken = true;
        //AudioSource.PlayOneShot(bridgeBreak);
        if (animator != null)
        {
            animator.SetTrigger("Break");
            animator.SetBool("Broken", true);
        }

        if (bridgeBreakEffectPrefab != null)
        {
            Instantiate(bridgeBreakEffectPrefab, transform.position, Quaternion.identity);
        }

        // Desativa todos os colliders da ponte
        foreach (EdgeCollider2D col in GetComponents<EdgeCollider2D>())
        {
            col.enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (detectPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(detectPoint.position, detectSize);
    }
}
