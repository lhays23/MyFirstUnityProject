using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackCooldown = 1.5f;
    private float nextAttackTime = 0f;
    private Transform targetEnemy; // Stores the currently selected enemy

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click to target an enemy
        {
            SelectEnemy();
        }

        if (targetEnemy != null && Time.time >= nextAttackTime)
        {
            ShootAtEnemy();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void SelectEnemy()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent(out EnemyBase enemy)) // Optimized check
        {
            targetEnemy = enemy.transform;
            //Debug.Log("Targeting enemy: " + targetEnemy.name);
        }
    }

    void ShootAtEnemy()
    {
        if (targetEnemy == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        if (projectile.TryGetComponent(out HomingProjectile homingScript)) // Optimized check
        {
            homingScript.SetTarget(targetEnemy);
        }
        else
        {
            //Debug.LogError("Projectile does not have a HomingProjectile script!");
        }
    }
}
