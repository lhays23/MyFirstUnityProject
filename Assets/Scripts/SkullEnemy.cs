using UnityEngine;
using System.Collections;

public class SkullEnemy : EnemyBase
{
    public float attackRange = 7f;
    public float attackCooldown = 2f;
    public float stopDuration = 0.5f;
    public GameObject skullProjectilePrefab;
    public Transform firePoint;

    private float nextAttackTime = 0f;

    protected override bool ShouldUseAbility(float distanceToPlayer)
    {
        return distanceToPlayer < attackRange && Time.time >= nextAttackTime;
    }

    protected override IEnumerator UseAbility()
    {
        isUsingAbility = true;
        yield return new WaitForSeconds(0.2f); // Short delay before shooting
    {
        isUsingAbility = true;

        ShootAtPlayer();
        nextAttackTime = Time.time + attackCooldown;

        yield return new WaitForSeconds(stopDuration); // Pause movement
        isUsingAbility = false;
    }

    void ShootAtPlayer()
    {
        if (player == null) return;

        if (skullProjectilePrefab == null)
        {
            Debug.LogError("SkullProjectilePrefab is missing! Assign it in the Inspector.");
            return;
        }

        Vector2 playerPosition = player.position;
        GameObject projectile = Instantiate(skullProjectilePrefab, firePoint.position, Quaternion.identity);

        if (projectile == null)
        {
            Debug.LogError("Failed to instantiate SkullProjectile!");
            return;
        }

        if (projectile.TryGetComponent(out SkullProjectile skullProjectile))
        {
            skullProjectile.SetTarget(playerPosition);
        }

        if (skullProjectile != null)
        {
            skullProjectile.SetTarget(playerPosition);
        }
        else
        {
            Debug.LogError("Projectile does not have a SkullProjectile script!");
        }
    }

}
