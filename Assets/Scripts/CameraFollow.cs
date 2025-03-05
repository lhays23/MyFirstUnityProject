using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float smoothSpeed = 0.125f;
    private Vector3 velocity = Vector3.zero; // Adjusts smoothness of camera movement
    public Vector3 offset; // Offset distance between player and camera

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
        }
    }
}
