using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class TreeFall : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Reference to the Tilemap
    [SerializeField] private Transform pivotPoint; // Point around which the tree will rotate
    [SerializeField] private float rotationSpeed = 90f; // Degrees per second

    private bool isFalling = false;

    [SerializeField] private Transform colliderPoint;
    [SerializeField] private float colliderRange = 0.5f;

    [SerializeField] private LayerMask groundLayer;

    void Update()
    {
        if (isFalling)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.RotateAround(pivotPoint.position, Vector3.back, step);
            DetectGround();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisão detectada com: " + collision.gameObject.name);

        Dash dashScript = collision.gameObject.GetComponent<Dash>();
        if (dashScript != null && dashScript.getIsDashing())
        {
            if (!isFalling)
            {
                isFalling = true;
            }
        }
    }
    void DetectGround()
    {
        Collider2D[] groundHit = Physics2D.OverlapCircleAll(colliderPoint.position, colliderRange, groundLayer);
        foreach (Collider2D enemy in groundHit)
        {
            Debug.Log(enemy);
            isFalling = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (colliderPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(colliderPoint.position, colliderRange);
    }
}
