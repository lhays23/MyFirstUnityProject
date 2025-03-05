using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 10f;
    public int maxHealth = 15;
    private int currentHealth;
    protected Transform player;
    protected bool isUsingAbility = false;

    public Slider healthBar; // Reference to health bar UI

    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    protected virtual void Update()
    {
        if (player == null || isUsingAbility) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            MoveTowardPlayer();
        }

        if (ShouldUseAbility(distanceToPlayer))
        {
            StartCoroutine(UseAbility());
        }
    }

    protected virtual void MoveTowardPlayer()
    {
        if (player != null) // Extra safety check
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(name + " took " + damage + " damage!"); // Debug message

        currentHealth -= damage;

        if (healthBar != null && healthBar.value != (float)currentHealth / maxHealth)
        {
            UpdateHealthBar();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected abstract bool ShouldUseAbility(float distanceToPlayer);
    protected abstract System.Collections.IEnumerator UseAbility();
}
