using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Camera cam;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (cam == null) return;

        
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, cam.transform.position.x - halfWidth, cam.transform.position.x + halfWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, cam.transform.position.y - halfHeight, cam.transform.position.y + halfHeight);

        transform.position = clampedPosition;
    }
}
