using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar; // Assign in Inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        IEnumerator InvulnerabilityCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        isInvulnerable = false;
    }
}

    private bool isInvulnerable = false;

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0); // Prevent negative health
        UpdateHealthBar();

        Debug.Log("Player took " + damage + " damage! Current health: " + currentHealth);

        StartCoroutine(InvulnerabilityCooldown());

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            Debug.Log("Updating health bar: " + ((float)currentHealth / maxHealth)); // Debug message
            healthBar.value = (float)currentHealth / maxHealth;
        }
        else
        {
            Debug.LogError("Health bar is not assigned in PlayerHealth script!");
        }
    }


    void Die()
    {
        Debug.Log("Player has died!");
        // Add respawn or game-over logic here
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.name); // Debug log

        if (other.CompareTag("EnemyProjectile")) // Ensure the projectile has the correct tag
        {
            Debug.Log("Hit by enemy projectile!");
            TakeDamage(10);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy")) // If the enemy itself touches the player
        {
            Debug.Log("Touched by an enemy!");
            TakeDamage(5);
        }
    }



}
