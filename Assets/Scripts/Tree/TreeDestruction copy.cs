using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
public class TreeDestruction : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);

        Dash dashScript = collision.gameObject.GetComponent<Dash>();
        if (dashScript != null && dashScript.getIsDashing())
        {
            Destroy(gameObject); // Destroi este objeto (a árvore com SpriteRenderer)
        }
    }
}
