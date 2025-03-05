using UnityEngine;

public class SkullProjectile : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;
    private Vector2 targetPosition;

    public void SetTarget(Vector2 position)
    {
        targetPosition = position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if ((Vector2)transform.position == targetPosition)
        {
            Destroy(gameObject); // Destroy projectile when it reaches the target
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by skull projectile!");
            Destroy(gameObject); // Destroy projectile on impact
        }
    }
}
