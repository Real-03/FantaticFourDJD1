using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomSpeed = 5f;
    public float followSpeed = 5f;
    public float maxDistance = 10f;
    
    private Camera cam;
    private Vector3 lastValidPosition;

    void Start()
    {
        cam = GetComponent<Camera>();
        lastValidPosition = transform.position;
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        Vector3 midPoint = (player1.position + player2.position) / 2f;
        float distance = Vector3.Distance(player1.position, player2.position);
        
        float targetZoom = Mathf.Lerp(minZoom, maxZoom, distance / maxDistance);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);

        if (distance >= maxDistance * 0.95f && ArePlayersMovingApart())
        {
            return;
        }
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(midPoint.x, midPoint.y, transform.position.z), Time.deltaTime * followSpeed);
        lastValidPosition = transform.position;
    }

    bool ArePlayersMovingApart()
    {
        Vector2 dir1 = player1.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        Vector2 dir2 = player2.GetComponent<Rigidbody2D>().linearVelocity.normalized;
        return Vector2.Dot(dir1, dir2) < -0.5f;
    }
}
