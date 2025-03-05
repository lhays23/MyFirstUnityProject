using System.Collections;
using UnityEngine;

public class SkullEnemy : EnemyBase
{
    public float attackRange = 7f;
    public float targetedAttackRange = 3f; // ✅ Special attack triggers at 3f
    public float attackCooldown = 2f;
    public float targetedAttackCooldown = 10f; // ✅ Cooldown for targeted attack
    public float stopDuration = 0.5f;
    public GameObject skullProjectilePrefab;
    public GameObject targetedAttackPrefab; // ✅ New prefab for the targeted attack
    public Transform firePoint;

    private float nextAttackTime = 0f;
    private float nextTargetedAttackTime = 0f; // ✅ Tracks cooldown for targeted attack

    protected override bool ShouldUseAbility(float distanceToPlayer)
    {
        if (distanceToPlayer < targetedAttackRange && Time.time >= nextTargetedAttackTime)
        {
            return true; // ✅ Use the targeted attack if cooldown allows
        }
        else if (distanceToPlayer < attackRange && Time.time >= nextAttackTime)
        {
            return true; // ✅ Use regular attack if within attack range and off cooldown
        }
        return false;
    }

    protected override IEnumerator UseAbility()
    {
        isUsingAbility = true; // ✅ Prevent spamming

        yield return new WaitForSeconds(0.2f); // Small delay before attacking

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < targetedAttackRange && Time.time >= nextTargetedAttackTime)
        {
            PerformTargetedAttack();
            nextTargetedAttackTime = Time.time + targetedAttackCooldown; // ✅ Apply cooldown
        }
        else
        {
            ShootAtPlayer();
            nextAttackTime = Time.time + attackCooldown; // ✅ Regular attack cooldown
        }

        yield return new WaitForSeconds(stopDuration);
        isUsingAbility = false; // ✅ Resume movement
    }

    void ShootAtPlayer()
    {
        if (player == null) return;

        Vector2 spawnPos = firePoint.position;
        Vector2 targetPosition = player.position;

        GameObject projectile = Instantiate(skullProjectilePrefab, spawnPos, Quaternion.identity);

        if (projectile.TryGetComponent(out SkullProjectile skullProjectile))
        {
            skullProjectile.SetTarget(targetPosition);
        }
    }

    void PerformTargetedAttack()
    {
        if (player == null) return;

        Vector2 spawnPos = firePoint.position;

        // ✅ Spawns a targeted attack (could be a bigger projectile, an AoE effect, etc.)
        GameObject targetedAttack = Instantiate(targetedAttackPrefab, spawnPos, Quaternion.identity);

        if (targetedAttack.TryGetComponent(out SkullProjectile skullProjectile))
        {
            skullProjectile.SetTarget(player.position); // Targeted attack homes in on player position
        }
    }
}
