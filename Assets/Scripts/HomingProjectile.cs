using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 5;
    public float rotateSpeed = 200f;
    private Transform target;

    public void SetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Compute direction only once
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0, 0, angle);

        // Use SetPositionAndRotation() for better performance
        transform.SetPositionAndRotation(
            transform.position + (Vector3)direction * speed * Time.deltaTime,
            Quaternion.RotateTowards(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime)
        );

        // OPTIMIZED: Use squared magnitude instead of Vector2.Distance()
        Vector2 difference = (Vector2)(target.position - transform.position);
        float distanceSquared = difference.x * difference.x + difference.y * difference.y;

        if (distanceSquared < 0.1f * 0.1f) // 0.1f squared for faster check
        {
            Destroy(gameObject); // Destroy when close to the target
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out EnemyBase enemy))
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
