using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
public class TreeDestruction : MonoBehaviour
{

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioClip TreeRockSound;
    [SerializeField] private AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);

        Dash dashScript = collision.gameObject.GetComponent<Dash>();
        if (dashScript != null && dashScript.getIsDashing())
        {
            particle.Play();
            AudioSource.PlayOneShot(TreeRockSound);
            Destroy(gameObject); // Destroi este objeto (a árvore com SpriteRenderer)
        }
    }
}
