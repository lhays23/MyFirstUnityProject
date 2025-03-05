using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // ✅ Required for TextMeshPro

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;
    //private bool isInvulnerable = false; //invuln stuff
    private SpriteRenderer spriteRenderer; // ✅ Reference to sprite renderer

    public GameObject damageTextPrefab; // ✅ Drag your "DamageText" prefab here

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        spriteRenderer = GetComponent<SpriteRenderer>(); // ✅ Get sprite renderer
    }

    public void TakeDamage(int damage)
    {
        //Debug.Log("TakeDamage() called. Damage: " + damage);

        /*
        if (isInvulnerable)
        {
            //Debug.Log("Player is invulnerable, ignoring damage.");
            return;
        }
        */ //invuln stuff

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthBar();

        //Debug.Log("Player Health: " + currentHealth);

        StartCoroutine(FlashRed()); // ✅ Flash red effect
        StartCoroutine(ShowDamageNumber(damage)); // ✅ Show floating damage text
        //StartCoroutine(InvulnerabilityCooldown()); //invuln stuff

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision detected with: " + other.name + " (Tag: " + other.tag + ")");

        if (other.CompareTag("EnemyProjectile"))
        {
            //Debug.Log("Hit by enemy projectile!");
            TakeDamage(10);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            //Debug.Log("Touched by an enemy!");
            TakeDamage(5);
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red; // ✅ Change to red
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white; // ✅ Reset to normal
    }

    private IEnumerator ShowDamageNumber(int damage)
    {
        if (damageTextPrefab != null)
        {
            Vector3 spawnPosition = transform.position + new Vector3(0, 1.5f, 0); // Move above head
            Debug.Log("Damage Text Spawn Position: " + spawnPosition); // ✅ Debug Log

            GameObject damageText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity);

            if (damageText.TryGetComponent(out TextMeshPro textMesh)) // ✅ Use TryGetComponent
            {
                textMesh.text = damage.ToString();
                //Debug.Log("Damage text set to: " + damage);
            }
            else
            {
                //Debug.LogError("❌ ERROR: DamageText prefab does NOT have a TextMeshPro component! Check the prefab.");
                yield break; // Stop the coroutine to prevent further errors
            }

            float duration = 1f;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = startPosition + new Vector3(0, 1, 0);
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                damageText.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1 - (elapsed / duration));
                yield return null;
            }

            Destroy(damageText);
        }
        else
        {
            //Debug.LogError("❌ ERROR: DamageTextPrefab is NOT assigned in PlayerHealth! Assign it in the Inspector.");
        }
    }


    /*
    private IEnumerator InvulnerabilityCooldown()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(0.5f); // ✅ 0.5s invulnerability
        isInvulnerable = false;
    }
    */ //invuln stuff
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        // Add respawn or game-over logic here
    }
}
