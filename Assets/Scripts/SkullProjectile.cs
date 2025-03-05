using UnityEngine;

public class SkullProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    private Vector2 moveDirection;
    private readonly float lifetime = 3f; // ✅ Projectile disappears after 3 seconds

    public void SetTarget(Vector2 targetPosition)
    {
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;
        Destroy(gameObject, lifetime); // ✅ Schedule auto-destruction
    }

    void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player hit by skull projectile!");
            Destroy(gameObject); // ✅ Destroy projectile on impact
        }
    }
}
