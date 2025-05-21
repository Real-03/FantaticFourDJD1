using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 15f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float maxDistance = 10f;
    
    private Camera cam;
    private Vector3 lastValidPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
        lastValidPosition = transform.position;
    }

    void LateUpdate()
    {
        if (player1 == null && player2 == null) return;

        bool p1Alive = player1 != null && player1.GetComponent<SpriteRenderer>().enabled;
        bool p2Alive = player2 != null && player2.GetComponent<SpriteRenderer>().enabled;

        // Nenhum jogador ativo: não faz nada
        if (!p1Alive && !p2Alive) return;

        Vector3 midPoint;
        float distance;

        if (p1Alive && p2Alive)
        {
            midPoint = (player1.position + player2.position) / 2f;
            distance = Vector3.Distance(player1.position, player2.position);
        }
        else if (p1Alive)
        {
            midPoint = player1.position;
            distance = 0;
        }
        else // p2Alive
        {
            midPoint = player2.position;
            distance = 0;
        }

        float targetZoom = Mathf.Lerp(minZoom, maxZoom, distance / maxDistance);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(midPoint.x, midPoint.y, transform.position.z),
            Time.deltaTime * followSpeed
        );
    }

    bool ArePlayersMovingApart()
    {
        Vector2 dir1 = player1.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Vector2 dir2 = player2.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        return Vector2.Dot(dir1, dir2) < -0.5f;
    }
}
